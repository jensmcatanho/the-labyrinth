using System;
using System.Collections;
using System.Numerics;
using UnityEditor.Experimental.GraphView;

public class PrimMazeFactory : IMazeFactory {

    #region private variables
    private Maze<PrimCell> _maze;

    private readonly float _probabilityOfChests = 0.14f;

    private readonly ArrayList _frontier = new ArrayList();

    private static readonly Random random = new Random();
    #endregion

    #region public methods
    public Maze<Cell> CreateMaze(int length, int width, int cellSize) {
        _maze = new Maze<PrimCell>(length, width, cellSize);

        for (int row = 0; row < length; row++)
            for (int col = 0; col < width; col++)
                _maze[row, col] = new PrimCell(row, col, cellSize);
        
        CreatePath();
        CreateChests();
        CreateEntrance();
        CreateExit();

        return PrimMazeToMaze();
    }
    #endregion

    #region private methods
    private void CreatePath() {
        PrimCell currentCell = GetRandomCell();
        MarkAsVisited(currentCell);

        do {
            AddNeighborsToFrontier(currentCell);

            currentCell = GetCellFromFrontier();
            MarkAsVisited(currentCell);

            Wall neighborDirection = GetRandomVisitedNeighbor(currentCell);
            ConnectToNeighborCell(currentCell, neighborDirection);

        } while (_frontier.Count > 0);
    }

    private PrimCell GetRandomCell() {
        return _maze[random.Next(0, _maze.Length), random.Next(0, _maze.Width)];
    }

    private void AddNeighborsToFrontier(PrimCell cell) {
        int row = (int)cell.Position.X;
        int col = (int)cell.Position.Y;

        MarkAsNeighbor(_maze[row, col - 1]);
        MarkAsNeighbor(_maze[row - 1, col]);
        MarkAsNeighbor(_maze[row, col + 1]);
        MarkAsNeighbor(_maze[row + 1, col]);
    }

    private void MarkAsNeighbor(PrimCell cell) {
        if (cell?.IsNone() ?? false) {
            _frontier.Add(cell.Position);
            MarkStatus(cell, PrimStatus.Neighbor);
        }
    }

    private PrimCell GetCellFromFrontier() {
        int index = random.Next(0, _frontier.Count);

        Vector2 nextCell = (Vector2)_frontier[index];
        _frontier.RemoveAt(index);

        return _maze[(int)nextCell.X, (int)nextCell.Y];
    }

    private void MarkAsVisited(PrimCell cell) {
        MarkStatus(cell, PrimStatus.Visited);
    }

    private void MarkStatus(PrimCell cell, PrimStatus status) {
        cell.Status = status;
    }

    private Wall GetRandomVisitedNeighbor(PrimCell cell) {
        ArrayList neighbors = new ArrayList();

        int row = (int)cell.Position.X;
        int col = (int)cell.Position.Y;

        if (IsVisited(row, col - 1))
            neighbors.Add(Wall.Left);

        if (IsVisited(row - 1, col))
            neighbors.Add(Wall.Up);

        if (IsVisited(row, col + 1))
            neighbors.Add(Wall.Right);

        if (IsVisited(row + 1, col))
            neighbors.Add(Wall.Down);

        return (Wall)neighbors[random.Next(0, neighbors.Count)];
    }

    private bool IsVisited(int row, int col) {
        return _maze[row, col]?.IsVisited() ?? false;
    }

    private void ConnectToNeighborCell(PrimCell cell, Wall direction) {
        int row = (int)cell.Position.X;
        int col = (int)cell.Position.Y;

        switch (direction) {
		    case Wall.Left:
                _maze[row, col].ToggleWall(Wall.Left);
                _maze[row, col - 1].ToggleWall(Wall.Right);
			    break;
			
		    case Wall.Up:
                _maze[row, col].ToggleWall(Wall.Up);
                _maze[row - 1, col].ToggleWall(Wall.Down);
			    break;

		    case Wall.Right:
                _maze[row, col].ToggleWall(Wall.Right);
                _maze[row, col + 1].ToggleWall(Wall.Left);
			    break;

		    case Wall.Down:
                _maze[row, col].ToggleWall(Wall.Down);
                _maze[row + 1, col].ToggleWall(Wall.Up);
			    break;
	        }
    }

    private void CreateChests() {
        for (int row = 0; row < _maze.Length; row++)
            for (int col = 0; col < _maze.Width; col++)
                if (_maze[row, col].IsDeadEnd() && random.NextDouble() < _probabilityOfChests)
                    _maze[row, col].HasChest = true;
    }

    private void CreateEntrance() {
        _maze.Entrance = _maze[0, 0];
        _maze[0, 0].ToggleWall(Wall.Left);
    }

    private void CreateExit() {
        Vector2 exitPosition = new Vector2(random.Range(_maze.Length * 0.5f, _maze.Length), random.Range(_maze.Width * 0.5f, _maze.Width));

        if (exitPosition.X > exitPosition.Y) {
            exitPosition.Y = _maze.Width - 1;
            _maze[(int)exitPosition.X, (int)exitPosition.Y].ToggleWall(Wall.Right);

        } else {
            exitPosition.X = _maze.Length - 1;
            _maze[(int)exitPosition.X, (int)exitPosition.Y].ToggleWall(Wall.Down);
        }

        _maze.Exit = _maze[(int)exitPosition.X, (int)exitPosition.Y];
    }

    private Maze<Cell> PrimMazeToMaze() {
        Maze<Cell> maze = new Maze<Cell>(_maze.Length, _maze.Width, _maze.CellSize);

        for (int row = 0; row < maze.Length; row++)
            for (int col = 0; col < maze.Width; col++) {
                maze[row, col] = new Cell(row, col, _maze.CellSize);
                maze[row, col].UnsetAllWalls();
                maze[row, col].SetWall(_maze[row, col].Walls);
            }

        maze.Entrance = maze[(int)_maze.Entrance.Position.X, (int)_maze.Entrance.Position.Y];
        maze.Exit = maze[(int)_maze.Exit.Position.X, (int)_maze.Exit.Position.Y];

        return maze;
    }
    #endregion
}
