// Lowers snow, provided there's no snow above this brick.
//
// @returns {BuildableSnowError}
//
function fxDTSBrick::lowerSnow ( %this )
{
	if ( !%this.dataBlock.isSnowBrick )
	{
		return $BuildableSnow::Error::NotSnowBrick;
	}

	if ( !%this.hasEmptySnowSpot (0, 0, 1) )
	{
		return $BuildableSnow::Error::HasSnowAbove;
	}

	%this.setSnowVertices (0, 0, 0, 0);
	%this.updateSnow ();

	return $BuildableSnow::Error::None;
}

// Flattens brick if it's not already; raises snow brick above if it is.
//
// @returns {BuildableSnowError}
//
function fxDTSBrick::raiseSnow ( %this )
{
	if ( !%this.dataBlock.isSnowBrick )
	{
		return $BuildableSnow::Error::NotSnowBrick;
	}

	//* Make sure the surrounding bricks below even exist to support raising it. *//

	if ( %this.snowGridZ > 0 )
	{
		for ( %w = -1;  %w <= 1;  %w++ )
		{
			for ( %l = -1;  %l <= 1;  %l++ )
			{
				if ( %this.hasEmptySnowSpot (%w, %l, -1)  &&  %this.hasSnowNeighbor (%w, %l, -1) )
				{
					return $BuildableSnow::Error::NoSnowBelow;
				}
			}
		}
	}

	//* Raise snow above if this brick is flat and there's no snow above it. *//

	%isAboveEmpty = %this.hasEmptySnowSpot (0, 0, 1);

	if ( %isAboveEmpty  &&  %this.dataBlock $= $BuildableSnow::DataBlock_[1, 1, 1, 1] )
	{
		%aboveSnow = %this.getSnowNeighbor (0, 0, 1);

		if ( isObject (%aboveSnow) )
		{
			%aboveSnow.raiseSnow ();
		}
	}

	%this.setSnowVertices (1, 1, 1, 1);

	if ( %isAboveEmpty )
	{
		%this.updateSnow ();
	}
	else
	{
		// If there's snow above, we can only update the neighbor bricks.
		%this.updateSnowNeighbors ();
	}

	return $BuildableSnow::Error::None;
}
