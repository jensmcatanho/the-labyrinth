using System.Collections;
using UnityEngine;

public class DFSMazeFactory {
	const float _probabilityOfDeadEnds = 0.1f;

	const float _probabilityOfChests = 0.5f;

    public Maze<Cell> CreateMaze(int length, int width, int cellSize) {
		Maze<DFSCell> dfsMaze = new Maze<DFSCell>(length, width, cellSize);

		for (int row = 0; row < length; row++)
			for (int col = 0; col < width; col++)
				dfsMaze[row, col] = new DFSCell(row, col, cellSize);

		CreatePath(dfsMaze);
		CreateChests(dfsMaze);

		Maze<Cell> maze = new Maze<Cell>(length, width, cellSize);
		maze.Entrance = dfsMaze.Entrance;
		maze.Exit = dfsMaze.Exit;

		for (int row = 0; row < maze.Length; row++)
			for (int col = 0; col < maze.Width; col++)
				maze[row, col] = dfsMaze[row, col];

		return maze;
	}

	void CreatePath(Maze<DFSCell> maze) {
		ArrayList history = new ArrayList();
		ArrayList neighbors = new ArrayList();

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
				char direction = System.Convert.ToChar(neighbors[Random.Range(0, neighbors.Count)]);

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
				row = (int)retrace.x;
				col = (int)retrace.y;

				history.RemoveAt(history.Count - 1);
			}

			// 6. If there are still cells in the history list, go back to step 3.
		}

		// 7. Open an entrance and a exit to the maze.
		maze.Entrance = maze[0, 0];
		maze.Entrance.SetType(CellType.Entrance);
		maze[0, 0].ToggleWall(Wall.Left);

		Vector2 exitPosition = new Vector2(Random.Range(maze.Length * 0.5f, maze.Length), Random.Range(maze.Width * 0.5f, maze.Width));
		if (exitPosition.x > exitPosition.y) {
			exitPosition.y = maze.Width - 1;
			maze[(int)exitPosition.x, (int)exitPosition.y].ToggleWall(Wall.Right);

		} else {
			exitPosition.x = maze.Length - 1;
			maze[(int)exitPosition.x, (int)exitPosition.y].ToggleWall(Wall.Down);
		}

		maze.Exit = maze[(int)exitPosition.x, (int)exitPosition.y];
		maze.Exit.SetType(CellType.Exit);
	}

	void CreateChests(Maze<DFSCell> maze) {
		for (int row = 0; row < maze.Length; row++)
			for (int col = 0; col < maze.Width; col++)
				if (maze[row, col].IsDeadEnd() && Random.value < _probabilityOfChests)
					maze[row, col].HasChest = true;
	}

	/*
	protected void ChestSetup() {
		 *  Equation 1: nC = (l * w) * dE * pC
		 *  Equation 2: nC = (l * w) * 0.05
		 * 
		 *  (l * w) * 0.05 = (l * w) * dE * pC =>
		 *  dE * pC = 0.05
		 * 
		 *  l = maze's length
		 *  w = maze's width
		 *  nC = maximum number of chests
		 *  dE = percentage of dead ends
		 *  pC = probability of chest spawning
		 * 
		 

		 pChest = 0.05 / pDeadEnd
		_probabilityOfDeadEnds = 0.1f;
		_probabilityOfChests = 0.5f;
	}
	*/
}