# Creating Stage Definitions

Stage definitions are stored in text files which are then assigned to the StageManager object.

### Metadata

The first few lines of the text file are used to set the stage's metadata, as follows:

Line 1: number
Line 2: name

Example:
2
Fun With Rocks

### Tiles

Tiles are what the character walks on, and they make up the stages of the game. Many tiles have special properties.

The stage's tiles are defined after the metadata with each line in the text file representing one row of tiles in 2D space. Each tile is represented by a tile identifier which is a unique character or sequence, and is separated from the next tile identifier by a comma.

##### Tile Definitions

Format is **TILE_NAME[TILE_IDENTIFIER]**: Description of what the tile does in the game.

* **Dirt[o]**: The most basic, harmless tile. No effect.
* **Water[w]**: Causes death if moved into. If a boulder is pushed into a water tile, the water tile turns into Dirty Water, which shares the same behavior as Dirt but has a different appearance.
* **Spikes[s]**: Causes death if moved into.

Example:
2
Fun With Rocks
o,o,w,o,o
o,o,w,o,o
o,o,w,o,o

This example would form a 5x3 tile grid with 2 columns of dirt on the left, a column of water in the middle, and 2 more columns of dirt on the right.

### Doodads

Doodads are any object that is placed on top of a tile, or otherwise attached to a tile. These always have special properties.

Each doodad is represented by a doodad identifier which is a unique character or sequence, and is separated from the next doodad identifier by a comma. A tile's attached doodads are defined by placing them in parentheses after the tile identifier, such as d(b,x)

##### Tile Definitions

Format is **DOODAD_NAME[DOODAD_IDENTIFIER]**: Description of what the doodad does in the game.

* **Entrance[e]**: The tile where the character will be located at the beginning of a stage. There must be exactly one entrance per stage.
* **Boulder[b]**: A pushable object. When a character moves onto a tile with a boulder, the boulder is pushed off into the next tile. A boulder may be pushed into water to turn it into dirt.
* **Exit[x]**: If the character reaches this tile, they have completed the stage.

Example:
2
Fun With Rocks
o,o(b),w,o,o
o(e),o,w,o,o(x)
o,o,w,o,o

This example would form a 5x3 tile grid with 2 columns of dirt on the left, a column of water in the middle, and 2 more columns of dirt on the right. The starting tile would be at position [0,1] and the exit tile would be at position [4,1]. A boulder would be located at position [1,0]. This example is now a complete stage file that would work in the game.