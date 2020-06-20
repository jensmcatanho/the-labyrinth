using NUnit.Framework;

namespace Tests {
    public class MazeSettingsTest {

        [Test]
        public void Test_MazeSettings_Width() {
            MazeSettings mazeSettings = new MazeSettings();
            Assert.AreEqual(50, mazeSettings.Width);
        }

        [Test]
        public void Test_MazeSettings_Height() {
            MazeSettings mazeSettings = new MazeSettings();
            Assert.AreEqual(50, mazeSettings.Height);
        }

        [Test]
        public void Test_MazeSettings_CellSize() {
            MazeSettings mazeSettings = new MazeSettings();
            Assert.AreEqual(5, mazeSettings.CellSize);
        }
    }
}
