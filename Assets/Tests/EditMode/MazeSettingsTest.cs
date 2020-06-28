using NUnit.Framework;
using UnityEngine;

namespace Tests {
    public class MazeSettingsTest {

        [Test]
        public void Test_MazeSettings_Width() {
            var mazeSettings = ScriptableObject.CreateInstance<MazeSettings>();
            Assert.AreEqual(50, mazeSettings.Width);
        }

        [Test]
        public void Test_MazeSettings_Height() {
            var mazeSettings = ScriptableObject.CreateInstance<MazeSettings>();
            Assert.AreEqual(50, mazeSettings.Height);
        }

        [Test]
        public void Test_MazeSettings_CellSize() {
            var mazeSettings = ScriptableObject.CreateInstance<MazeSettings>();
            Assert.AreEqual(5, mazeSettings.CellSize);
        }
    }
}
