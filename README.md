# Rpg-Demo
This Is a 10x10 grid based rpg demo with 3d isometric view.
## Features

- 10x10 Grid
- Obstacle Generator

### 10x10 Grid
The Grid is made of 10 by 10 unity cubes. each cude has a [script](RpgProject/Assets/Scripts/TileInfo.cs) which holds the detail of the cube. It checks the mouse position on screen and with [raycast](RpgProject/Assets/Scripts/RaycastManager.cs) checks the block and updates the UI with block location.
### Obstacle Generator
[Obstacle Generator](RpgProject/Assets/Scripts/ObstacleGenerator.cs) is a unity window editor tool. It has 10x10 toggle buttons for obstacle info. On Generate obstacle it stores the obstacle data to a [scriptable object](RpgProject/Assets/Scripts/ObstacleScriptableObject.cs) specified in the tool.
[Obstacle Manager Script](RpgProject/Assets/Scripts/ObstacleManager.cs) uses this object to generate obstacle at runtime. 
