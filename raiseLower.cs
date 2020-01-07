// Lowers snow, provided there's no snow above this brick.
//
// @returns {boolean} Whether or not the operation was successful.  Use $BuildableSnow::LastError
//                    to check for errors.
//
function fxDTSBrick::lowerSnow ( %this )
{
	if ( !%this.dataBlock.isSnowBrick )
	{
		$BuildableSnow::LastError = $BuildableSnow::Error::NotSnowBrick;
		return false;
	}

	if ( !%this.hasEmptySnowSpot (0, 0, 1) )
	{
		$BuildableSnow::LastError = $BuildableSnow::Error::HasSnowAbove;
		return false;
	}

	%this.setSnowVertices (0, 0, 0, 0);
	%this.updateSnow ();

	$BuildableSnow::LastError = $BuildableSnow::Error::None;

	return true;
}

// Flattens brick if it's not already; raises snow brick above if it is.
//
// @returns {boolean} Whether or not the operation was successful.  Use $BuildableSnow::LastError
//                    to check for errors.
//
function fxDTSBrick::raiseSnow ( %this )
{
	if ( !%this.dataBlock.isSnowBrick )
	{
		$BuildableSnow::LastError = $BuildableSnow::Error::NotSnowBrick;
		return false;
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
					$BuildableSnow::LastError = $BuildableSnow::Error::NoSnowBelow;
					return false;
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

	$BuildableSnow::LastError = $BuildableSnow::Error::None;

	return true;
}
