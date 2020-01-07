// ------------------------------------------------------------------------
//  Title:   Create Brick
//  Author:  Electrk
//  Version: 1
//  Updated: January 6th, 2020
// ------------------------------------------------------------------------
//  Utility function for brick creation.
// ------------------------------------------------------------------------
//  Include this code in your own scripts as an *individual file* called
//  "Support_CreateBrick.cs".  Do not modify this code.
// ------------------------------------------------------------------------
//  This function does not print error messages.  Instead, it sets the
//  $CreateBrick::LastError variable to a value and returns -1.  Use this
//  variable to check for errors.
// ------------------------------------------------------------------------

if ( $CreateBrick::Version >= 1 )
{
	return;
}

$CreateBrick::Version = 1;

//* Error codes -- Do not change these. *//

$CreateBrick::Error::None          = 0;
$CreateBrick::Error::PlantOverlap  = 1;
$CreateBrick::Error::PlantFloat    = 2;
$CreateBrick::Error::PlantStuck    = 3;
$CreateBrick::Error::PlantUnstable = 4;
$CreateBrick::Error::PlantBuried   = 5;
$CreateBrick::Error::Generic       = 6;
$CreateBrick::Error::DataBlock     = 7;
$CreateBrick::Error::AngleID       = 8;
$CreateBrick::Error::BrickGroup    = 9;

$CreateBrick::LastError = $CreateBrick::Error::None;


// ------------------------------------------------

// Function for brick creation.
//
// @param {fxDTSBrickData} data          - The datablock of the brick we want to create.
// @param {Vector3D}       pos           - The position we want to create the brick at.
// @param {AngleID}        angID         - The angle ID (0-3) of the brick.
// @param {ColorID}        colorID       - The color ID (0-63) of the brick.
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
function createBrick ( %data, %pos, %angID, %colorID, %plant, %group, %ignoreStuck, %ignoreFloat )
{
	if ( !isObject (%data)  ||  %data.getClassName () !$= "fxDTSBrickData" )
	{
		$CreateBrick::LastError = $CreateBrick::Error::DataBlock;
		return -1;
	}

	switch ( %angID )
	{
		case 0: %rotation = "1 0 0 0";
		case 1: %rotation = "0 0 1 90.0002";
		case 2: %rotation = "0 0 1 180";
		case 3: %rotation = "0 0 -1 90.0002";

		default:
			$CreateBrick::LastError = $CreateBrick::Error::AngleID;
			return -1;
	}

	if ( %group $= "" )
	{
		%group = BrickGroup_888888;
	}
	else if ( !isObject (%group) )
	{
		$CreateBrick::LastError = $CreateBrick::Error::BrickGroup;
		return -1;
	}

	%brick = new fxDTSBrick ()
	{
		dataBlock = %data;

		position = %pos;
		rotation = %rotation;
		angleID  = %angID;

		colorID = %colorID;

		isPlanted = %plant;
	};

	if ( !isObject (%brick) )
	{
		$CreateBrick::LastError = $CreateBrick::Error::Generic;
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

		if ( (%error == 1  &&  !%ignoreStuck)  ||  (%error == 2  &&  !%ignoreFloat)  ||  %error >= 3 )
		{
			%brick.delete ();
			$CreateBrick::LastError = %error;

			return -1;
		}

		%brick.onPlant ();
		%brick.setTrusted (true);
	}

	$CreateBrick::LastError = $CreateBrick::Error::None;

	return %brick;
}
