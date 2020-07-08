namespace Maze.Factory {

    public interface IMazeFactory {
        Maze<Cell> CreateMaze(int length, int width, int cellSize);
    }

}