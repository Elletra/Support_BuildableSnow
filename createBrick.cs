if ( !isObject (BuildableSnowBrickset) )
{
	new SimSet (BuildableSnowBrickset);
}

// Creates snow brick at a grid position and adds it to the grid.
//
// @param {integer} gridX
// @param {integer} gridY
// @param {integer} gridZ
//
// @returns {fxDTSBrick|-1} Returns -1 if there was an issue creating or planting the brick.
//
function BuildableSnow_CreateSnowBrick ( %gridX, %gridY, %gridZ )
{
	if ( !BuildableSnow_isValidGridPos (%gridX, %gridY, %gridZ) )
	{
		error ("ERROR: BuildableSnow_CreateSnowBrick () - Invalid grid position");
		return -1;
	}

	%data     = $BuildableSnow::DataBlock_[1, 1, 1, 1];
	%position = BuildableSnow_GridToWorld (%gridX, %gridY, %gridZ);
	%angleID  = $BuildableSnow::SnowAngleID;
	%color    = $BuildableSnow::SnowColorID;
	%group    = $BuildableSnow::SnowBrickGroup;

	%brick = createBrick (%data, %position, %angleID, %color, 1, %group, 0, 1);

	if ( !isObject (%brick) )
	{
		return -1;
	}

	BuildableSnowBrickset.add (%brick);

	%brick.snowGridX = %gridX;
	%brick.snowGridY = %gridY;
	%brick.snowGridZ = %gridZ;

	%brick.snowVertexLeft   = %gridX;      // Leftmost X vertex coordinate
	%brick.snowVertexRight  = %gridX + 1;  // Rightmost X vertex coordinate
	%brick.snowVertexTop    = %gridY;      // Topmost Y vertex coordinate
	%brick.snowVertexBottom = %gridY + 1;  // Bottommost Y vertex coordinate

	%existingBrick = $BuildableSnow::Grid::Brick_[%gridX, %gridY, %gridZ];

	if ( isObject (%existingBrick) )
	{
		%existingBrick.delete ();
	}

	$BuildableSnow::Grid::Brick_[%gridX, %gridY, %gridZ] = %brick;

	%brick.setSnowVertices (1, 1, 1, 1);

	return %brick;
}
