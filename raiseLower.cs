// Lowers snow, provided there's no snow above this brick.
function fxDTSBrick::lowerSnow ( %this )
{
	%x = %this.gridX;
	%y = %this.gridY;
	%z = %this.gridZ;

	if ( !%this.hasEmptySpot (0, 0, 1) )
	{
		return;
	}

	%this.setSnowVertices (0, 0, 0, 0);

	%this.updateSnow ();
	%this.updateNeighbors ();
}

// Flattens brick if it's not already, and raises snow brick above it if it is.
function fxDTSBrick::raiseSnow ( %this )
{
	%x = %this.gridX;
	%y = %this.gridY;
	%z = %this.gridZ;

	// Don't raise snow if there's no brick below it (and we're not on the ground).
	if ( %z > 0  &&  %this.hasEmptySpot (0, 0, -1) )
	{
		return;
	}

	%aboveSnow = %this.getSnowNeighbor (0, 0, 1);

	// This is so we can actually plant snow on top of other snow, provided it's flat.
	if ( %this.dataBlock $= $BuildableSnow::DataBlocks_[1, 1, 1, 1]  &&  isObject (%aboveSnow) )
	{
		%aboveSnow.raiseSnow ();
	}

	%this.setSnowVertices (1, 1, 1, 1);

	%this.updateSnow ();
	%this.updateNeighbors ();
}
