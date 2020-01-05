// Find the closest paint color to a given color.
//
// @param   {ColorRGBA}
// @returns {ColorID}
//
function getClosestPaintColor ( %rgba )
{
	%alpha = getWord (%rgba, 3);

	%prevDist   = 100000;
	%colorMatch = 0;

	for ( %i = 0;  %i < 64;  %i++ )
	{
		%checkColor = getColorIDTable (%i);
		%checkRGB   = getWords (%checkColor, 0, 2);

		%alphaDiff = %alpha - getWord (%checkColor, 3);

		if ( vectorDist (%rgba, %checkRGB) < %prevDist  &&  %alphaDiff < 0.3  &&  %alphaDiff > -0.3 )
		{
			%prevDist   = vectorDist (%rgba, %checkColor);
			%colorMatch = %i;
		}
	}

	return %colorMatch;
}
