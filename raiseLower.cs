// Lowers snow, provided there's no snow above this brick.
//
// @returns {BuildableSnowError}
//
function fxDTSBrick::lowerSnow ( %this )
{
	if ( !%this.hasEmptySnowSpot (0, 0, 1) )
	{
		return $BuildableSnow::Error::HasSnowAbove;
	}

	%this.setSnowVertices (0, 0, 0, 0);
	%this.updateSnow ();

	return $BuildableSnow::Error::None;
}

// Flattens brick if it's not already, and raises snow brick above it if it is.
//
// @returns {BuildableSnowError}
//
function fxDTSBrick::raiseSnow ( %this )
{
	// Don't raise snow if there's a brick above it.
	if ( !%this.hasEmptySnowSpot (0, 0, 1) )
	{
		return $BuildableSnow::Error::HasSnowAbove;
	}

	%z = %this.gridZ;

	//* Make sure the surrounding bricks below it even exist to support raising it. *//

	for ( %w = -1;  %w <= 1;  %w++ )
	{
		for ( %l = -1;  %l <= 1;  %l++ )
		{
			if ( %z > 0  &&  %this.hasEmptySnowSpot (%w, %l, -1) )
			{
				return $BuildableSnow::Error::NoSnowBelow;
			}
		}
	}

	// Raise snow above if this brick is flat.
	if ( %this.dataBlock $= $BuildableSnow::DataBlock_[1, 1, 1, 1] )
	{
		%aboveSnow = %this.getSnowNeighbor (0, 0, 1);

		if ( isObject (%aboveSnow) )
		{
			%aboveSnow.raiseSnow ();
		}
	}

	%this.setSnowVertices (1, 1, 1, 1);
	%this.updateSnow ();

	return $BuildableSnow::Error::None;
}
