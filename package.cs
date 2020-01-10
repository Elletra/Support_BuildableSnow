//* Prevent tricky players from planting snow bricks. *//

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

	function ndTrustCheckSelect ( %obj, %group2, %bl_id, %admin )
	{
		if ( isObject (%obj)  &&  %obj.dataBlock.isSnowBrick  &&  !%admin )
		{
			return false;
		}

		return Parent::ndTrustCheckSelect (%obj, %group2, %bl_id, %admin);
	}

	function ndTrustCheckModify ( %obj, %group2, %bl_id, %admin )
	{
		if ( isObject (%obj)  &&  %obj.dataBlock.isSnowBrick  &&  !%admin )
		{
			return false;
		}

		return Parent::ndTrustCheckModify (%obj, %group2, %bl_id, %admin);
	}
};
schedule (0, 0, activatePackage, Support_BuildableSnow);
