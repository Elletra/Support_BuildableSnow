$BuildableSnow::Version = "0.9.0";


// For use by other mods to require a minimum version of this mod for them to work.
//
// @param {SemanticVersion} minVersion - The minimum version of this mod required for the invoking
//                                       mod to work.
//
// @returns {boolean} Whether this mod is at least the minimum version required.
//
function BuildableSnow_RequireMinVersion ( %minVersion )
{
	return semanticVersionCompare (%minVersion, $BuildableSnow::Version) != 1;
}
