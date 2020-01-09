// Gets the brick at the position (x, y, z) relative to this brick, if any.
//
// @param {integer} x
// @param {integer} y
// @param {integer} z
//
// @returns {fxDTSBrick|-1} Returns -1 if no brick found.
//
function fxDTSBrick::getSnowNeighbor ( %this, %x, %y, %z )
{
	if ( !%this.dataBlock.isSnowBrick )
	{
		BuildableSnow_DebugError ("Brick " @ %this.getID () @ " is not a snow brick!");
		return -1;
	}

	if ( !%this.isInSnowGrid )
	{
		BuildableSnow_DebugError ("Brick " @ %this.getID () @ " is not in the snow grid!");
		return -1;
	}

	%neighborX = %this.snowGridX + %x;
	%neighborY = %this.snowGridY + %y;
	%neighborZ = %this.snowGridZ + %z;

	%neighbor = BuildableSnow_GetBrick (%neighborX, %neighborY, %neighborZ);

	if ( isObject (%neighbor) )
	{
		return %neighbor;
	}

	return -1;
}

// Whether or not there's a brick at the position (x, y, z) relative to this brick.
//
// @returns {boolean}
//
function fxDTSBrick::hasSnowNeighbor ( %this, %x, %y, %z )
{
	return isObject (%this.getSnowNeighbor (%x, %y, %z));
}

// Whether or not there's an empty spot at the position (x, y, z) relative to this brick.
// "Empty spot" meaning either an empty snow brick, or the lack of a brick.
//
// @param {integer} x
// @param {integer} y
// @param {integer} z
//
// @returns {boolean}
//
function fxDTSBrick::hasEmptySnowSpot ( %this, %x, %y, %z )
{
	%neighbor = %this.getSnowNeighbor (%x, %y, %z);

	if ( isObject (%neighbor) )
	{
		return %neighbor.dataBlock $= $BuildableSnow::DataBlock_[0, 0, 0, 0];
	}

	return true;
}
