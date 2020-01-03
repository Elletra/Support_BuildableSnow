// Update adjacent neighbor snow bricks.
function fxDTSBrick::updateSnowNeighbors ( %this )
{
	%x = %this.gridX;
	%y = %this.gridY;
	%z = %this.gridZ;

	for ( %w = -1;  %w < 2;  %w++ )
	{
		for ( %l = -1;  %l < 2;  %l++ )
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
// to the direction of the adapter's slope.  If either of the neighbors is a middle snow brick,
// we can't change it.
//
// This function is a total hack just to make everything look less ugly.
//
// Basically, we want bricks under corner slopes to become adapters because it looks nicer.
// However, it looks hideous when middle snow bricks are on either side of the adapter, so this is
// a special function to make sure that this doesn't happen.
//
// @param {fxDTSBrickData} data - The adapter datablock we want to set this brick to.
//
// @returns {boolean} Whether or not we can set this brick to the adapter.
//
function fxDTSBrick::snowAdapterCheck ( %this, %data )
{
	switch$ ( %data )
	{
		// Top left adapter
		case $BuildableSnow::DataBlocks_[0, 1, 1, 1]:
			%coordX = -1;
			%coordY = -1;

		// Top right adapter
		case $BuildableSnow::DataBlocks_[1, 0, 1, 1]:
			%coordX =  1;
			%coordY = -1;

		// Bottom left adapter
		case $BuildableSnow::DataBlocks_[1, 1, 0, 1]:
			%coordX = -1;
			%coordY =  1;

		// Bottom right adapter
		case $BuildableSnow::DataBlocks_[1, 1, 1, 0]:
			%coordX = 1;
			%coordY = 1;

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

		// Check if neighbor is a middle brick.
		if ( %checkDB $= $BuildableSnow::DataBlocks_[1, 1, 1, 1] )
		{
			return false;
		}
	}

	return true;
}

function fxDTSBrick::updateSnow ( %this )
{
	if ( !%this.dataBlock.isSnowBrick )
	{
		return;
	}

	%left   = %this.gridVertexLeft;
	%right  = %this.gridVertexRight;
	%top    = %this.gridVertexTop;
	%bottom = %this.gridVertexBottom;

	%x = %this.gridX;
	%y = %this.gridY;
	%z = %this.gridZ;

	%topLeft     = $BuildableSnow::Grid::Vertex_[%left,  %top,    %z];
	%topRight    = $BuildableSnow::Grid::Vertex_[%right, %top,    %z];
	%bottomLeft  = $BuildableSnow::Grid::Vertex_[%left,  %bottom, %z];
	%bottomRight = $BuildableSnow::Grid::Vertex_[%right, %bottom, %z];

	%aboveSnow = %this.getSnowNeighbor (0, 0, 1);

	// Bricks can only change their datablock if there are no snow bricks above it, either because
	// the above brick is invisible, or we're at the top of the grid.
	//
	// (Though there is an exception, which is explained below.)
	//
	%canUpdate = %this.hasEmptySnowSpot (0, 0, 1)  ||  !isObject (%aboveSnow);

	%data = $BuildableSnow::DataBlocks_[1, 1, 1, 1];

	// As explained in the fxDTSBrick::snowAdapterCheck method, we want bricks under corner slopes
	// to become adapters because it looks nicer.  However, this is only if said brick is not
	// surrounded by middle bricks, otherwise that would look hideous.
	//
	if ( isObject (%aboveSnow)  &&  %aboveSnow.dataBlock.isSnowCorner )
	{
		%vertices = strReplace (%aboveSnow.getSnowVertices (), " ", "_");
		%adapter  = $BuildableSnow::CornerToAdapter_[%vertices];

		if ( %this.snowAdapterCheck (%adapter) )
		{
			%data = %adapter;
		}
	}
	else if ( %canUpdate )
	{
		%data = $BuildableSnow::DataBlocks_[%topLeft, %topRight, %bottomLeft, %bottomRight];
	}

	%this.setDataBlock (%data);

	// Update snow brick below (if there is one).
	if ( %z > 0 )
	{
		%this.getSnowNeighbor (0, 0, -1).updateSnow ();
	}

	%this.setColliding (%data !$= $BuildableSnow::DataBlocks_[0, 0, 0, 0]);
	%this.setRayCasting (%data !$= $BuildableSnow::DataBlocks_[0, 0, 0, 0]);
}
