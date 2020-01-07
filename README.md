# Support_BuildableSnow
> Modifiable terrain-like snow.

## <a name="api"></a>API

#### <a name="api-create-grid"></a>`BuildableSnow_CreateGrid (width, length, height[, useAsync[, asyncCallback]]);`

Creates the WxLxH grid of snow bricks, either synchronously or asynchronously.

| Argument | Type |  Description  | Default Value |
| -------- | ---- | ------------- | ------------- |
| width  | integer | | | |
| length | integer | | | |
| height | integer | | | |
| useAsync | boolean | _(Optional)_  Use async method of creating the grid using schedules.  Calls `asyncCallback` when finished. | false |
| asyncCallback | string | _(Optional)_  Function to call when grid creation is done. | |

##

#### <a name="api-destroy-grid"></a>`BuildableSnow_DestroyGrid ([useAsync[, asyncCallback]]);`

Destroys the snow grid, either synchronously or asynchronously.

| Argument | Type |  Description  | Default Value |
| -------- | ---- | ------------- | ------------- |
| useAsync | boolean | _(Optional)_  Use async method of destroying the grid using schedules.  Calls `asyncCallback` when finished. | false |
| asyncCallback | string | _(Optional)_  Function to call when grid destruction is done. | |

##

#### <a name="api-raise-snow"></a>`fxDTSBrick::raiseSnow ();`

Flattens brick if it's not already; raises snow brick above if it is.

**Returns**  `boolean`

Whether or not the operation was successful.  Use [`$BuildableSnow::LastError`](#error-handling) to check for errors.

##

#### <a name="api-lower-snow"></a>`fxDTSBrick::lowerSnow ();`

Lowers snow, provided there's no snow above this brick.

**Returns**  `boolean`

Whether or not the operation was successful.  Use [`$BuildableSnow::LastError`](#error-handling) to check for errors.

##

#### <a name="api-set-snow-vertices"></a> `fxDTSBrick::setSnowVertices (topLeft, topRight, bottomLeft, bottomRight);`

Sets the brick's snow vertex data.

Please note that this does not update the brick's datablock.  You'll normally want to use this function in conjunction with [`fxDTSBrick::updateSnow()`]("#api-update-snow") afterward.

| Argument | Type |  Description  |
| -------- | ---- | ------------- |
| topLeft | boolean | Height of the brick's top left vertex (only supports 0 or 1). |
| topRight | boolean | Height of the brick's top right vertex (only supports 0 or 1). |
| bottomLeft | boolean | Height of the brick's bottom left vertex (only supports 0 or 1). |
| bottomRight | boolean | Height of the brick's bottom right vertex (only supports 0 or 1). |

**Returns**  `boolean`

Whether or not the operation was successful.  Use [`$BuildableSnow::LastError`](#error-handling) to check for errors.

##

#### <a name="api-get-snow-vertices"></a> `fxDTSBrick::getSnowVertices ();`

Gets the actual snow vertex data.

Since the brick's datablock does not always correspond with the actual vertex data for aesthetic purposes, we want to be able to accurately get the vertex data.

**Returns**  `BuildableSnowVertices` or `null`

A string with the vertices separated by a space in this order: top left, top right, bottom left, bottom right.

Or if there was an error, it will return an empty string (null).  Use [`$BuildableSnow::LastError`](#error-handling) to check for errors.

##

#### <a name="api-update-snow"></a> `fxDTSBrick::updateSnow ();`

Updates snow brick datablock and, if needed, surrounding neighbors.

**Returns**  `boolean`

Whether or not the operation was successful.  Use [`$BuildableSnow::LastError`](#error-handling) to check for errors.

##

#### <a name="api-update-snow-neighbors"></a> `fxDTSBrick::updateSnowNeighbors ();`

Updates adjacent neighbor snow bricks.

**Returns**  `boolean`

Whether or not the operation was successful.  Use [`$BuildableSnow::LastError`](#error-handling) to check for errors.

## <a name="error-handling">Error Handling

If a function returns a value indicating some sort of issue (usually `false`, an empty string, or `-1`), you should check `$BuildableSnow::LastError` to see what error occurred, if any.

Here are all the errors:

| Variable | Description |
| -------- | ----------- |
| $BuildableSnow::Error::None | There was no error and the operation was successful. |
| $BuildableSnow::Error::Generic | Reserved for possible future use.  Currently unused. |
| $BuildableSnow::Error::NotSnowBrick | The brick we're trying to operate on is not a snow brick. |
| $BuildableSnow::Error::HasSnowAbove | The brick we're trying to operate on has snow above it. |
| $BuildableSnow::Error::NoSnowBelow | The brick we're trying to operate on doesn't have the supporting snow required. |
| $BuildableSnow::Error::InvalidGridPos | Attempted to use an invalid grid position. |
| $BuildableSnow::Error::BrickNotFound | No brick was found at this grid position. |
| $BuildableSnow::Error::CreateBrick | There was an error with `createBrick()` — check `$CreateBrick::LastError` |

This add-on uses `Support_CreateBrick.cs`, which has its own errors with `$CreateBrick::LastError`:

| Variable | Description |
| -------- | ----------- |
| $CreateBrick::Error::None | There was no error and brick creation was successful. |
| $CreateBrick::Error::PlantOverlap | "Overlap" plant error. |
| $CreateBrick::Error::PlantFloat | "Float" plant error. |
| $CreateBrick::Error::PlantStuck | "Stuck" plant error. |
| $CreateBrick::Error::PlantUnstable | "Unstable" plant error. |
| $CreateBrick::Error::PlantBuried | "Buried" plant error. |
| $CreateBrick::Error::Generic | There was some miscellaneous/unspecified error creating the brick. |
| $CreateBrick::Error::DataBlock | Attempted to create a brick with an invalid/nonexistent datablock. |
| $CreateBrick::Error::AngleID | Attempted to create a brick with an invalid angle ID. |
| $CreateBrick::Error::BrickGroup | Attempted to create a brick with a nonexistent brick group. |
