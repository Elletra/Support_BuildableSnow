package Support_BuildableSnow
{
	function serverCmdInstantUseBrick ( %client, %data )
	{
		if ( isObject (%data)  &&  !%data.isSnowBrick )
		{
			Parent::serverCmdInstantUseBrick (%client, %data);
		}
	}

	function serverCmdPlantBrick ( %client )
	{
		if ( isObject (%player = %client.player)  &&  isObject (%tempBrick = %player.tempBrick) )
		{
			if ( %tempBrick.dataBlock.isSnowBrick )
			{
				%tempBrick.delete ();
				%player.tempBrick = "";

				return;
			}
		}

		Parent::serverCmdPlantBrick (%client);
	}
};
activatePackage (Support_BuildableSnow);
