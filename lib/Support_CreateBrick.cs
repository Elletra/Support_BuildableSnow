// ----------------------------------------------------------------------
//  Title:   Create Brick
//  Author:  Electrk
//  Version: 1
//  Updated: January 2nd, 2020
// ----------------------------------------------------------------------
//  Utility function for brick creation.
// ----------------------------------------------------------------------
//  Include this code in your own scripts as an *individual file*
//  called "Support_CreateBrick.cs".  Do not modify this code.
// ----------------------------------------------------------------------

if ( $Support_CreateBrick::Version >= 1 )
{
	return;
}

$Support_CreateBrick::Version = 1;


// Function for brick creation.
//
// @param {fxDTSBrickData} data          - The datablock of the brick we want to create.
// @param {Vector3D}       pos           - The position we want to create the brick at.
// @param {AngleID}        angID         - The angle ID (0-3) of the brick.
// @param {ColorID}        colorID       - The color ID (0-63) of the brick.
// @param {boolean}        plant         - Whether or not we want the brick to be planted.
// @param {BrickGroup}     [group]       - Defaults to BrickGroup_888888.
// @param {boolean}        [ignoreStuck] - Whether to ignore the "stuck" error and plant anyway.
// @param {boolean}        [ignoreFloat] - Whether to ignore the "float" error and plant anyway.
//
// @returns {fxDTSBrick|-1} Returns -1 if there was an issue creating or planting the brick.
//
function createBrick ( %data, %pos, %angID, %color, %plant, %group, %ignoreStuck, %ignoreFloat )
{
	switch ( %angID )
	{
		case 0: %rotation = "1 0 0 0";
		case 1: %rotation = "0 0 1 90.0002";
		case 2: %rotation = "0 0 1 180";
		case 3: %rotation = "0 0 -1 90.0002";

		default:
			error ("createBrick () - Invalid angleID: " @ %angID);
			return -1;
	}

	if ( %group $= "" )
	{
		%group = BrickGroup_888888;
	}
	else if ( !isObject (%group) )
	{
		error ("createBrick () - Brick group does not exist!");
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
			error ("createBrick () - Could not plant brick: " @ %error);

			return -1;
		}

		%brick.onPlant ();
		%brick.setTrusted (true);
	}

	return %brick;
}
