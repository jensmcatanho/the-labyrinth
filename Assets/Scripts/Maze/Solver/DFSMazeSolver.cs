using System;
using System.Collections;

namespace Labyrinth.Maze.Solver {

    public class DFSSolver : IMazeSolver {

        private Maze<DFSCell> _maze;

        private readonly ArrayList _history = new ArrayList();

        private static readonly Random random = new Random();

        public DFSSolver() {

        }

        public bool IsSolvable(Maze<Cell> maze) {
            _maze = new Maze<DFSCell>(maze.Length, maze.Width, maze.CellSize);

            for (int row = 0; row < maze.Length; row++)
                for (int col = 0; col < maze.Width; col++) {
                    _maze[row, col] = new DFSCell(row, col, maze.CellSize);
                    _maze[row, col].UnsetAllWalls();
                    _maze[row, col].SetWall(maze[row, col].Walls);
                }

            _maze.Entrance = _maze[(int)maze.Entrance.Position.X, (int)maze.Entrance.Position.Y];
            _maze.Exit = _maze[(int)maze.Exit.Position.X, (int)maze.Exit.Position.Y];

            return FindPath(_maze.Entrance);
        }

        private bool FindPath(DFSCell cell) {
            DFSCell currentCell = cell;
            currentCell.IsVisited = true;
            _history.Add(currentCell);

            while (_history.Count > 0) {
                if (currentCell == _maze.Exit)
                    return true;

                DFSCell nextCell = GetNeighbor(currentCell);

                if (nextCell != null) {
                    currentCell = nextCell;
                    currentCell.IsVisited = true;
                    _history.Add(currentCell);

                } else {
                    _history.RemoveAt(_history.Count - 1);
                    currentCell = _history.Count > 0 ? (DFSCell)_history[_history.Count - 1] : null;
                }
            }

            return false;
        }

        private DFSCell GetNeighbor(DFSCell cell) {
            ArrayList neighbors = new ArrayList();

            int row = (int)cell.Position.X;
            int col = (int)cell.Position.Y;

            if (!cell.HasWall(Direction.Left) && !IsVisited(row, col - 1))
                neighbors.Add(_maze[row, col - 1]);

            if (!cell.HasWall(Direction.Up) && !IsVisited(row - 1, col))
                neighbors.Add(_maze[row - 1, col]);

            if (!cell.HasWall(Direction.Right) && !IsVisited(row, col + 1))
                neighbors.Add(_maze[row, col + 1]);

            if (!cell.HasWall(Direction.Down) && !IsVisited(row + 1, col))
                neighbors.Add(_maze[row + 1, col]);

            if (neighbors.Count > 0)
                return (DFSCell)neighbors[random.Next(0, neighbors.Count)];

            return null;
        }

        private bool IsVisited(int row, int col) {
            return _maze[row, col]?.IsVisited ?? true;
        }
    }

}