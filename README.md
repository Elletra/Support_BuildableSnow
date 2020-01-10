# Support_BuildableSnow
> Modifiable terrain-like snow.

## <a name="how-it-works"></a>How It Works

Basically, it maintains a grid of vertices and a grid of bricks.  Four vertices make up a "tile".  The "tiles" in this case are bricks.

Each brick has four vertices:
  1. Top left corner of the brick.
  2. Top right corner of the brick.
  3. Bottom left corner of the brick.
  4. Bottom right corner of the brick.

Each vertex has only two heights: `0` or `1`.

Each snow brick datablock corresponds to a certain configuration of four vertex heights.

Any time a brick is updated, it checks its four vertex heights and attempts to set its
datablock to the appropriate one.  If it successfully changes its datablock, it updates
its immediate neighbors.

See the source code for [`fxDTSBrick::setSnowVertices()`](https://github.com/Electrk/Support_BuildableSnow/blob/master/snowVertices.cs) and [`fxDTSBrick::updateSnow()`](https://github.com/Electrk/Support_BuildableSnow/blob/master/updateSnow.cs) to see how this works.

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

**Returns**  [`BuildableSnowError`](#error-handling)

##

#### <a name="api-lower-snow"></a>`fxDTSBrick::lowerSnow ();`

Lowers snow, provided there's no snow above this brick.

**Returns**  [`BuildableSnowError`](#error-handling)

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

**Returns**  [`BuildableSnowError`](#error-handling)

##

#### <a name="api-get-snow-vertices"></a> `fxDTSBrick::getSnowVertices ();`

Gets the actual snow vertex data.

Since the brick's datablock does not always correspond with the actual vertex data for aesthetic purposes, we want to be able to accurately get the vertex data.

**Returns**  `BuildableSnowVertices` or `null`

A string with the vertices separated by a space in this order: top left, top right, bottom left, bottom right.

Or if there was an error, it will return an empty string (null).

##

#### <a name="api-update-snow"></a> `fxDTSBrick::updateSnow ();`

Updates snow brick datablock and, if needed, surrounding neighbors.

**Returns**  [`BuildableSnowError`](#error-handling)

##

#### <a name="api-update-snow-neighbors"></a> `fxDTSBrick::updateSnowNeighbors ();`

Updates adjacent neighbor snow bricks.

**Returns**  [`BuildableSnowError`](#error-handling)

##

#### <a name="api-can-update-snow"></a> `fxDTSBrick::canUpdateSnow ();`

Whether or not this snow brick can change into its appropriate datablock.

**Returns**  `boolean`

##

#### <a name="api-get-snow-update-datablock"></a> `fxDTSBrick::getSnowUpdateDataBlock ();`

Determines the datablock this snow brick is going to be when it updates.

**Returns**  `fxDTSBrickData` or `-1`

Returns -1 if there was an error.

##

#### <a name="api-get-brick"></a> `BuildableSnow_GetBrick (x, y, z);`

Gets a brick at grid (x, y, z).

| Argument | Type |  Description  |
| -------- | ---- | ------------- |
| x | integer | Grid coordinate X. |
| y | integer | Grid coordinate Y. |
| z | integer | Grid coordinate Z. |

**Returns**  `fxDTSBrick` or `-1`

If a brick cannot be found, it returns `-1`.

##

#### <a name="api-grid-to-world"></a> `BuildableSnow_GridToWorld (x, y, z);`

Converts a grid position to an actual world position.

| Argument | Type |  Description  |
| -------- | ---- | ------------- |
| x | integer | Grid coordinate X. |
| y | integer | Grid coordinate Y. |
| z | integer | Grid coordinate Z. |

**Returns**  `Vector3D`

##

#### <a name="api-is-valid-grid-pos"></a> `BuildableSnow_isValidGridPos (x, y, z);`

Checks whether (x, y, z) is a valid grid position.

| Argument | Type |  Description  |
| -------- | ---- | ------------- |
| x | integer | Grid coordinate X. |
| y | integer | Grid coordinate Y. |
| z | integer | Grid coordinate Z. |

**Returns**  `boolean`

##

#### <a name="api-get-snow-neighbor"></a> `fxDTSBrick::getSnowNeighbor (x, y, z);`

Gets the brick at the grid position (x, y, z) relative to this brick, if any.

| Argument | Type |  Description  |
| -------- | ---- | ------------- |
| x | integer | Grid coordinate X relative to this brick. |
| y | integer | Grid coordinate Y relative to this brick. |
| z | integer | Grid coordinate Z relative to this brick. |

**Returns**  `fxDTSBrick` or `-1`

If a brick cannot be found, it returns `-1`.

##

#### <a name="api-has-snow-neighbor"></a> `fxDTSBrick::hasSnowNeighbor (x, y, z);`

Whether or not there's a brick at the grid position (x, y, z) relative to this brick.

| Argument | Type |  Description  |
| -------- | ---- | ------------- |
| x | integer | Grid coordinate X relative to this brick. |
| y | integer | Grid coordinate Y relative to this brick. |
| z | integer | Grid coordinate Z relative to this brick. |

**Returns**  `boolean`

##

#### <a name="api-has-empty-snow-spot"></a> `fxDTSBrick::hasEmptySnowSpot (x, y, z);`

Whether or not there's an empty spot at the grid position (x, y, z) relative to this brick.  "Empty spot" meaning either an empty snow brick, or the lack of a brick.

| Argument | Type |  Description  |
| -------- | ---- | ------------- |
| x | integer | Grid coordinate X relative to this brick. |
| y | integer | Grid coordinate Y relative to this brick. |
| z | integer | Grid coordinate Z relative to this brick. |

**Returns**  `boolean`

## <a name="error-handling">Error Handling

Some functions return a `BuildableSnowError`, which will be one of the following:

| Variable | Description |
| -------- | ----------- |
| $BuildableSnow::Error::None | There was no error and the operation was successful. |
| $BuildableSnow::Error::Generic | Reserved for possible future use.  Currently unused. |
| $BuildableSnow::Error::NotSnowBrick | The brick we're trying to operate on is not a snow brick. |
| $BuildableSnow::Error::NotInGrid | The brick we're trying to operate on is not in the snow grid. |
| $BuildableSnow::Error::HasSnowAbove | The brick we're trying to operate on has snow above it. |
| $BuildableSnow::Error::NoSnowBelow | The brick we're trying to operate on doesn't have the supporting snow required. |
| $BuildableSnow::Error::InvalidGridPos | The grid position we tried to use is invalid. |
| $BuildableSnow::Error::InvalidDataBlock | The datablock we tried to use is invalid/nonexistent. |

## <a name="configurable-variables">Configurable Variables

This mod was written to be as flexible as possible.  As such, there are many global variables that you can change if you so wish.

If you want to change the snow brick's brick group, color, or angle ID:

| Variable | Description | Default Value |
| -------- | ----------- | ------------- |
| $BuildableSnow::SnowBrickGroup | The brick group to add all snow bricks to. | BrickGroup_888888 |
| $BuildableSnow::SnowColorID | The color ID to set all snow bricks to. | The closest color to "1 1 1 1" |
| $BuildableSnow::SnowAngleID | The angle ID to create all snow bricks with. | 0 |

##

If you want to change the tick rates for async grid creation/destruction:

| Variable | Description | Default Value |
| -------- | ----------- | ------------- |
| $BuildableSnow::CreateGridTickRate | The tick rate of async grid creation (in milliseconds). | 0 |
| $BuildableSnow::DestroyGridTickRate | The tick rate of async grid destruction (in milliseconds). | 0 |

## <a name="custom-bricks">Custom Bricks

If you want to make custom bricks for this mod, you'll have to make 16 bricks of all the same size.  They don't have to be the same size as the default ones, but they do have to be the same size as each other.

![](https://i.imgur.com/0Ae57QH.png)

These are the 16 required brick datablocks.  You'll notice that #16 is above an empty stud.  This is the empty brick, which is what bricks get set to when all four vertex heights are `0`.

Since Blockland has built-in, hardcoded effects when planting bricks, we have to use `setDataBlock()` for smooth, instant terrain sculpting.  This empty brick **_must be the same size as the rest of the bricks_**.

![](https://i.imgur.com/JEOVhtM.png)

The vertex data for each of the bricks.  Blue means a vertex height of `0` and red means a vertex height of `1`.

![](https://i.imgur.com/fmb9Kn5.png)

The names of all the bricks.  Note the direction of north.

##

Once you have your bricks made, you'll have to set these in your mod to replace the default bricks:

| Variable | Description | Default Value |
| -------- | ----------- | ------------- |
| $BuildableSnow::DataBlock_* | Get a datablock from a certain configuration of vertices.  See [`config.cs`](https://github.com/Electrk/Support_BuildableSnow/blob/master/config.cs) for all of them. | See [`config.cs`](https://github.com/Electrk/Support_BuildableSnow/blob/master/config.cs). |
| $BuildableSnow::CornerToAdapter_* | A quick way to get a respective adapter brick from corner vertices.  See [`config.cs`](https://github.com/Electrk/Support_BuildableSnow/blob/master/config.cs) for all of them. | See [`config.cs`](https://github.com/Electrk/Support_BuildableSnow/blob/master/config.cs). |
| $BuildableSnow::DefaultDataBlock | The default datablock to create snow bricks as. | brick_snow_middle_middle_data |

And set the following properties for each of the datablocks:

| Property | Type | Description |
| -------- | ---- | ----------- |
| isSnowBrick | boolean | Whether or not this is a snow brick datablock (set this to `true`). |
| snowBrickType | string | The type of snow brick this datablock is. |
| snowVertices | BuildableSnowVertices | Four vertex heights that correspond to this datablock. |

Here are the six snow brick types:

| Type | Bricks of this type |
| ---- | ------ |
| empty | Empty snow brick. |
| middle | Middle snow brick. |
| corner | Top left, top right, bottom left, and bottom right snow bricks. |
| ramp | Top middle, middle left, middle right, and bottom middle snow bricks. |
| adapter | Top left adapter, top right adapter, bottom left adapter, and bottom right adapter snow bricks. |
| doubleRamp | "Top left bottom right" and "top right bottom left" snow bricks. |

Also be sure to leave blank `category` and `subcategory` properties, as players are not meant to plant these bricks themselves.

#

#### <a name="custom-bricks-version-checking">Version Checking

If your mod requires a certain minimum version of this mod to work, you can use the `BuildableSnow_RequireMinVersion()` function:

#### <a name="custom-bricks-version-checking-require-min-version"></a> `BuildableSnow_RequireMinVersion (minVersion);`

For use by other mods that require a minimum version of this mod to work.

| Argument | Type |  Description  |
| -------- | ---- | ------------- |
| minVersion | SemanticVersion | A semantic version string of the minimum version of this mod required for your mod to work. |

**Returns**  `boolean`

Whether this mod is at least the minimum version required.

#

#### <a name="custom-bricks-debug-mode">Debug Mode

If you want to enable debug mode and see error messages:

| Variable | Description | Default Value |
| -------- | ----------- | ------------- |
| $BuildableSnow::DebugMode | Whether or not to print debug messages. | false |
