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
	%data     = $BuildableSnow::DataBlock_[1, 1, 1, 1];
	%position = BuildableSnow_GridToWorld (%x, %y, %z);
	%angleID  = $BuildableSnow::SnowAngleID;
	%color    = $BuildableSnow::SnowColorID;
	%group    = $BuildableSnow::SnowBrickGroup;

	%brick = createBrick (%data, %position, %angleID, %color, 1, %group, 0, 1);

	if ( !isObject (%brick) )
	{
		return -1;
	}

	BuildableSnowBrickset.add (%brick);

	%brick.snowGridX = %x;
	%brick.snowGridY = %y;
	%brick.snowGridZ = %z;

	%brick.snowVertexLeft   = %x;      // Leftmost X vertex coordinate
	%brick.snowVertexRight  = %x + 1;  // Rightmost X vertex coordinate
	%brick.snowVertexTop    = %y;      // Topmost Y vertex coordinate
	%brick.snowVertexBottom = %y + 1;  // Bottommost Y vertex coordinate

	if ( isObject ($BuildableSnow::Grid::Brick_[%x, %y, %z]) )
	{
		$BuildableSnow::Grid::Brick_[%x, %y, %z].delete ();
	}

	$BuildableSnow::Grid::Brick_[%x, %y, %z] = %brick;

	%brick.setSnowVertices (1, 1, 1, 1);

	return %brick;
}
