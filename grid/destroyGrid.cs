// Deletes all bricks in the existing grid, if any, and deletes all grid-related variables.
//
// @param {boolean} [useAsync]      - Use async brick deletion using schedules.
// @param {string}  [asyncCallback] - Function to call when async brick deletion is done.
//
function BuildableSnow_DestroyGrid ( %useAsync, %asyncCallback )
{
	if ( isEventPending ($BuildableSnow::CreateGridTick) )
	{
		cancel ($BuildableSnow::CreateGridTick);
	}

	if ( isEventPending ($BuildableSnow::DestroyGridTick) )
	{
		cancel ($BuildableSnow::DestroyGridTick);
	}

	if ( %useAsync )
	{
		BuildableSnow_DestroyGrid_Tick (%asyncCallback);
		return;
	}

	BuildableSnowBrickset.deleteAll ();
	deleteVariables ("$BuildableSnow::Grid::*");
}

// Tick function for async grid destruction.  Internal use only.
//
// @param {string} [asyncCallback]
// @private
//
function BuildableSnow_DestroyGrid_Tick ( %asyncCallback )
{
	cancel ($BuildableSnow::DestroyGridTick);

	if ( BuildableSnowBrickset.getCount () <= 0 )
	{
		$BuildableSnow::DestroyGridTick = "";
		deleteVariables ("$BuildableSnow::Grid::*");

		if ( %asyncCallback !$= "" )
		{
			call (%asyncCallback);
		}

		return;
	}

	BuildableSnowBrickset.getObject (0).delete ();

	$BuildableSnow::DestroyGridTick = schedule ($BuildableSnow::DestroyGridTickRate, MissionCleanup,
		BuildableSnow_DestroyGrid_Tick, %asyncCallback);
}
