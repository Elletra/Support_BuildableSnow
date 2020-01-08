// @param {string} message
function BuildableSnow_DebugMessage ( %message )
{
	if ( $BuildableSnow::DebugMode )
	{
		echo (%message);
	}
}

// @param {string} message
function BuildableSnow_DebugWarn ( %message )
{
	if ( $BuildableSnow::DebugMode )
	{
		warn ("WARNING: " @ %message);
	}
}

// @param {string} message
function BuildableSnow_DebugError ( %message )
{
	if ( $BuildableSnow::DebugMode )
	{
		error ("ERROR: " @ %message);
	}
}
