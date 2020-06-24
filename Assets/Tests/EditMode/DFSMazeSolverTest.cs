using NUnit.Framework;

namespace Maze.Solver.Tests {
    public class DFSMazeSolverTest {

        [Test]
        public void Test_IsSolvable_WhenMazeIsGeneratedByDFSAlgorithm() {
            Factory.IMazeFactory mazeFactory = new Factory.DFSFactory();
            Maze<Cell> maze = mazeFactory.CreateMaze(100, 100, 1);

            IMazeSolver mazeSolver = new DFSSolver();

            Assert.True(mazeSolver.IsSolvable(maze));
        }

        [Test]
        public void Test_IsSolvable_WhenMazeIsGeneratedByPrimAlgorithm() {
            Factory.IMazeFactory mazeFactory = new Factory.PrimFactory();
            Maze<Cell> maze = mazeFactory.CreateMaze(100, 100, 1);

            IMazeSolver mazeSolver = new DFSSolver();

            Assert.True(mazeSolver.IsSolvable(maze));
        }

        [Test]
        public void Test_IsSolvable_WhenMazeIsNotSolvable() {
            Factory.IMazeFactory mazeFactory = new Factory.DFSFactory();
            Maze<Cell> maze = mazeFactory.CreateMaze(100, 100, 1);
            maze.Entrance.SetAllWalls();

            IMazeSolver mazeSolver = new DFSSolver();

            Assert.False(mazeSolver.IsSolvable(maze));
        }
    }
}
