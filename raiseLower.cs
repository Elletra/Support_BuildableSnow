// Lowers snow, provided there's no snow above this brick.
function fxDTSBrick::lowerSnow ( %this )
{
	if ( !%this.hasEmptySnowSpot (0, 0, 1) )
	{
		return;
	}

	%this.setSnowVertices (0, 0, 0, 0);
	%this.updateSnow ();
}

// Flattens brick if it's not already, and raises snow brick above it if it is.
function fxDTSBrick::raiseSnow ( %this )
{
	// Don't raise snow if there's a brick above it.
	if ( !%this.hasEmptySnowSpot (0, 0, 1) )
	{
		return;
	}

	%z = %this.gridZ;

	// Don't raise snow if there's no brick below it, provided we're not on the ground.
	if ( %z > 0  &&  %this.hasEmptySnowSpot (0, 0, -1) )
	{
		return;
	}

	// Raise snow brick above if this brick is flat.
	if ( %this.dataBlock $= $BuildableSnow::DataBlock_[1, 1, 1, 1] )
	{
		%aboveSnow = %this.getSnowNeighbor (0, 0, 1);

		if ( isObject (%aboveSnow) )
		{
			%aboveSnow.raiseSnow ();
		}
	}

	//* Make sure the surrounding bricks below it even exist to support raising it. *//

	for ( %w = -1;  %w <= 1;  %w++ )
	{
		for ( %l = -1;  %l <= 1;  %l++ )
		{
			if ( %z > 0  &&  %this.hasEmptySnowSpot (%w, %l, -1) )
			{
				return;
			}
		}
	}

	%this.setSnowVertices (1, 1, 1, 1);
	%this.updateSnow ();
}
