using NUnit.Framework;

namespace Tests {
    public class GameEventsTest {

        [Test]
        public void Test_GameEvent_GameSceneLoaded() {
            var evt = new Events.GameSceneLoaded();
            Assert.NotNull(evt);
        }

        [Test]
        public void Test_GameEvent_MazeFinished() {
            var evt = new Events.MazeFinished();
            Assert.NotNull(evt);
        }

        [Test]
        public void Test_GameEvent_MenuSceneLoaded() {
            var evt = new Events.MenuSceneLoaded();
            Assert.NotNull(evt);
        }

        [Test]
        public void Test_GameEvent_StartButtonClicked() {
            var evt = new Events.StartButtonClicked();
            Assert.NotNull(evt);
        }
    }
}
