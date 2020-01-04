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

	%vertices = %this.getSnowVertices ();

	%topLeft     = getWord (%vertices, 0);
	%topRight    = getWord (%vertices, 1);
	%bottomLeft  = getWord (%vertices, 2);
	%bottomRight = getWord (%vertices, 3);

	%this.setSnowVertices (0, 0, 0, 0);

	%below = %this.getSnowNeighbor (0, 0, -1);

	if ( isObject (%below)  &&  %below.getSnowVertices () $= "0 0 0 0" )
	{
		%below.setSnowVertices (%topLeft, %topRight, %bottomLeft, %bottomRight);
	}

	%this.updateSnow ();

	return $BuildableSnow::Error::None;
}

// Flattens brick if it's not already, and raises snow brick above it if it is.
//
// @returns {BuildableSnowError}
//
function fxDTSBrick::raiseSnow ( %this )
{
	if ( !%this.dataBlock.isSnowBrick )
	{
		return $BuildableSnow::Error::NotSnowBrick;
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

	if ( %this.hasEmptySnowSpot (0, 0, 1) )
	{
		%this.updateSnow ();
	}
	else
	{
		// We can only update the neighbors if there's a snow brick above.
		%this.updateSnowNeighbors ();
	}

	return $BuildableSnow::Error::None;
}
