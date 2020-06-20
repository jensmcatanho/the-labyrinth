using NUnit.Framework;

namespace Tests {
    public class DFSCellTest {

        [Test]
        public void Test_Constructor() {
            int x = 1;
            int y = 1;
            int size = 5;

            DFSCell cell = new DFSCell(x, y, size);

            Assert.AreEqual(x, cell.Position.X);
            Assert.AreEqual(y, cell.Position.Y);
            Assert.AreEqual(size, cell.Size);
        }

        [Test]
        public void Test_IsVisited() {
            DFSCell cell = new DFSCell(1, 1, 5);
            cell.IsVisited = true;
            Assert.True(cell.IsVisited);
        }
    }
}
