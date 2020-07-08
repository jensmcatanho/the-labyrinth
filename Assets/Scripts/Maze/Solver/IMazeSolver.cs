namespace Maze.Solver {

    public interface IMazeSolver {

        bool IsSolvable(Maze<Cell> maze);
    }

}