﻿using System;
using System.Collections;
using System.Numerics;

public static class DFSMazeFactory {

	#region private variables

	private static readonly float _probabilityOfChests = 0.5f;
    #endregion

    #region public methods
    public static Maze<Cell> CreateMaze(int length, int width, int cellSize) {
		Maze<DFSCell> dfsMaze = new Maze<DFSCell>(length, width, cellSize);

		for (int row = 0; row < length; row++)
			for (int col = 0; col < width; col++)
				dfsMaze[row, col] = new DFSCell(row, col, cellSize);

		CreatePath(dfsMaze);
		CreateChests(dfsMaze);
		CreateEntrance(dfsMaze);
		CreateExit(dfsMaze);

		return DFSMazeToMaze(dfsMaze);
	}
    #endregion

    #region private methods
    private static void CreatePath(Maze<DFSCell> maze) {
		ArrayList history = new ArrayList();
		ArrayList neighbors = new ArrayList();
		Random rand = new Random();

		// 1. Start in the first cell.
		int row = 0;
		int col = 0;

		// 2. Add it to the history stack.
		history.Add(new Vector2(row, col));

		while (history.Count > 0) {
			// 3. Mark it as visited.
			maze[row, col].IsVisited = true;

			// 4. Check which of its neighbors were not yet visited.
			neighbors.Clear();
			if (col > 0 && !maze[row, col - 1].IsVisited)
				neighbors.Add('L');

			if (row > 0 && !maze[row - 1, col].IsVisited)
				neighbors.Add('U');

			if (col < maze.Length - 1 && !maze[row, col + 1].IsVisited)
				neighbors.Add('R');

			if (row < maze.Width - 1 && !maze[row + 1, col].IsVisited)
				neighbors.Add('D');

			// 5a. If there is a neighbor not yet visited, choose one randomly to connect to the current cell. 
			if (neighbors.Count > 0) {
				history.Add(new Vector2(row, col));
				char direction = Convert.ToChar(neighbors[rand.Next(0, neighbors.Count)]);

				switch (direction) {
					case 'L':
						maze[row, col].ToggleWall(Wall.Left);
						maze[row, --col].ToggleWall(Wall.Right);
						break;

					case 'U':
						maze[row, col].ToggleWall(Wall.Up);
						maze[--row, col].ToggleWall(Wall.Down);
						break;

					case 'R':
						maze[row, col].ToggleWall(Wall.Right);
						maze[row, ++col].ToggleWall(Wall.Left);
						break;

					case 'D':
						maze[row, col].ToggleWall(Wall.Down);
						maze[++row, col].ToggleWall(Wall.Up);
						break;
				}

			} else {
				// 5b. If there isn't a neighbor to visit, backtrack one step.
				Vector2 retrace = (Vector2)history[history.Count - 1];
				row = (int)retrace.X;
				col = (int)retrace.Y;

				history.RemoveAt(history.Count - 1);
			}

			// 6. If there are still cells in the history list, go back to step 3.
		}
	}

	private static void CreateChests(Maze<DFSCell> maze) {
		Random rand = new Random();

		for (int row = 0; row < maze.Length; row++)
			for (int col = 0; col < maze.Width; col++)
				if (maze[row, col].IsDeadEnd() && rand.NextDouble() < _probabilityOfChests)
					maze[row, col].HasChest = true;
	}

	private static void CreateEntrance(Maze<DFSCell> maze) {
		maze.Entrance = maze[0, 0];
		maze[0, 0].ToggleWall(Wall.Left);
	}

	private static void CreateExit(Maze<DFSCell> maze) {
		Random rand = new Random();

		Vector2 exitPosition = new Vector2(rand.Range(maze.Length * 0.5f, maze.Length), rand.Range(maze.Width * 0.5f, maze.Width));
		
		if (exitPosition.X > exitPosition.Y) {
			exitPosition.Y = maze.Width - 1;
			maze[(int)exitPosition.X, (int)exitPosition.Y].ToggleWall(Wall.Right);

		} else {
			exitPosition.X = maze.Length - 1;
			maze[(int)exitPosition.X, (int)exitPosition.Y].ToggleWall(Wall.Down);
		}

		maze.Exit = maze[(int)exitPosition.X, (int)exitPosition.Y];
	}

	private static Maze<Cell> DFSMazeToMaze(Maze<DFSCell> dfsMaze) {
		Maze<Cell> maze = new Maze<Cell>(dfsMaze.Length, dfsMaze.Width, dfsMaze.CellSize);
		maze.Entrance = dfsMaze.Entrance;
		maze.Exit = dfsMaze.Exit;

		for (int row = 0; row < maze.Length; row++)
			for (int col = 0; col < maze.Width; col++)
				maze[row, col] = dfsMaze[row, col];

		return maze;
	}
#endregion
}