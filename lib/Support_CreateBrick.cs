// ------------------------------------------------------------------------
//  Title:   Create Brick
//  Author:  Electrk
//  Version: 1
//  Updated: January 7th, 2020
// ------------------------------------------------------------------------
//  Utility function for brick creation.
// ------------------------------------------------------------------------
//  Include this code in your own scripts as an *individual file* called
//  "Support_CreateBrick.cs".  Do not modify this code.
// ------------------------------------------------------------------------
//  Notes:
//    + Use $CreateBrick::LastError to check for errors.
//    + Use $CreateBrick::DebugMode to enable debug messages.
// ------------------------------------------------------------------------


if ( $CreateBrick::Version >= 1 )
{
	return;
}

$CreateBrick::Version = 1;

//* Error codes -- Do not change these. *//

$CreateBrick::Error::None          = 0;  // There was no error and brick creation was successful.
$CreateBrick::Error::PlantOverlap  = 1;  // "Overlap" plant error.
$CreateBrick::Error::PlantFloat    = 2;  // "Float" plant error.
$CreateBrick::Error::PlantStuck    = 3;  // "Stuck" plant error.
$CreateBrick::Error::PlantUnstable = 4;  // "Unstable" plant error.
$CreateBrick::Error::PlantBuried   = 5;  // "Buried" plant error.
$CreateBrick::Error::Generic       = 6;  // There was some unspecified error creating the brick.
$CreateBrick::Error::DataBlock     = 7;  // Tried to create brick with invalid/nonexistent datablock.
$CreateBrick::Error::AngleID       = 8;  // Tried to create brick with an invalid angle ID.
$CreateBrick::Error::BrickGroup    = 9;  // Tried to create brick with a nonexistent brick group.

// Use this variable to check for errors.
$CreateBrick::LastError = $CreateBrick::Error::None;

// Whether or not to print debug messages.
$CreateBrick::DebugMode = false;

// ------------------------------------------------


// Function for brick creation.
//
// @param {fxDTSBrickData} data          - The datablock of the brick we want to create.
// @param {Vector3D}       pos           - The position we want to create the brick at.
// @param {AngleID}        angID         - The angle ID (0-3) of the brick (converts to rotation).
// @param {ColorID}        color         - The color ID (0-63) of the brick.
// @param {boolean}        plant         - Whether or not we want the brick to be planted.
// @param {BrickGroup}     [group]       - The brick group we want to add the brick to.
//                                         Defaults to BrickGroup_888888.
// @param {boolean}        [ignoreStuck] - Whether to ignore the "stuck" error and plant anyway.
//                                         Defaults to false.
// @param {boolean}        [ignoreFloat] - Whether to ignore the "float" error and plant anyway.
//                                         Defaults to false.
//
// @returns {fxDTSBrick|-1} Returns -1 if there was an issue creating or planting the brick.
//
function createNewBrick ( %data, %pos, %angID, %color, %plant, %group, %ignoreStuck, %ignoreFloat )
{
	// Make sure that the datablock exists and is even a brick datablock.
	if ( !isObject (%data)  ||  %data.getClassName () !$= "fxDTSBrickData" )
	{
		createBrickError ($CreateBrick::Error::DataBlock, "Invalid datablock '" @ %data @ "'");
		return -1;
	}

	switch ( %angID )
	{
		case 0: %rotation = "1 0 0 0";
		case 1: %rotation = "0 0 1 90.0002";
		case 2: %rotation = "0 0 1 180";
		case 3: %rotation = "0 0 -1 90.0002";

		default:
			createBrickError ($CreateBrick::Error::AngleID, "Invalid angleID '" @ %angID @ "'");
			return -1;
	}

	if ( %group $= "" )
	{
		%group = BrickGroup_888888;
	}
	else if ( !isObject (%group) )
	{
		createBrickError ($CreateBrick::Error::BrickGroup, "Brick group '" @ %group @ "' does not exist");
		return -1;
	}

	%brick = new fxDTSBrick ()
	{
		dataBlock = %data;

		position = %pos;
		rotation = %rotation;
		angleID  = %angID;

		colorID = %color;

		isPlanted = %plant;
	};

	// If brick creation failed for whatever reason, set the error to generic and return.
	if ( !isObject (%brick) )
	{
		createBrickError ($CreateBrick::Error::Generic, "Error creating fxDTSBrick object");
		return -1;
	}

	%group.add (%brick);

	if ( isObject (%group.client) )
	{
		%brick.client = %group.client;
	}
	
	if ( %plant )
	{
		%error = %brick.plant ();

		// Any value other than 0 returned from plant() means an error occurred.
		if ( (%error == 1  &&  !%ignoreStuck)  ||  (%error == 2  &&  !%ignoreFloat)  ||  %error >= 3 )
		{
			%brick.delete ();
			createBrickError (%error, "Error planting brick (code: " @ %error @ ")");

			return -1;
		}

		%brick.onPlant ();
		%brick.setTrusted (true);
	}

	$CreateBrick::LastError = $CreateBrick::Error::None;

	return %brick;
}

// Internal use only.  Do not use this function.
//
// @param {CreateBrickError} errorCode
// @param {string}           errorMessage
//
// @private
//
function createBrickError ( %errorCode, %errorMessage )
{
	$CreateBrick::LastError = %errorCode;

	if ( $CreateBrick::DebugMode )
	{
		error ("ERROR: createNewBrick () - " @ %errorMessage);
	}
}
