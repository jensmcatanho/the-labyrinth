using NUnit.Framework;

namespace Tests {
    public class DFSMazeSolverTest {

        [Test]
        public void Test_IsSolvable_WhenMazeIsGeneratedByDFSAlgorithm() {
            IMazeFactory mazeFactory = new DFSMazeFactory();
            Maze<Cell> maze = mazeFactory.CreateMaze(100, 100, 1);

            IMazeSolver mazeSolver = new DFSMazeSolver();

            Assert.True(mazeSolver.IsSolvable(maze));
        }

        [Test]
        public void Test_IsSolvable_WhenMazeIsGeneratedByPrimAlgorithm() {
            IMazeFactory mazeFactory = new PrimMazeFactory();
            Maze<Cell> maze = mazeFactory.CreateMaze(100, 100, 1);

            IMazeSolver mazeSolver = new DFSMazeSolver();

            Assert.True(mazeSolver.IsSolvable(maze));
        }

        [Test]
        public void Test_IsSolvable_WhenMazeIsNotSolvable() {
            IMazeFactory mazeFactory = new DFSMazeFactory();
            Maze<Cell> maze = mazeFactory.CreateMaze(100, 100, 1);
            maze.Entrance.SetAllWalls();

            IMazeSolver mazeSolver = new DFSMazeSolver();

            Assert.False(mazeSolver.IsSolvable(maze));
        }
    }
}
