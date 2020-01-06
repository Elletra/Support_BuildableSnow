// Main function for creating a snow brick grid.
//
// @param {integer} width
// @param {integer} length
// @param {integer} height
// @param {boolean} [useAsync]      - Use async brick planting via schedules.
// @param {string}  [asyncCallback] - Function to call when async brick planting is done.
//
function BuildableSnow_CreateGrid ( %width, %length, %height, %useAsync, %asyncCallback )
{
	if ( isEventPending ($BuildableSnow::CreateGridTick) )
	{
		cancel ($BuildableSnow::CreateGridTick);
	}

	BuildableSnow_DestroyGrid ();

	if ( %width $= ""  ||  %length $= ""  ||  %height $= "" )
	{
		error ("ERROR: BuildableSnow_CreateGrid () - Missing required parameter(s)");
		return;
	}

	$BuildableSnow::Grid::Width  = %width;
	$BuildableSnow::Grid::Length = %length;
	$BuildableSnow::Grid::Height = %height;

	if ( %useAsync )
	{
		BuildableSnow_CreateGrid_Tick (0, 0, 0, %asyncCallback);
		return;
	}

	for ( %x = 0;  %x < $BuildableSnow::Grid::Width;  %x++ )
	{
		for ( %y = 0;  %y < $BuildableSnow::Grid::Length;  %y++ )
		{
			for ( %z = 0;  %z < $BuildableSnow::Grid::Height;  %z++ )
			{
				BuildableSnow_PlaceGridBrick (%x, %y, %z);
			}
		}
	}
}

// Tick function for async grid creation.  Internal use only.
//
// @param {integer} x
// @param {integer} y
// @param {integer} z
// @param {integer} [asyncCallback]
//
// @private
//
function BuildableSnow_CreateGrid_Tick ( %x, %y, %z, %asyncCallback )
{
	BuildableSnow_PlaceGridBrick (%x, %y, %z);

	cancel ($BuildableSnow::CreateGridTick);

	%x++;

	if ( %x >= $BuildableSnow::Grid::Width )
	{
		%x = 0;
		%y++;
	}

	if ( %y >= $BuildableSnow::Grid::Length )
	{
		%x = 0;
		%y = 0;
		%z++;
	}

	if ( %z >= $BuildableSnow::Grid::Height )
	{
		$BuildableSnow::CreateGridTick = "";

		if ( %asyncCallback !$= "" )
		{
			call (%asyncCallback);
		}

		return;
	}

	$BuildableSnow::CreateGridTick = schedule ($BuildableSnow::CreateGridTickRate, MissionCleanup,
		BuildableSnow_CreateGrid_Tick, %x, %y, %z, %asyncCallback);
}

// Simple wrapper function used by both CreateGrid functions to create and plant snow bricks.
//
// @param {integer} x
// @param {integer} y
// @param {integer} z
//
// @returns {fxDTSBrick|-1} Returns -1 if there was an issue creating or planting the brick.
//
function BuildableSnow_PlaceGridBrick ( %x, %y, %z )
{
	%brick = BuildableSnow_CreateSnowBrick (%x, %y, %z);

	if ( isObject (%brick) )
	{
		%brick.updateSnow ();
	}

	return %brick;
}

// Deletes all bricks in the existing grid, if any, and deletes all grid-related variables.
function BuildableSnow_DestroyGrid ()
{
	BuildableSnowBrickset.deleteAll ();
	deleteVariables ("$BuildableSnow::Grid::*");
}

// @param {integer} x
// @param {integer} y
// @param {integer} z
//
// @returns {fxDTSBrick|-1} Returns -1 if no brick found.
//
function BuildableSnow_GetBrick ( %x, %y, %z )
{
	%brick = $BuildableSnow::Grid::Brick_[%x, %y, %z];

	if ( isObject (%brick) )
	{
		return %brick;
	}

	return -1;
}

// Converts a grid coordinate to a world coordinate, based on the default snow brick size.
//
// @param {integer} x
// @param {integer} y
// @param {integer} z
//
// @returns {Vector3D}
//
function BuildableSnow_GridToWorld ( %x, %y, %z )
{
	%sizeX = $BuildableSnow::DataBlock_[1, 1, 1, 1].brickSizeX;
	%sizeY = $BuildableSnow::DataBlock_[1, 1, 1, 1].brickSizeY;
	%sizeZ = $BuildableSnow::DataBlock_[1, 1, 1, 1].brickSizeZ;

	%worldX = -(%x * (%sizeX / 2));
	%worldY =  (%y * (%sizeY / 2));
	%worldZ =  (%z * (%sizeZ / 5)) + 0.3;

	return %worldX SPC %worldY SPC %worldZ;
}

// Checks whether or not a grid position is within the grid's bounds.
//
// @param {integer} x
// @param {integer} y
// @param {integer} z
//
// @returns {boolean}
//
function BuildableSnow_isValidGridPos ( %x, %y, %z )
{
	return %x >= 0  &&  %x < $BuildableSnow::Grid::Width   &&
	       %y >= 0  &&  %y < $BuildableSnow::Grid::Length  &&
	       %z >= 0  &&  %z < $BuildableSnow::Grid::Height;
}

// Gets the brick at the position (x, y, z) relative to this brick, if any.
//
// @param {integer} x
// @param {integer} y
// @param {integer} z
//
// @returns {fxDTSBrick|-1} Returns -1 if no brick found.
//
function fxDTSBrick::getSnowNeighbor ( %this, %x, %y, %z )
{
	%neighborX = %this.snowGridX + %x;
	%neighborY = %this.snowGridY + %y;
	%neighborZ = %this.snowGridZ + %z;

	%neighbor = BuildableSnow_GetBrick (%neighborX, %neighborY, %neighborZ);

	if ( isObject (%neighbor) )
	{
		return %neighbor;
	}

	return -1;
}

// Whether or not there's a brick at the position (x, y, z) relative to this brick.
//
// @returns {boolean}
//
function fxDTSBrick::hasSnowNeighbor ( %this, %x, %y, %z )
{
	return isObject (%this.getSnowNeighbor (%x, %y, %z));
}

// Whether or not there's an empty spot at the position (x, y, z) relative to this brick.
// "Empty spot" meaning either an invisible brick, or a lack of a brick.
//
// @param {integer} x
// @param {integer} y
// @param {integer} z
//
// @returns {boolean}
//
function fxDTSBrick::hasEmptySnowSpot ( %this, %x, %y, %z )
{
	%neighbor = %this.getSnowNeighbor (%x, %y, %z);

	if ( isObject (%neighbor) )
	{
		return %neighbor.dataBlock $= $BuildableSnow::DataBlock_[0, 0, 0, 0];
	}

	return true;
}
