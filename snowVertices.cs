// Sets the brick's snow vertices data.
//
// Does not change the brick's datablock (@see fxDTSBrick::updateSnow)
//
// @param {boolean} topLeft
// @param {boolean} topRight
// @param {boolean} bottomLeft
// @param {boolean} bottomRight
//
// @returns {BuildableSnowError}
//
function fxDTSBrick::setSnowVertices ( %this, %topLeft, %topRight, %bottomLeft, %bottomRight )
{
	if ( !%this.dataBlock.isSnowBrick )
	{
		return $BuildableSnow::Error::NotSnowBrick;
	}

	%left   = %this.snowVertexLeft;
	%right  = %this.snowVertexRight;
	%top    = %this.snowVertexTop;
	%bottom = %this.snowVertexBottom;

	%x = %this.snowGridX;
	%y = %this.snowGridY;
	%z = %this.snowGridZ;

	$BuildableSnow::Grid::Vertex_[%left,  %top,    %z] = %topLeft;
	$BuildableSnow::Grid::Vertex_[%right, %top,    %z] = %topRight;
	$BuildableSnow::Grid::Vertex_[%left,  %bottom, %z] = %bottomLeft;
	$BuildableSnow::Grid::Vertex_[%right, %bottom, %z] = %bottomRight;

	return $BuildableSnow::Error::None;
}

// @returns {BuildableSnowVertices} A string with the vertices separated by a space in this order:
//                                  top left, top right, bottom left, bottom right.
//
function fxDTSBrick::getSnowVertices ( %this )
{
	%left   = %this.snowVertexLeft;
	%right  = %this.snowVertexRight;
	%top    = %this.snowVertexTop;
	%bottom = %this.snowVertexBottom;

	%z = %this.snowGridZ;

	%topLeft     = $BuildableSnow::Grid::Vertex_[%left,  %top,    %z];
	%topRight    = $BuildableSnow::Grid::Vertex_[%right, %top,    %z];
	%bottomLeft  = $BuildableSnow::Grid::Vertex_[%left,  %bottom, %z];
	%bottomRight = $BuildableSnow::Grid::Vertex_[%right, %bottom, %z];

	return %topLeft SPC %topRight SPC %bottomLeft SPC %bottomRight;
}
