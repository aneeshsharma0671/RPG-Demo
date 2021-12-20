# Rpg-Demo
This Is a 10x10 grid based rpg demo with 3d isometric view. Uses BFS pathfinding to detect path avoiding the obstacles.
## Features

- 10x10 Grid
- Obstacle Generator
- Path Finding

### 10x10 Grid

<p align="center">
  <img src="https://drive.google.com/uc?export=view&id=10d2VSIW-WZvxm4vuLG-ZriGQLId8-Pm_">
</p>

The Grid is made of 10 by 10 unity cubes. each cude has a [script](RpgProject/Assets/Scripts/TileInfo.cs) which holds the detail of the cube. It checks the mouse position on screen and with [raycast](RpgProject/Assets/Scripts/RaycastManager.cs) checks the block and updates the UI with block location.
### Obstacle Generator

<p align="center">
  <img src="https://drive.google.com/uc?export=view&id=11jm1ehCfDiS73xkRoZoN3SENYBH9r_0L">
</p>

[Obstacle Generator](RpgProject/Assets/Scripts/ObstacleGenerator.cs) is a unity window editor tool. It has 10x10 toggle buttons for obstacle info. On Generate obstacle it stores the obstacle data to a [scriptable object](RpgProject/Assets/Scripts/ObstacleScriptableObject.cs) specified in the tool.
[Obstacle Manager Script](RpgProject/Assets/Scripts/ObstacleManager.cs) uses this object to generate obstacle at runtime. 

### Pathfinding

Breadth first search is used to find the path to target location avoiding the obstacles. 



https://user-images.githubusercontent.com/54682356/146807004-76defda7-ab54-490d-b7ab-758fe87549c1.mp4

