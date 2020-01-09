// Creates snow brick at a grid position and adds it to the grid.
//
// @param {integer} gridX
// @param {integer} gridY
// @param {integer} gridZ
//
// @returns {fxDTSBrick|-1} Returns -1 if there was an issue creating or planting the brick.
//
function BuildableSnow_CreateSnowBrick ( %gridX, %gridY, %gridZ )
{
	if ( !BuildableSnow_isValidGridPos (%gridX, %gridY, %gridZ) )
	{
		%errorMsg = "Invalid grid position: (" @ %gridX @ ", " @ %gridY @ ", " @ %gridZ @ ")";
		BuildableSnow_DebugError (%errorMsg);

		return -1;
	}

	//* (@see config.cs) *//

	%data     = $BuildableSnow::DefaultDataBlock;
	%position = BuildableSnow_GridToWorld (%gridX, %gridY, %gridZ);
	%angleID  = $BuildableSnow::SnowAngleID;
	%color    = $BuildableSnow::SnowColorID;
	%group    = $BuildableSnow::SnowBrickGroup;

	%brick = createNewBrick (%data, %position, %angleID, %color, 1, %group, 0, 1);

	if ( !isObject (%brick) )
	{
		BuildableSnow_DebugError ("createNewBrick() - Error code: " @ $CreateBrick::LastError);
		return -1;
	}

	//* (@see grid/insertBrick.cs) *//

	%error = %brick.insertIntoSnowGrid (%gridX, %gridY, %gridZ);

	if ( %error != $BuildableSnow::Error::None )
	{
		%brick.delete ();
		BuildableSnow_DebugError ("Could not insert brick into grid (code: " @ %error @ ")");

		return -1;
	}

	return %brick;
}
