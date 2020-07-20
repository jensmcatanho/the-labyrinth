using NUnit.Framework;

namespace Labyrinth.Maze.Tests {
    public class CellTest {

        public class CellMock : Cell {
            public CellMock(int x, int y, int size)
                : base(x, y, size) {

            }
        }

        [Test]
        public void Test_Constructor() {
            int x = 1;
            int y = 1;
            int size = 5;

            Cell cell = new CellMock(x, y, size);

            Assert.AreEqual(x, cell.Position.X);
            Assert.AreEqual(y, cell.Position.Y);
            Assert.AreEqual(size, cell.Size);
        }

        [Test]
        public void Test_SetWall_WhenAWallIsSet() {
            Cell cell = new CellMock(1, 1, 5);

            cell.SetWall(Direction.Up);

            Assert.True(cell.HasWall(Direction.Up));
        }

        [Test]
        public void Test_SetWall_WhenNoWallIsSet() {
            Cell cell = new CellMock(1, 1, 5);

            cell.SetWall(Direction.None);

            Assert.True(cell.HasWall(Direction.None));
        }

        [Test]
        public void Test_SetWall_WhenThreeWallsAreSet() {
            Cell cell = new CellMock(1, 1, 5);
            cell.UnsetAllWalls();

            cell.SetWall(Direction.Down | Direction.Up | Direction.Right);

            Assert.True(
                cell.HasWall(Direction.Down) &&
                cell.HasWall(Direction.Up) &&
                cell.HasWall(Direction.Right) &&
                !cell.HasWall(Direction.Left));
        }

        [Test]
        public void Test_SetAllWalls() {
            Cell cell = new CellMock(1, 1, 5);

            cell.SetAllWalls();

            Assert.True(cell.HasWall(Direction.Left));
            Assert.True(cell.HasWall(Direction.Up));
            Assert.True(cell.HasWall(Direction.Down));
            Assert.True(cell.HasWall(Direction.Right));
        }

        [Test]
        public void Test_Position() {
            System.Numerics.Vector2 position = new System.Numerics.Vector2(2, 2);

            Cell cell = new CellMock(1, 1, 5) {
                Position = position
            };

            Assert.AreEqual(position, cell.Position);
        }

        [Test]
        public void Test_Size() {
            Cell cell = new CellMock(1, 1, 5);

            Assert.AreEqual(5, cell.Size);
        }

        [Test]
        public void Test_HasChest() {
            Cell cell = new CellMock(1, 1, 5) {
                HasChest = true
            };

            Assert.True(cell.HasChest);
        }

        [Test]
        public void Test_SetWall() {
            Cell cell = new CellMock(1, 1, 5);
            cell.UnsetAllWalls();

            cell.SetWall(Direction.Left);
            Assert.True(cell.HasWall(Direction.Left));
            Assert.False(cell.HasWall(Direction.Up));
            Assert.False(cell.HasWall(Direction.Down));
            Assert.False(cell.HasWall(Direction.Right));
        }

        [Test]
        public void Test_UnsetWall() {
            Cell cell = new CellMock(1, 1, 5);
            cell.SetAllWalls();

            cell.UnsetWall(Direction.Left);
            Assert.False(cell.HasWall(Direction.Left));
            Assert.True(cell.HasWall(Direction.Up));
            Assert.True(cell.HasWall(Direction.Down));
            Assert.True(cell.HasWall(Direction.Right));
        }

        [Test]
        public void Test_ToggleWall() {
            Cell cell = new CellMock(1, 1, 5);

            cell.ToggleWall(Direction.Left);
            Assert.False(cell.HasWall(Direction.Left));
        }

        [Test]
        public void Test_DeadEndOpening() {
            Cell cell = new CellMock(1, 1, 5);

            cell.UnsetWall(Direction.Left);
            Assert.AreEqual(Direction.Left, cell.DeadEndOpening());
            cell.SetWall(Direction.Left);

            cell.UnsetWall(Direction.Up);
            Assert.AreEqual(Direction.Up, cell.DeadEndOpening());
            cell.SetWall(Direction.Up);

            cell.UnsetWall(Direction.Down);
            Assert.AreEqual(Direction.Down, cell.DeadEndOpening());
            cell.SetWall(Direction.Down);

            cell.UnsetWall(Direction.Right);
            Assert.AreEqual(Direction.Right, cell.DeadEndOpening());
            cell.SetWall(Direction.Right);
        }

        [Test]
        public void Test_DeadEndOpening_WhenCellIsNotDeadEnd() {
            Cell cell = new CellMock(1, 1, 5);

            Assert.AreEqual(Direction.None, (Direction)cell.DeadEndOpening());
        }

        [Test]
        public void Test_IsDeadEnd_WhenCellIsDeadEnd() {
            Cell cell = new CellMock(1, 1, 5);
            cell.UnsetWall(Direction.Left);

            Assert.True(cell.IsDeadEnd());
        }

        [Test]
        public void Test_IsDeadEnd_WhenAllWallsAreSet() {
            Cell cell = new CellMock(1, 1, 5);

            Assert.False(cell.IsDeadEnd());
        }

        [Test]
        public void Test_IsDeadEnd_WhenCellIsNotDeadEnd() {
            Cell cell = new CellMock(1, 1, 5);
            cell.UnsetWall(Direction.Left);
            cell.UnsetWall(Direction.Down);

            Assert.False(cell.IsDeadEnd());
        }

        [Test]
        public void Test_IsDeadEnd_WhenNoWallIsSet() {
            Cell cell = new CellMock(1, 1, 5);
            cell.UnsetAllWalls();

            Assert.False(cell.IsDeadEnd());
        }
    }
}
