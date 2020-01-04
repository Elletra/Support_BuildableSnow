// Bricks have obnoxiously long names because of an old method I'm not using anymore.
// Also makes it easier to programmatically pick bricks based on their names/directions if need be.

datablock fxDTSBrickData (brick_snow_empty_data)
{
	brickFile   = "./brickFiles/empty.blb";
	category    = "Building Snow";
	subcategory = "Special";
	uiName      = "Invisible";

	isSnowBrick   = true;
	snowBrickType = "empty";
	snowVertices  = "0 0 0 0";
};

datablock fxDTSBrickData (brick_snow_top_left_data)
{
	brickFile          = "./brickFiles/topLeft.blb";
	collisionShapeName = "./collisionFiles/topLeft.dts";
	category           = "Building Snow";
	subcategory        = "Snow";
	uiName             = "Top Left";

	isSnowBrick   = true;
	snowBrickType = "corner";
	snowVertices  = "0 0 0 1";
};

datablock fxDTSBrickData (brick_snow_top_middle_data)
{
	brickFile          = "./brickFiles/topMiddle.blb";
	collisionShapeName = "./collisionFiles/topMiddle.dts";
	category           = "Building Snow";
	subcategory        = "Snow";
	uiName             = "Top Middle";

	isSnowBrick   = true;
	snowBrickType = "ramp";
	snowVertices  = "0 0 1 1";
};

datablock fxDTSBrickData (brick_snow_top_right_data)
{
	brickFile          = "./brickFiles/topRight.blb";
	collisionShapeName = "./collisionFiles/topRight.dts";
	category           = "Building Snow";
	subcategory        = "Snow";
	uiName             = "Top Right";

	isSnowBrick   = true;
	snowBrickType = "corner";
	snowVertices  = "0 0 1 0";
};

datablock fxDTSBrickData (brick_snow_middle_left_data)
{
	brickFile          = "./brickFiles/middleLeft.blb";
	collisionShapeName = "./collisionFiles/middleLeft.dts";
	category           = "Building Snow";
	subcategory        = "Snow";
	uiName             = "Middle Left";

	isSnowBrick   = true;
	snowBrickType = "ramp";
	snowVertices  = "0 1 0 1";
};

datablock fxDTSBrickData (brick_snow_middle_middle_data)
{
	brickFile   = "./brickFiles/middleMiddle.blb";
	category    = "Building Snow";
	subcategory = "Snow";
	uiName      = "Middle Middle";

	isSnowBrick   = true;
	snowBrickType = "middle";
	snowVertices  = "1 1 1 1";
};

datablock fxDTSBrickData (brick_snow_middle_right_data)
{
	brickFile          = "./brickFiles/middleRight.blb";
	collisionShapeName = "./collisionFiles/middleRight.dts";
	category           = "Building Snow";
	subcategory        = "Snow";
	uiName             = "Middle Right";

	isSnowBrick   = true;
	snowBrickType = "ramp";
	snowVertices  = "1 0 1 0";
};

datablock fxDTSBrickData (brick_snow_bottom_left_data)
{
	brickFile          = "./brickFiles/bottomLeft.blb";
	collisionShapeName = "./collisionFiles/bottomLeft.dts";
	category           = "Building Snow";
	subcategory        = "Snow";
	uiName             = "Bottom Left";

	isSnowBrick   = true;
	snowBrickType = "corner";
	snowVertices  = "0 1 0 0";
};

datablock fxDTSBrickData (brick_snow_bottom_middle_data)
{
	brickFile          = "./brickFiles/bottomMiddle.blb";
	collisionShapeName = "./collisionFiles/bottomMiddle.dts";
	category           = "Building Snow";
	subcategory        = "Snow";
	uiName             = "Bottom Middle";

	isSnowBrick   = true;
	snowBrickType = "ramp";
	snowVertices  = "1 1 0 0";
};

datablock fxDTSBrickData (brick_snow_bottom_right_data)
{
	brickFile          = "./brickFiles/bottomRight.blb";
	collisionShapeName = "./collisionFiles/bottomRight.dts";
	category           = "Building Snow";
	subcategory        = "Snow";
	uiName             = "Bottom Right";

	isSnowBrick   = true;
	snowBrickType = "corner";
	snowVertices  = "1 0 0 0";
};

datablock fxDTSBrickData (brick_snow_top_left_adapter_data)
{
	brickFile          = "./brickFiles/topLeftAdapter.blb";
	collisionShapeName = "./collisionFiles/topLeftAdapter.dts";
	category           = "Building Snow";
	subcategory        = "Snow";
	uiName             = "Top Left Adapter";

	isSnowBrick   = true;
	snowBrickType = "adapter";
	snowVertices  = "0 1 1 1";

	// Valid adjacent ramps.
	snowRampX = brick_snow_top_middle_data;
	snowRampY = brick_snow_middle_left_data;
};

datablock fxDTSBrickData (brick_snow_top_right_adapter_data)
{
	brickFile          = "./brickFiles/topRightAdapter.blb";
	collisionShapeName = "./collisionFiles/topRightAdapter.dts";
	category           = "Building Snow";
	subcategory        = "Snow";
	uiName             = "Top Right Adapter";

	isSnowBrick   = true;
	snowBrickType = "adapter";
	snowVertices  = "1 0 1 1";

	// Valid adjacent ramps.
	snowRampX = brick_snow_top_middle_data;
	snowRampY = brick_snow_middle_right_data;
};

datablock fxDTSBrickData (brick_snow_bottom_left_adapter_data)
{
	brickFile          = "./brickFiles/bottomLeftAdapter.blb";
	collisionShapeName = "./collisionFiles/bottomLeftAdapter.dts";
	category           = "Building Snow";
	subcategory        = "Snow";
	uiName             = "Bottom Left Adapter";

	isSnowBrick   = true;
	snowBrickType = "adapter";
	snowVertices  = "1 1 0 1";

	// Valid adjacent ramps.
	snowRampX = brick_snow_bottom_middle_data;
	snowRampY = brick_snow_middle_left_data;
};

datablock fxDTSBrickData (brick_snow_bottom_right_adapter_data)
{
	brickFile          = "./brickFiles/bottomRightAdapter.blb";
	collisionShapeName = "./collisionFiles/bottomRightAdapter.dts";
	category           = "Building Snow";
	subcategory        = "Snow";
	uiName             = "Bottom Right Adapter";

	isSnowBrick   = true;
	snowBrickType = "adapter";
	snowVertices  = "1 1 1 0";

	// Valid adjacent ramps.
	snowRampX = brick_snow_bottom_middle_data;
	snowRampY = brick_snow_middle_right_data;
};

datablock fxDTSBrickData (brick_snow_top_left_bottom_right_data)
{
	brickFile          = "./brickFiles/topLeftBottomRight.blb";
	collisionShapeName = "./collisionFiles/topLeftBottomRight.dts";
	category           = "Building Snow";
	subcategory        = "Snow";
	uiName             = "Top Left Bottom Right";

	isSnowBrick   = true;
	snowBrickType = "doubleRamp";
	snowVertices  = "0 1 1 0";
};

datablock fxDTSBrickData (brick_snow_top_right_bottom_left_data)
{
	brickFile          = "./brickFiles/topRightBottomLeft.blb";
	collisionShapeName = "./collisionFiles/topRightBottomLeft.dts";
	category           = "Building Snow";
	subcategory        = "Snow";
	uiName             = "Top Right Bottom Left";

	isSnowBrick   = true;
	snowBrickType = "doubleRamp";
	snowVertices  = "1 0 0 1";
};
