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

Flattens brick if it's not already; raises snow brick above it if it is.  Returns a [BuildableSnowError](#errors).

##

#### <a name="api-lower-snow"></a>`fxDTSBrick::lowerSnow ();`

Lowers snow, provided there's no snow above this brick.  Returns a [BuildableSnowError](#errors).

## <a name="errors">Errors

Some functions return a `BuildableSnowError`, which will be one of the following:

| Variable | Description |
| -------- | ----------- |
| $BuildableSnow::Error::None | There was no error and the operation was successful. |
| $BuildableSnow::Error::Generic | Reserved for possible future use.  Currently unused. |
| $BuildableSnow::Error::NotSnowBrick | The brick we're trying to operate on is not a snow brick. |
| $BuildableSnow::Error::HasSnowAbove | The brick we're trying to operate on has snow above it. |
| $BuildableSnow::Error::NoSnowBelow | The brick we're trying to operate on doesn't have the supporting snow required. |
