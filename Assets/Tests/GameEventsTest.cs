using NUnit.Framework;

namespace Tests {
    public class GameEventsTest {

        [Test]
        public void Test_GameEvent_GameSceneLoaded() {
            Events.GameSceneLoaded evt = new Events.GameSceneLoaded();
            Assert.NotNull(evt);
        }

        [Test]
        public void Test_GameEvent_MazeFinished() {
            Events.MazeFinished evt = new Events.MazeFinished();
            Assert.NotNull(evt);
        }

                [Test]
        public void Test_GameEvent_MenuSceneLoaded() {
            Events.MenuSceneLoaded evt = new Events.MenuSceneLoaded();
            Assert.NotNull(evt);
        }

        [Test]
        public void Test_GameEvent_StartButtonClicked() {
            Events.StartButtonClicked evt = new Events.StartButtonClicked();
            Assert.NotNull(evt);
        }
    }
}
