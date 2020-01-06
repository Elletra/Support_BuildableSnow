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
