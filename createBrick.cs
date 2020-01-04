if ( !isObject (BuildableSnowBrickset) )
{
	new SimSet (BuildableSnowBrickset);
}

// Creates snow brick at a position and adds it to the grid.
//
// @param {integer} x
// @param {integer} y
// @param {integer} z
//
// @returns {fxDTSBrick|-1} Returns -1 if there was an issue creating or planting the brick.
//
function BuildableSnow_CreateSnowBrick ( %x, %y, %z )
{
	%data     = $BuildableSnow::DataBlocks_[1, 1, 1, 1];
	%position = BuildableSnow_GridToWorld (%x, %y, %z);
	%angleID  = $BuildableSnow::SnowAngleID;
	%color    = $BuildableSnow::SnowColor;
	%group    = $BuildableSnow::SnowBrickGroup;

	%brick = createBrick (%data, %position, %angleID, %color, 1, %group, 0, 1);

	BuildableSnowBrickset.add (%brick);

	%brick.gridX = %x;
	%brick.gridY = %y;
	%brick.gridZ = %z;

	%brick.gridVertexLeft   = %x;      // Leftmost X vertex coordinate
	%brick.gridVertexRight  = %x + 1;  // Rightmost X vertex coordinate
	%brick.gridVertexTop    = %y;      // Topmost Y vertex coordinate
	%brick.gridVertexBottom = %y + 1;  // Bottommost Y vertex coordinate

	if ( isObject ($BuildableSnow::Grid::Brick_[%x, %y, %z]) )
	{
		$BuildableSnow::Grid::Brick_[%x, %y, %z].delete ();
	}

	$BuildableSnow::Grid::Brick_[%x, %y, %z] = %brick;

	%brick.setSnowVertices (1, 1, 1, 1);

	return %brick;
}
