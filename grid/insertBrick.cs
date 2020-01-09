if ( !isObject (BuildableSnowBrickset) )
{
	new SimSet (BuildableSnowBrickset);
}

// Adds this brick to the snow grid at (x, y, z), deleting any existing brick at that position.
//
// @param {integer} gridX
// @param {integer} gridY
// @param {integer} gridZ
//
// @returns {BuildableSnowError}
//
function fxDTSBrick::insertIntoSnowGrid ( %this, %gridX, %gridY, %gridZ )
{
	if ( !%this.dataBlock.isSnowBrick )
	{
		return $BuildableSnow::Error::NotSnowBrick;
	}

	// We're already in the snow grid, no need to insert it again.
	if ( %this.isInSnowGrid )
	{
		return $BuildableSnow::Error::None;
	}

	if ( !BuildableSnow_isValidGridPos (%gridX, %gridY, %gridZ) )
	{
		return $BuildableSnow::Error::InvalidGridPos;
	}

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

	if ( !BuildableSnowBrickset.isMember (%this) )
	{
		BuildableSnowBrickset.add (%this);
	}

	//* The brick's position in the "tile" grid. *//

	%this.snowGridX = %gridX;
	%this.snowGridY = %gridY;
	%this.snowGridZ = %gridZ;

	//* The brick's vertex coordinates: two X values and two Y values. *//

	%this.snowVertexLeft   = %gridX;      // Leftmost X vertex coordinate
	%this.snowVertexRight  = %gridX + 1;  // Rightmost X vertex coordinate
	%this.snowVertexTop    = %gridY;      // Topmost Y vertex coordinate
	%this.snowVertexBottom = %gridY + 1;  // Bottommost Y vertex coordinate

	//* If there's already a brick at this position, delete it. *//

	%existingBrick = $BuildableSnow::Grid::Brick_[%gridX, %gridY, %gridZ];

	if ( isObject (%existingBrick)  &&  %existingBrick != %this )
	{
		%existingBrick.delete ();
	}

	$BuildableSnow::Grid::Brick_[%gridX, %gridY, %gridZ] = %this;

	%vertices = %this.dataBlock.snowVertices;

	%topLeft     = getWord (%vertices, 0);
	%topRight    = getWord (%vertices, 1);
	%bottomLeft  = getWord (%vertices, 2);
	%bottomRight = getWord (%vertices, 3);

	%this.isInSnowGrid = true;

	%this.setSnowVertices (%topLeft, %topRight, %bottomLeft, %bottomRight);
	%this.updateSnow ();

	return $BuildableSnow::Error::None;
}
