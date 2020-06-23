using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMazeFactory {
    Maze<Cell> CreateMaze(int length, int width, int cellSize);
}
