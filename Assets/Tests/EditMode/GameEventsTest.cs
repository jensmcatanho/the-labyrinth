using NUnit.Framework;

namespace Tests {
    public class GameEventsTest {

        [Test]
        public void Test_GameEvent_MazeFinished() {
            var evt = new Events.MazeFinished();
            Assert.NotNull(evt);
        }

        [Test]
        public void Test_GameEvent_StartButtonClicked() {
            var evt = new Events.StartButtonClicked();
            Assert.NotNull(evt);
        }
    }
}
