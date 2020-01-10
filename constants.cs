// Constant values/read-only variables -- Do not change these.
//
// There are some global variables that you can change in config.cs
// These are not those.  Do not change these.
//

//* Vertex order indices. *//

$BuildableSnow::Vertex::TopLeft     = 0;
$BuildableSnow::Vertex::TopRight    = 1;
$BuildableSnow::Vertex::BottomLeft  = 2;
$BuildableSnow::Vertex::BottomRight = 3;

//* Error codes *//

$BuildableSnow::Error::None             = 0;  // The operation was successful.
$BuildableSnow::Error::Generic          = 1;  // Reserved for possible future use.
$BuildableSnow::Error::NotSnowBrick     = 2;  // Brick we're trying to operate on isn't a snow brick.
$BuildableSnow::Error::NotInGrid        = 3;  // Brick we're trying to operate on isn't in the grid.
$BuildableSnow::Error::HasSnowAbove     = 4;  // Brick we're trying to operate on has snow above it.
$BuildableSnow::Error::NoSnowBelow      = 5;  // Brick doesn't have the supporting snow required.
$BuildableSnow::Error::InvalidGridPos   = 6;  // The grid position we tried to use is invalid.
$BuildableSnow::Error::InvalidDataBlock = 7;  // The datablock we tried to use is invalid/nonexistent.
