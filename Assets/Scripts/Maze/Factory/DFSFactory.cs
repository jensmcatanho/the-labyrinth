using System;
using System.Collections;
using System.Numerics;

namespace Maze.Factory {

public class DFSFactory : IMazeFactory {

    #region private fields
    private Maze<DFSCell> _maze;

    private readonly float _probabilityOfChests = 0.5f;

    private readonly ArrayList _history = new ArrayList();

    private static readonly Random random = new Random();
    #endregion

    #region public methods
    public Maze<Cell> CreateMaze(int length, int width, int cellSize) {
        _maze = new Maze<DFSCell>(length, width, cellSize);

        for (int row = 0; row < length; row++)
            for (int col = 0; col < width; col++)
                _maze[row, col] = new DFSCell(row, col, cellSize);

        CreatePath();
        CreateChests();
        CreateEntrance();
        CreateExit();

        return DFSMazeToMaze();
    }
    #endregion

    #region private methods

    private void CreatePath() {
        DFSCell currentCell = _maze[0, 0];
        _history.Add(currentCell);

        while (_history.Count > 0) {
            MarkAsVisited(currentCell);
            Direction neighborDirection = GetNeighborNotVisited(currentCell);

            if (neighborDirection != Direction.None) {
                _history.Add(currentCell);
                currentCell = ConnectToNeighborCell(currentCell, neighborDirection);

            } else {
                currentCell = BacktrackCurrentCell();
            }
        }
    }

    private void MarkAsVisited(DFSCell cell) {
        cell.IsVisited = true;
    }

    private Direction GetNeighborNotVisited(DFSCell cell) {
        ArrayList neighbors = new ArrayList();

        int row = (int)cell.Position.X;
        int col = (int)cell.Position.Y;

        if (!IsVisited(row, col - 1))
            neighbors.Add(Direction.Left);

        if (!IsVisited(row - 1, col))
            neighbors.Add(Direction.Up);

        if (!IsVisited(row, col + 1))
            neighbors.Add(Direction.Right);

        if (!IsVisited(row + 1, col))
            neighbors.Add(Direction.Down);

        if (neighbors.Count > 0)
            return (Direction)neighbors[random.Next(0, neighbors.Count)];

        return Direction.None;
    }

    private bool IsVisited(int row, int col) {
        return _maze[row, col]?.IsVisited ?? true;
    }

    private DFSCell BacktrackCurrentCell() {
        DFSCell cell = (DFSCell)_history[_history.Count - 1];
        _history.RemoveAt(_history.Count - 1);

        return cell;
    }

    private DFSCell ConnectToNeighborCell(DFSCell cell, Direction direction) {
        int row = (int)cell.Position.X;
        int col = (int)cell.Position.Y;

        switch (direction) {
            case Direction.Left:
                _maze[row, col].ToggleWall(Direction.Left);
                _maze[row, --col].ToggleWall(Direction.Right);
                break;

            case Direction.Up:
                _maze[row, col].ToggleWall(Direction.Up);
                _maze[--row, col].ToggleWall(Direction.Down);
                break;

            case Direction.Right:
                _maze[row, col].ToggleWall(Direction.Right);
                _maze[row, ++col].ToggleWall(Direction.Left);
                break;

            case Direction.Down:
                _maze[row, col].ToggleWall(Direction.Down);
                _maze[++row, col].ToggleWall(Direction.Up);
                break;
        }

        return _maze[row, col];
    }

    private void CreateChests() {
        for (int row = 0; row < _maze.Length; row++)
            for (int col = 0; col < _maze.Width; col++)
                if (_maze[row, col].IsDeadEnd() && random.NextDouble() < _probabilityOfChests)
                    _maze[row, col].HasChest = true;
    }

    private void CreateEntrance() {
        _maze.Entrance = _maze[0, 0];
        _maze[0, 0].ToggleWall(Direction.Left);
    }

    private void CreateExit() {
        Vector2 exitPosition = new Vector2(random.Range(_maze.Length * 0.5f, _maze.Length), random.Range(_maze.Width * 0.5f, _maze.Width));

        if (exitPosition.X > exitPosition.Y) {
            exitPosition.Y = _maze.Width - 1;
            _maze[(int)exitPosition.X, (int)exitPosition.Y].ToggleWall(Direction.Right);

        } else {
            exitPosition.X = _maze.Length - 1;
            _maze[(int)exitPosition.X, (int)exitPosition.Y].ToggleWall(Direction.Down);
        }

        _maze.Exit = _maze[(int)exitPosition.X, (int)exitPosition.Y];
    }

    private Maze<Cell> DFSMazeToMaze() {
        Maze<Cell> maze = new Maze<Cell>(_maze.Length, _maze.Width, _maze.CellSize) {
            Entrance = _maze.Entrance,
            Exit = _maze.Exit
        };

        for (int row = 0; row < maze.Length; row++)
            for (int col = 0; col < maze.Width; col++)
                maze[row, col] = _maze[row, col];

        return maze;


    }
    #endregion
}

}