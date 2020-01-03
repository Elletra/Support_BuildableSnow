// @param {0|1} topLeft
// @param {0|1} topRight
// @param {0|1} bottomLeft
// @param {0|1} bottomRight
//
function fxDTSBrick::setSnowVertices ( %this, %topLeft, %topRight, %bottomLeft, %bottomRight )
{
	if ( !%this.dataBlock.isSnowBrick )
	{
		return;
	}

	%left   = %this.gridVertexLeft;
	%right  = %this.gridVertexRight;
	%top    = %this.gridVertexTop;
	%bottom = %this.gridVertexBottom;

	%x = %this.gridX;
	%y = %this.gridY;
	%z = %this.gridZ;

	$BuildableSnow::Grid::Vertex_[%left,  %top,    %z] = %topLeft;
	$BuildableSnow::Grid::Vertex_[%right, %top,    %z] = %topRight;
	$BuildableSnow::Grid::Vertex_[%left,  %bottom, %z] = %bottomLeft;
	$BuildableSnow::Grid::Vertex_[%right, %bottom, %z] = %bottomRight;
}

// @returns {SnowVertices} A string with the vertices separated by a space in this order:
//                         top left, top right, bottom left, bottom right.
//
function fxDTSBrick::getSnowVertices ( %this )
{
	%left   = %this.gridVertexLeft;
	%right  = %this.gridVertexRight;
	%top    = %this.gridVertexTop;
	%bottom = %this.gridVertexBottom;

	%z = %this.gridZ;

	%topLeft     = $BuildableSnow::Grid::Vertex_[%left,  %top,    %z];
	%topRight    = $BuildableSnow::Grid::Vertex_[%right, %top,    %z];
	%bottomLeft  = $BuildableSnow::Grid::Vertex_[%left,  %bottom, %z];
	%bottomRight = $BuildableSnow::Grid::Vertex_[%right, %bottom, %z];

	return %topLeft SPC %topRight SPC %bottomLeft SPC %bottomRight;
}
