//* Various constants related to brick planting. *//

$BuildableSnow::SnowBrickGroup     = BrickGroup_888888;
$BuildableSnow::SnowColorID        = getClosestPaintColor ("1 1 1 1");
$BuildableSnow::SnowAngleID        = 3;
$BuildableSnow::CreateGridTickRate = 1;

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

//* Various errors to return from functions. *//

$BuildableSnow::Error::None         = 0;
$BuildableSnow::Error::Generic      = 1;
$BuildableSnow::Error::NotSnowBrick = 2;
$BuildableSnow::Error::HasSnowAbove = 3;
$BuildableSnow::Error::NoSnowBelow  = 4;
