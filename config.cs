// Configurable global variables that you can change only if you know what you're doing.
//
// If you want to make a mod that replaces the existing snow datablocks with other bricks, change
// the $BuildableSnow::DataBlock_* and $BuildableSnow::CornerToAdapter_* variables.
//

// Whether or not to print debug messages.
$BuildableSnow::DebugMode = false;

//* Variables related to brick planting. *//

$BuildableSnow::SnowBrickGroup = BrickGroup_888888;
$BuildableSnow::SnowColorID    = getClosestPaintColor ("1 1 1 1");
$BuildableSnow::SnowAngleID    = 3;

//* Tick rates of async grid creation/destruction. *//

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

$BuildableSnow::DefaultDataBlock = brick_snow_middle_middle_data;

//* A quick way to get a respective adapter brick from corner vertices. *//

$BuildableSnow::CornerToAdapter_[0, 0, 0, 1] = brick_snow_top_left_adapter_data;
$BuildableSnow::CornerToAdapter_[0, 0, 1, 0] = brick_snow_top_right_adapter_data;
$BuildableSnow::CornerToAdapter_[0, 1, 0, 0] = brick_snow_bottom_left_adapter_data;
$BuildableSnow::CornerToAdapter_[1, 0, 0, 0] = brick_snow_bottom_right_adapter_data;
