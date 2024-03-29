﻿using NUnit.Framework;

namespace Labyrinth.Maze.Factory.Tests {
    public class DFSMazeFactoryTest {

        private readonly float _probabilityOfDeadEnds = 0.1f;

        private readonly float _probabilityOfChests = 0.5f;

        [Test]
        public void Test_CreateMaze_ApproximateDeadEndsPercentageShouldMatchTheExpectedForDFSAlgorithm() {
            IMazeFactory dfsMazeFactory = new DFSFactory();
            int maxSize = 50;
            float delta = 0.05f;

            for (int size = 10; size < maxSize; size += 10) {
                int length = size;
                int width = size;
                int numberOfCells = length * width;
                int numberOfDeadEnds = 0;

                Maze<Cell> maze = dfsMazeFactory.CreateMaze(length, width, 1);

                for (int row = 0; row < length; row++)
                    for (int col = 0; col < width; col++)
                        numberOfDeadEnds += maze[row, col].IsDeadEnd() ? 1 : 0;

                float percentageOfDeadEnds = numberOfDeadEnds / (float)numberOfCells;
                Assert.AreEqual(_probabilityOfDeadEnds, percentageOfDeadEnds, delta);
            }
        }

        [Test]
        public void Test_CreateMaze_NumberOfChestsShouldMatchTheExpectedProbability() {
            IMazeFactory dfsMazeFactory = new DFSFactory();
            int maxSize = 50;
            float delta = 0.5f;

            for (int size = 10; size < maxSize; size += 10) {
                int length = size;
                int width = size;
                int numberOfCells = length * width;
                int numberOfDeadEnds = 0;
                int numberOfChests = 0;

                Maze<Cell> maze = dfsMazeFactory.CreateMaze(length, width, 1);

                for (int row = 0; row < length; row++)
                    for (int col = 0; col < width; col++) {
                        numberOfDeadEnds += maze[row, col].IsDeadEnd() ? 1 : 0;
                        numberOfChests += maze[row, col].HasChest ? 1 : 0;
                    }

                float percentageOfChests = numberOfChests / (float)numberOfDeadEnds;
                Assert.AreEqual(_probabilityOfChests, percentageOfChests, delta);
            }
        }

        [Test]
        public void Test_CreateMaze_MazeCellsShouldHaveTheSameSize() {
            IMazeFactory dfsMazeFactory = new DFSFactory();
            int length = 20;
            int width = 20;
            int cellSize = 2;

            Maze<Cell> maze = dfsMazeFactory.CreateMaze(length, width, cellSize);

            for (int row = 0; row < length; row++)
                for (int col = 0; col < width; col++)
                    Assert.AreEqual(cellSize, maze[row, col].Size);
        }

        [Test]
        public void Test_CreateMaze_MazeShouldHaveAnEntrance() {
            IMazeFactory dfsMazeFactory = new DFSFactory();

            Maze<Cell> maze = dfsMazeFactory.CreateMaze(20, 20, 1);

            Assert.IsNotNull(maze.Entrance);
        }

        [Test]
        public void Test_CreateMaze_MazeShouldHaveAnExit() {
            IMazeFactory dfsMazeFactory = new DFSFactory();

            Maze<Cell> maze = dfsMazeFactory.CreateMaze(20, 20, 1);

            Assert.IsNotNull(maze.Exit);
        }

        [Test]
        public void Test_CreateMaze_MazeSizeShouldMatchArguments() {
            IMazeFactory dfsMazeFactory = new DFSFactory();
            int length = 20;
            int width = 20;
            int cellSize = 2;

            Maze<Cell> maze = dfsMazeFactory.CreateMaze(length, width, cellSize);

            Assert.AreEqual(length, maze.Length);
            Assert.AreEqual(width, maze.Width);
        }

        [Test]
        public void Test_CreateMaze_MazeShouldHaveASolution() {
            IMazeFactory dfsMazeFactory = new DFSFactory();

            int length = 50;
            int width = 50;
            int cellSize = 2;

            Maze<Cell> maze = dfsMazeFactory.CreateMaze(length, width, cellSize);

            Solver.IMazeSolver dfsMazeSolver = new Solver.DFSSolver();
            Assert.True(dfsMazeSolver.IsSolvable(maze));
        }

        [Test]
        public void Test_CreateMaze_MazeShoulNotHaveCellsWithFourWalls() {
            IMazeFactory dfsMazeFactory = new DFSFactory();
            int length = 20;
            int width = 20;
            int cellSize = 2;

            Maze<Cell> maze = dfsMazeFactory.CreateMaze(length, width, cellSize);

            for (int row = 0; row < length; row++)
                for (int col = 0; col < width; col++) {
                    Cell cell = maze[row, col];
                    Assert.False(
                        cell.HasWall(Direction.Left) &&
                        cell.HasWall(Direction.Up) &&
                        cell.HasWall(Direction.Down) &&
                        cell.HasWall(Direction.Right)
                    );
                }
        }
    }
}
