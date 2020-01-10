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
	%sizeX = $BuildableSnow::DefaultDataBlock.brickSizeX;
	%sizeY = $BuildableSnow::DefaultDataBlock.brickSizeY;
	%sizeZ = $BuildableSnow::DefaultDataBlock.brickSizeZ;

	%worldX = -(%x * (%sizeX / 2));
	%worldY =  (%y * (%sizeY / 2));
	%worldZ =  (%z * (%sizeZ / 5)) + (0.1 * %sizeZ);

	return %worldX @ " " @ %worldY @ " " @ %worldZ;
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
	return %x == ~~%x  &&  %y == ~~%y  &&  %z == ~~%z  &&
	       %x >= 0  &&  %x < $BuildableSnow::Grid::Width   &&
	       %y >= 0  &&  %y < $BuildableSnow::Grid::Length  &&
	       %z >= 0  &&  %z < $BuildableSnow::Grid::Height;
}
