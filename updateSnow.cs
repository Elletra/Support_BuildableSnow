// Update adjacent neighbor snow bricks.
function fxDTSBrick::updateSnowNeighbors ( %this )
{
	for ( %w = -1;  %w <= 1;  %w++ )
	{
		for ( %l = -1;  %l <= 1;  %l++ )
		{
			%neighbor = %this.getSnowNeighbor (%w, %l, 0);

			if ( isObject (%neighbor)  &&  %neighbor != %this )
			{
				%neighbor.updateSnow ();
			}
		}
	}
}

// Checks if a brick can be set to an adapter datablock, based on two adjacent neighbors relative
// to the direction of the adapter's slope.  If certain vertices are not set to zero, we can't
// change it.
//
// This function is a total hack just to make everything look less ugly.
//
// Basically, we want bricks under corner slopes to become adapters because it looks nicer.
// However, this can cause certain problems, so this is a special function to make sure that it
// doesn't happen.
//
// @param {fxDTSBrickData} data - The adapter datablock we want to set this brick to.
//
// @returns {boolean} Whether or not we can set this brick to the adapter.
//
function fxDTSBrick::snowAdapterCheck ( %this, %data )
{
	if ( !%this.dataBlock.isSnowBrick )
	{
		$BuildableSnow::LastError = $BuildableSnow::Error::NotSnowBrick;
		return false;
	}

	$BuildableSnow::LastError = $BuildableSnow::Error::None;

	switch$ ( %data )
	{
		// Top left adapter
		case $BuildableSnow::DataBlock_[0, 1, 1, 1]:
			// The two directions we're going to check.
			%coordX = -1;  // Which neighbor to check in the X direction.
			%coordY = -1;  // Which neighbor to check in the Y direction.

			// Vertices that need to be set to zero for this brick to be able to change.
			%emptyVert0 = 1;  // Corresponds to %coordX
			%emptyVert1 = 2;  // Corresponds to %coordY

		// Top right adapter
		case $BuildableSnow::DataBlock_[1, 0, 1, 1]:
			%coordX =  1;
			%coordY = -1;

			%emptyVert0 = 0;
			%emptyVert1 = 3;

		// Bottom left adapter
		case $BuildableSnow::DataBlock_[1, 1, 0, 1]:
			%coordX = -1;
			%coordY =  1;

			%emptyVert0 = 3;
			%emptyVert1 = 0;

		// Bottom right adapter
		case $BuildableSnow::DataBlock_[1, 1, 1, 0]:
			%coordX = 1;
			%coordY = 1;

			%emptyVert0 = 2;
			%emptyVert1 = 1;

		default:
			return false;
	}

	%neighbor0 = %this.getSnowNeighbor (%coordX, 0, 0);
	%neighbor1 = %this.getSnowNeighbor (0, %coordY, 0);

	for ( %i = 0;  %i < 2;  %i++ )
	{
		%checkBrick = %neighbor[%i];

		if ( !isObject (%checkBrick) )
		{
			continue;
		}

		%checkDB = %checkBrick.dataBlock;

		if ( !%checkDB.isSnowBrick  ||  getWord (%checkDB.snowVertices, %emptyVert[%i]) != 0 )
		{
			return false;
		}
	}

	return true;
}

// Updates snow brick datablock and, if needed, surrounding neighbors.
//
// @returns {boolean} Whether or not the operation was successful.  Use $BuildableSnow::LastError
//                    when checking for errors.
//
function fxDTSBrick::updateSnow ( %this )
{
	if ( !%this.dataBlock.isSnowBrick )
	{
		$BuildableSnow::LastError = $BuildableSnow::Error::NotSnowBrick;
		return false;
	}

	%vertices = %this.getSnowVertices ();

	%topLeft     = getWord (%vertices, 0);
	%topRight    = getWord (%vertices, 1);
	%bottomLeft  = getWord (%vertices, 2);
	%bottomRight = getWord (%vertices, 3);

	%aboveSnow = %this.getSnowNeighbor (0, 0, 1);

	// Bricks can only change their datablock if there are no snow bricks above it, either because
	// the above brick is invisible, or we're at the top of the grid.
	//
	// (Though there is an exception, which is explained below.)
	//
	%canUpdate = %this.hasEmptySnowSpot (0, 0, 1)  ||  !isObject (%aboveSnow);

	%data = $BuildableSnow::DataBlock_[1, 1, 1, 1];

	// As explained in the fxDTSBrick::snowAdapterCheck method, we want bricks under corner slopes
	// to become adapters because it looks nicer.  However, this can only happen if certain vertices
	// are set to 0.
	//
	if ( isObject (%aboveSnow)  &&  %aboveSnow.dataBlock.snowBrickType $= "corner" )
	{
		%aboveVertices = strReplace (%aboveSnow.getSnowVertices (), " ", "_");
		%adapter       = $BuildableSnow::CornerToAdapter_[%aboveVertices];

		if ( %this.snowAdapterCheck (%adapter) )
		{
			%data = %adapter;
		}
	}
	else if ( %canUpdate )
	{
		%data = $BuildableSnow::DataBlock_[%topLeft, %topRight, %bottomLeft, %bottomRight];
	}

	//* Update brick datablock and surrounding neighbors if the snow data has been changed. *//

	if ( %this.dataBlock !$= %data )
	{
		%this.setDataBlock (%data);
		%this.updateSnowNeighbors ();

		// Update snow brick below (if there is one).
		if ( %this.snowGridZ > 0 )
		{
			%this.getSnowNeighbor (0, 0, -1).updateSnow ();
		}
	}

	%this.setColliding (%data !$= $BuildableSnow::DataBlock_[0, 0, 0, 0]);
	%this.setRayCasting (%data !$= $BuildableSnow::DataBlock_[0, 0, 0, 0]);

	$BuildableSnow::LastError = $BuildableSnow::Error::None;

	return true;
}
