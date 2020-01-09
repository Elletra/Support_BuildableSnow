package Support_BuildableSnow
{
	// Block tricky players from trying to plant snow bricks.
	function serverCmdInstantUseBrick ( %client, %data )
	{
		if ( isObject (%data)  &&  !%data.isSnowBrick )
		{
			Parent::serverCmdInstantUseBrick (%client, %data);
		}
	}
};
activatePackage (Support_BuildableSnow);
