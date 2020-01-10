$BuildableSnow::Version = "0.9.0";


// For use by other mods that require a minimum version of this mod to work.
//
// @param {SemanticVersion} minVersion - A semantic version string of the minimum version of this
//                                       mod required for the invoking mod to work.
//
// @returns {boolean} Whether this mod is at least the minimum version required.
//
function BuildableSnow_RequireMinVersion ( %minVersion )
{
	return semanticVersionCompare (%minVersion, $BuildableSnow::Version) != 1;
}
