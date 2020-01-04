// Lowers snow, provided there's no snow above this brick.
function fxDTSBrick::lowerSnow ( %this )
{
	%x = %this.gridX;
	%y = %this.gridY;
	%z = %this.gridZ;

	if ( !%this.hasEmptySnowSpot (0, 0, 1) )
	{
		return;
	}

	%this.setSnowVertices (0, 0, 0, 0);

	%this.updateSnow ();
	%this.updateSnowNeighbors ();
}

// Flattens brick if it's not already, and raises snow brick above it if it is.
function fxDTSBrick::raiseSnow ( %this )
{
	%x = %this.gridX;
	%y = %this.gridY;
	%z = %this.gridZ;

	// Don't raise snow if there's no brick below it (and we're not on the ground).
	if ( %z > 0  &&  %this.hasEmptySnowSpot (0, 0, -1) )
	{
		return;
	}

	//* Raise snow brick above if this brick is flat and there's no snow brick above already. *//

	%isFlat = %this.dataBlock $= $BuildableSnow::DataBlock_[1, 1, 1, 1];

	if ( %isFlat  &&  %this.hasEmptySnowSpot (0, 0, 1) )
	{
		%aboveSnow = %this.getSnowNeighbor (0, 0, 1);

		if ( isObject (%aboveSnow) )
		{
			%aboveSnow.raiseSnow ();
		}
	}

	%this.setSnowVertices (1, 1, 1, 1);

	%this.updateSnow ();
	%this.updateSnowNeighbors ();
}
