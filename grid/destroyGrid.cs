// Deletes all bricks in the existing grid, if any, and deletes all grid-related variables.
function BuildableSnow_DestroyGrid ()
{
	BuildableSnowBrickset.deleteAll ();
	deleteVariables ("$BuildableSnow::Grid::*");
}
