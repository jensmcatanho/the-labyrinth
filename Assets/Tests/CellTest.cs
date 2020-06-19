using NUnit.Framework;

namespace Tests {
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

            cell.SetWall(Wall.Up);

            Assert.True(cell.HasWall(Wall.Up));
        }

        [Test]
        public void Test_SetWall_WhenNoWallIsSet() {
            Cell cell = new CellMock(1, 1, 5);

            cell.SetWall(Wall.None);

            Assert.True(cell.HasWall(Wall.None));
        }

        [Test]
        public void Test_SetAllWalls() {
            Cell cell = new CellMock(1, 1, 5);

            cell.SetAllWalls();

            Assert.True(cell.HasWall(Wall.Left));
            Assert.True(cell.HasWall(Wall.Up));
            Assert.True(cell.HasWall(Wall.Down));
            Assert.True(cell.HasWall(Wall.Right));
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

            cell.SetWall(Wall.Left);
            Assert.True(cell.HasWall(Wall.Left));
            Assert.False(cell.HasWall(Wall.Up));
            Assert.False(cell.HasWall(Wall.Down));
            Assert.False(cell.HasWall(Wall.Right));
        }

        [Test]
        public void Test_UnsetWall() {
            Cell cell = new CellMock(1, 1, 5);
            cell.SetAllWalls();

            cell.UnsetWall(Wall.Left);
            Assert.False(cell.HasWall(Wall.Left));
            Assert.True(cell.HasWall(Wall.Up));
            Assert.True(cell.HasWall(Wall.Down));
            Assert.True(cell.HasWall(Wall.Right));
        }

        [Test]
        public void Test_ToggleWall() {
            Cell cell = new CellMock(1, 1, 5);

            cell.ToggleWall(Wall.Left);
            Assert.False(cell.HasWall(Wall.Left));
        }

        [Test]
        public void Test_DeadEndOpening() {
            Cell cell = new CellMock(1, 1, 5);

            cell.UnsetWall(Wall.Left);
            Assert.AreEqual(Wall.Left, cell.DeadEndOpening());
            cell.SetWall(Wall.Left);

            cell.UnsetWall(Wall.Up);
            Assert.AreEqual(Wall.Up, cell.DeadEndOpening());
            cell.SetWall(Wall.Up);

            cell.UnsetWall(Wall.Down);
            Assert.AreEqual(Wall.Down, cell.DeadEndOpening());
            cell.SetWall(Wall.Down);

            cell.UnsetWall(Wall.Right);
            Assert.AreEqual(Wall.Right, cell.DeadEndOpening());
            cell.SetWall(Wall.Right);
        }

                [Test]
        public void Test_DeadEndOpening_WhenCellIsNotDeadEnd() {
            Cell cell = new CellMock(1, 1, 5);

            Assert.AreEqual(Wall.None, (Wall)cell.DeadEndOpening());
        }

        [Test]
        public void Test_IsDeadEnd_WhenCellIsDeadEnd() {
            Cell cell = new CellMock(1, 1, 5);
            cell.UnsetWall(Wall.Left);

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
            cell.UnsetWall(Wall.Left);
            cell.UnsetWall(Wall.Down);

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
