using NUnit.Framework;

namespace Tests {
    public class GameEventsTest {

        [Test]
        public void Test_GameEvent_GameSceneLoaded() {
            Core.Events.GameSceneLoaded evt = new Core.Events.GameSceneLoaded();
            Assert.NotNull(evt);
        }

        [Test]
        public void Test_GameEvent_MazeFinished() {
            Events.MazeFinished evt = new Events.MazeFinished();
            Assert.NotNull(evt);
        }

        [Test]
        public void Test_GameEvent_MenuSceneLoaded() {
            Core.Events.MenuSceneLoaded evt = new Core.Events.MenuSceneLoaded();
            Assert.NotNull(evt);
        }

        [Test]
        public void Test_GameEvent_StartButtonClicked() {
            Events.StartButtonClicked evt = new Events.StartButtonClicked();
            Assert.NotNull(evt);
        }
    }
}
