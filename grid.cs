// @param {integer} [width]  - Defaults to 16.
// @param {integer} [length] - Defaults to 16.
// @param {integer} [height] - Defaults to 1.
//
function BuildableSnow_CreateGrid ( %width, %length, %height )
{
	BuildableSnow_DestroyGrid ();

	$BuildableSnow::Grid::Width  = (%width  $= "" ? 16 : %width);
	$BuildableSnow::Grid::Length = (%length $= "" ? 16 : %length);
	$BuildableSnow::Grid::Height = (%height $= "" ? 1  : %height);

	for ( %x = 0;  %x < $BuildableSnow::Grid::Width;  %x++ )
	{
		for ( %y = 0;  %y < $BuildableSnow::Grid::Length;  %y++ )
		{
			for ( %z = 0;  %z < $BuildableSnow::Grid::Height;  %z++ )
			{
				%brick = BuildableSnow_CreateSnowBrick (%x, %y, %z);

				if ( isObject (%brick) )
				{
					%brick.updateSnow ();
				}
			}
		}
	}
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
	%gridX = %this.snowGridX;
	%gridY = %this.snowGridY;
	%gridZ = %this.snowGridZ;

	%neighbor = BuildableSnow_GetBrick (%gridX + %x, %gridY + %y, %gridZ + %z);

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
	%gridX = %this.snowGridX;
	%gridY = %this.snowGridY;
	%gridZ = %this.snowGridZ;

	%neighbor = BuildableSnow_GetBrick (%gridX + %x, %gridY + %y, %gridZ + %z);

	if ( isObject (%neighbor) )
	{
		return %neighbor.dataBlock $= $BuildableSnow::DataBlock_[0, 0, 0, 0];
	}

	return true;
}
