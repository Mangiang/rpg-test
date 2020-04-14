# RPG-Test

Playground for testing and making a turned based RPG.

## The level editor

### Open the Level Editor

1. Open unity
1. Open the scene `SampleScene`
1. In the Unity menu bar `Window` -> `LevelEditor`
1. Playaround with it

### Define a walkable area

1. Add a 3D object to the scene
1. Add a `Grid Object` component on it
1. Make it `walkable` in the inspector view while it is selected
1. Update the level using the `Level Editor` window

### Define an Obstacle

1. Add a 3D object to the scene
1. Add a `Grid Object` component on it
1. Make it `walkable` or not in the inspector view while it is selected
1. Update the level using the `Level Editor` window

### Define a Wall

Same as an obstacle but you need the object to be smaller than the detection of the cells (red cubes)
