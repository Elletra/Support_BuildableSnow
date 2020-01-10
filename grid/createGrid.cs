// Main function for creating a snow brick grid.
//
// @param {integer} width
// @param {integer} length
// @param {integer} height
// @param {boolean} [useAsync]      - Use async brick planting via schedules.
// @param {string}  [asyncCallback] - Function to call when async brick planting is done.
//
function BuildableSnow_CreateGrid ( %width, %length, %height, %useAsync, %asyncCallback )
{
	if ( isEventPending ($BuildableSnow::CreateGridTick) )
	{
		cancel ($BuildableSnow::CreateGridTick);
	}

	if ( isEventPending ($BuildableSnow::DestroyGridTick) )
	{
		cancel ($BuildableSnow::DestroyGridTick);
	}

	BuildableSnow_DestroyGrid ();

	if ( %width $= ""  ||  %length $= ""  ||  %height $= "" )
	{
		error ("ERROR: BuildableSnow_CreateGrid () - Missing required parameter(s)");
		return;
	}

	$BuildableSnow::Grid::Width  = %width;
	$BuildableSnow::Grid::Length = %length;
	$BuildableSnow::Grid::Height = %height;

	if ( %useAsync )
	{
		BuildableSnow_CreateGrid_Tick (0, 0, 0, %asyncCallback);
		return;
	}

	for ( %x = 0;  %x < $BuildableSnow::Grid::Width;  %x++ )
	{
		for ( %y = 0;  %y < $BuildableSnow::Grid::Length;  %y++ )
		{
			for ( %z = 0;  %z < $BuildableSnow::Grid::Height;  %z++ )
			{
				BuildableSnow_CreateSnowBrick (%x, %y, %z);
			}
		}
	}
}

// Tick function for async grid creation.  Internal use only.  Do not use this function.
//
// @param {integer} x
// @param {integer} y
// @param {integer} z
// @param {integer} [asyncCallback]
//
// @private
//
function BuildableSnow_CreateGrid_Tick ( %x, %y, %z, %asyncCallback )
{
	BuildableSnow_CreateSnowBrick (%x, %y, %z);

	cancel ($BuildableSnow::CreateGridTick);

	%x++;

	if ( %x >= $BuildableSnow::Grid::Width )
	{
		%x = 0;
		%y++;
	}

	if ( %y >= $BuildableSnow::Grid::Length )
	{
		%x = 0;
		%y = 0;
		%z++;
	}

	if ( %z >= $BuildableSnow::Grid::Height )
	{
		$BuildableSnow::CreateGridTick = "";

		if ( %asyncCallback !$= "" )
		{
			call (%asyncCallback);
		}

		return;
	}

	$BuildableSnow::CreateGridTick = schedule ($BuildableSnow::CreateGridTickRate, MissionCleanup,
		BuildableSnow_CreateGrid_Tick, %x, %y, %z, %asyncCallback);
}
