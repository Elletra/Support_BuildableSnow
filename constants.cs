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

$BuildableSnow::Error::None           = 0;  // The operation was successful.
$BuildableSnow::Error::Generic        = 1;  // Reserved for possible future use.
$BuildableSnow::Error::NotSnowBrick   = 2;  // Brick we're trying to operate on isn't a snow brick.
$BuildableSnow::Error::HasSnowAbove   = 3;  // Brick we're trying to operate on has snow above it.
$BuildableSnow::Error::NoSnowBelow    = 4;  // Brick doesn't have the supporting snow required.
$BuildableSnow::Error::InvalidGridPos = 5;  // Attempted to use an invalid grid position.
$BuildableSnow::Error::BrickNotFound  = 6;  // No brick was found at this grid position.
$BuildableSnow::Error::CreateBrick    = 7;  // createBrick() error -- check $CreateBrick::LastError

$BuildableSnow::LastError = $BuildableSnow::Error::None;
