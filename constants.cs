$BuildableSnow::SnowBrickGroup = BrickGroup_888888;
$BuildableSnow::SnowColor      = 15;
$BuildableSnow::SnowAngleID    = 3;

//* Has vertices: top left, top right, bottom left, bottom right. *//

$BuildableSnow::DataBlocks_[0, 0, 0, 0] = brick_snow_empty_data;
$BuildableSnow::DataBlocks_[0, 0, 0, 1] = brick_snow_top_left_data;
$BuildableSnow::DataBlocks_[0, 0, 1, 1] = brick_snow_top_middle_data;
$BuildableSnow::DataBlocks_[0, 0, 1, 0] = brick_snow_top_right_data;
$BuildableSnow::DataBlocks_[0, 1, 0, 1] = brick_snow_middle_left_data;
$BuildableSnow::DataBlocks_[1, 1, 1, 1] = brick_snow_middle_middle_data;
$BuildableSnow::DataBlocks_[1, 0, 1, 0] = brick_snow_middle_right_data;
$BuildableSnow::DataBlocks_[0, 1, 0, 0] = brick_snow_bottom_left_data;
$BuildableSnow::DataBlocks_[1, 1, 0, 0] = brick_snow_bottom_middle_data;
$BuildableSnow::DataBlocks_[1, 0, 0, 0] = brick_snow_bottom_right_data;
$BuildableSnow::DataBlocks_[0, 1, 1, 1] = brick_snow_top_left_adapter_data;
$BuildableSnow::DataBlocks_[1, 0, 1, 1] = brick_snow_top_right_adapter_data;
$BuildableSnow::DataBlocks_[1, 1, 0, 1] = brick_snow_bottom_left_adapter_data;
$BuildableSnow::DataBlocks_[1, 1, 1, 0] = brick_snow_bottom_right_adapter_data;
$BuildableSnow::DataBlocks_[0, 1, 1, 0] = brick_snow_top_left_bottom_right_data;
$BuildableSnow::DataBlocks_[1, 0, 0, 1] = brick_snow_top_right_bottom_left_data;

//* A quick way to get a respective adapter brick from corner vertices. *//

$BuildableSnow::CornerToAdapter_[0, 0, 0, 1] = brick_snow_top_left_adapter_data;
$BuildableSnow::CornerToAdapter_[0, 0, 1, 0] = brick_snow_top_right_adapter_data;
$BuildableSnow::CornerToAdapter_[0, 1, 0, 0] = brick_snow_bottom_left_adapter_data;
$BuildableSnow::CornerToAdapter_[1, 0, 0, 0] = brick_snow_bottom_right_adapter_data;
