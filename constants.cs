//* Various constants related to brick planting. *//

$BuildableSnow::SnowBrickGroup = BrickGroup_888888;
$BuildableSnow::SnowColorID    = getClosestPaintColor ("1 1 1 1");
$BuildableSnow::SnowAngleID    = 3;

$BuildableSnow::CreateGridTickRate  = 0;
$BuildableSnow::DestroyGridTickRate = 0;

//* Has vertices: top left, top right, bottom left, bottom right. *//

$BuildableSnow::DataBlock_[0, 0, 0, 0] = brick_snow_empty_data;
$BuildableSnow::DataBlock_[0, 0, 0, 1] = brick_snow_top_left_data;
$BuildableSnow::DataBlock_[0, 0, 1, 1] = brick_snow_top_middle_data;
$BuildableSnow::DataBlock_[0, 0, 1, 0] = brick_snow_top_right_data;
$BuildableSnow::DataBlock_[0, 1, 0, 1] = brick_snow_middle_left_data;
$BuildableSnow::DataBlock_[1, 1, 1, 1] = brick_snow_middle_middle_data;
$BuildableSnow::DataBlock_[1, 0, 1, 0] = brick_snow_middle_right_data;
$BuildableSnow::DataBlock_[0, 1, 0, 0] = brick_snow_bottom_left_data;
$BuildableSnow::DataBlock_[1, 1, 0, 0] = brick_snow_bottom_middle_data;
$BuildableSnow::DataBlock_[1, 0, 0, 0] = brick_snow_bottom_right_data;
$BuildableSnow::DataBlock_[0, 1, 1, 1] = brick_snow_top_left_adapter_data;
$BuildableSnow::DataBlock_[1, 0, 1, 1] = brick_snow_top_right_adapter_data;
$BuildableSnow::DataBlock_[1, 1, 0, 1] = brick_snow_bottom_left_adapter_data;
$BuildableSnow::DataBlock_[1, 1, 1, 0] = brick_snow_bottom_right_adapter_data;
$BuildableSnow::DataBlock_[0, 1, 1, 0] = brick_snow_top_left_bottom_right_data;
$BuildableSnow::DataBlock_[1, 0, 0, 1] = brick_snow_top_right_bottom_left_data;

//* A quick way to get a respective adapter brick from corner vertices. *//

$BuildableSnow::CornerToAdapter_[0, 0, 0, 1] = brick_snow_top_left_adapter_data;
$BuildableSnow::CornerToAdapter_[0, 0, 1, 0] = brick_snow_top_right_adapter_data;
$BuildableSnow::CornerToAdapter_[0, 1, 0, 0] = brick_snow_bottom_left_adapter_data;
$BuildableSnow::CornerToAdapter_[1, 0, 0, 0] = brick_snow_bottom_right_adapter_data;

//* Vertex order indices. *//

$BuildableSnow::Vertex::TopLeft     = 0;
$BuildableSnow::Vertex::TopRight    = 1;
$BuildableSnow::Vertex::BottomLeft  = 2;
$BuildableSnow::Vertex::BottomRight = 3;

//* Error codes -- Do not change these. *//

$BuildableSnow::Error::None           = 0;  // The operation was successful.
$BuildableSnow::Error::Generic        = 1;  // Reserved for possible future use.
$BuildableSnow::Error::NotSnowBrick   = 2;  // Brick we're trying to operate on isn't a snow brick.
$BuildableSnow::Error::HasSnowAbove   = 3;  // Brick we're trying to operate on has snow above it.
$BuildableSnow::Error::NoSnowBelow    = 4;  // Brick doesn't have the supporting snow required.
$BuildableSnow::Error::InvalidGridPos = 5;  // Attempted to use an invalid grid position.
$BuildableSnow::Error::BrickNotFound  = 6;  // No brick was found at this grid position.
$BuildableSnow::Error::CreateBrick    = 7;  // createBrick() error -- check $CreateBrick::LastError

$BuildableSnow::LastError = $BuildableSnow::Error::None;
