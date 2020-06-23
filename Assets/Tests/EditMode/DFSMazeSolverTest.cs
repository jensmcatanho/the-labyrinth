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
    }
}
