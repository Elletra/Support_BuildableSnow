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
		$BuildableSnow::LastError = $BuildableSnow::Error::InvalidGridPos;
		return -1;
	}

	//* Everything uses global variables.  No hardcoded values. *//

	%data     = $BuildableSnow::DefaultDataBlock;
	%position = BuildableSnow_GridToWorld (%gridX, %gridY, %gridZ);
	%angleID  = $BuildableSnow::SnowAngleID;
	%color    = $BuildableSnow::SnowColorID;
	%group    = $BuildableSnow::SnowBrickGroup;

	%brick = createNewBrick (%data, %position, %angleID, %color, 1, %group, 0, 1);

	if ( !isObject (%brick) )
	{
		$BuildableSnow::LastError = $BuildableSnow::Error::CreateBrick;
		return -1;
	}

	BuildableSnowBrickset.add (%brick);

	// Basically how this add-on works is that it maintains a grid of vertices and a grid of bricks.
	// Four vertices make up a "tile".  The "tiles" in this case are bricks.
	//
	// Each brick has four vertices:
	//   1. Top left corner of the brick.
	//   2. Top right corner of the brick.
	//   3. Bottom left corner of the brick.
	//   4. Bottom right corner of the brick.
	//
	// Each vertex has only two heights: 0 or 1.
	// Each snow brick datablock corresponds to a certain configuration of four vertex heights.
	//
	// Any time a brick is updated, it checks its four vertex heights and attempts to set its
	// datablock to the appropriate one.  If it successfully changes its datablock, it updates
	// its immediate neighbors.
	//
	// See fxDTSBrick::setSnowVertices() and fxDTSBrick::updateSnow() to see how this works.
	//

	//* The brick's position in the "tile" grid. *//

	%brick.snowGridX = %gridX;
	%brick.snowGridY = %gridY;
	%brick.snowGridZ = %gridZ;

	//* The brick's vertex coordinates: two X values and two Y values. *//

	%brick.snowVertexLeft   = %gridX;      // Leftmost X vertex coordinate
	%brick.snowVertexRight  = %gridX + 1;  // Rightmost X vertex coordinate
	%brick.snowVertexTop    = %gridY;      // Topmost Y vertex coordinate
	%brick.snowVertexBottom = %gridY + 1;  // Bottommost Y vertex coordinate

	//* If there's already a brick at this position, delete it. *//

	%existingBrick = $BuildableSnow::Grid::Brick_[%gridX, %gridY, %gridZ];

	if ( isObject (%existingBrick) )
	{
		%existingBrick.delete ();
	}

	$BuildableSnow::Grid::Brick_[%gridX, %gridY, %gridZ] = %brick;

	%vertices = $BuildableSnow::DefaultDataBlock.snowVertices;

	%topLeft     = getWord (%vertices, 0);
	%topRight    = getWord (%vertices, 1);
	%bottomLeft  = getWord (%vertices, 2);
	%bottomRight = getWord (%vertices, 3);

	%brick.setSnowVertices (%topLeft, %topRight, %bottomLeft, %bottomRight);

	// The brick's datablock already corresponds to its vertices so we don't call updateSnow()

	$BuildableSnow::LastError = $BuildableSnow::Error::None;

	return %brick;
}
