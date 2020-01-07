// Sets the brick's snow vertices data.
//
// This does not change the brick's datablock (@see fxDTSBrick::updateSnow).
//
// @param {boolean} topLeft
// @param {boolean} topRight
// @param {boolean} bottomLeft
// @param {boolean} bottomRight
//
// @returns {boolean} Whether or not the operation was successful.  Use $BuildableSnow::LastError
//                    to check for errors.
//
function fxDTSBrick::setSnowVertices ( %this, %topLeft, %topRight, %bottomLeft, %bottomRight )
{
	if ( !%this.dataBlock.isSnowBrick )
	{
		$BuildableSnow::LastError = $BuildableSnow::Error::NotSnowBrick;
		return false;
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

	$BuildableSnow::LastError = $BuildableSnow::Error::None;

	return true;
}

// Returns the actual snow vertex data.
//
// Since the brick's datablock does not always necessarily correspond with the actual vertex data
// for aesthetic purposes, we want to be able to accurately get the vertex data.
//
// @returns {BuildableSnowVertices|null} A string with the vertices separated by a space in this order:
//                                       top left, top right, bottom left, bottom right.
//                                       Or an empty string (null) if it's not a snow brick.
//
function fxDTSBrick::getSnowVertices ( %this )
{
	if ( !%this.dataBlock.isSnowBrick )
	{
		$BuildableSnow::LastError = $BuildableSnow::Error::NotSnowBrick;
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

	$BuildableSnow::LastError = $BuildableSnow::Error::None;

	return %topLeft @ " " @ %topRight @ " " @ %bottomLeft @ " " @ %bottomRight;
}
