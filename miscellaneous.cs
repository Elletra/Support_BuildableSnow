// Gets the position of the center of the player.
//
// @returns {Vector3D}
//
function Player::getPlayerCenter ( %this )
{
	%worldBoxZ = getWord (%this.getWorldBoxCenter (), 2);

	return vectorAdd (%this.position, "0 0 " @ %worldBoxZ / 4);
}
