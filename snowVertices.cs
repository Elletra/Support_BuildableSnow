// Sets the brick's snow vertex data.
//
// This does not change the brick's datablock (@see fxDTSBrick::updateSnow).
//
// @param {boolean} topLeft     - Height of the top left vertex (0 or 1).
// @param {boolean} topRight    - Height of the top right vertex (0 or 1).
// @param {boolean} bottomLeft  - Height of the bottom left vertex (0 or 1).
// @param {boolean} bottomRight - Height of the bottom right vertex (0 or 1).
//
// @returns {BuildableSnowError}
//
function fxDTSBrick::setSnowVertices ( %this, %topLeft, %topRight, %bottomLeft, %bottomRight )
{
	if ( !%this.dataBlock.isSnowBrick )
	{
		return $BuildableSnow::Error::NotSnowBrick;
	}

	if ( !%this.isInSnowGrid )
	{
		return $BuildableSnow::Error::NotInGrid;
	}

	%left   = %this.snowVertexLeft;
	%right  = %this.snowVertexRight;
	%top    = %this.snowVertexTop;
	%bottom = %this.snowVertexBottom;

	%z = %this.snowGridZ;

	//* The vertex grid: (x, y, z) *//

	$BuildableSnow::Grid::Vertex_[%left,  %top,    %z] = %topLeft;
	$BuildableSnow::Grid::Vertex_[%right, %top,    %z] = %topRight;
	$BuildableSnow::Grid::Vertex_[%left,  %bottom, %z] = %bottomLeft;
	$BuildableSnow::Grid::Vertex_[%right, %bottom, %z] = %bottomRight;

	return $BuildableSnow::Error::None;
}

// Gets the actual snow vertex data.
//
// Since the brick's datablock does not always correspond with the actual vertex data for aesthetic
// purposes, we want to be able to accurately get the vertex data.
//
// @returns {BuildableSnowVertices|null} Returns empty string (null) if not a snow brick or not in grid.
//
function fxDTSBrick::getSnowVertices ( %this )
{
	if ( !%this.dataBlock.isSnowBrick  ||  !%this.isInSnowGrid )
	{
		return "";
	}

	%left   = %this.snowVertexLeft;
	%right  = %this.snowVertexRight;
	%top    = %this.snowVertexTop;
	%bottom = %this.snowVertexBottom;

	%z = %this.snowGridZ;

	%topLeft     = $BuildableSnow::Grid::Vertex_[%left,  %top,    %z];
	%topRight    = $BuildableSnow::Grid::Vertex_[%right, %top,    %z];
	%bottomLeft  = $BuildableSnow::Grid::Vertex_[%left,  %bottom, %z];
	%bottomRight = $BuildableSnow::Grid::Vertex_[%right, %bottom, %z];

	return %topLeft @ " " @ %topRight @ " " @ %bottomLeft @ " " @ %bottomRight;
}
