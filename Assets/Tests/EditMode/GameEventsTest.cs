using NUnit.Framework;

namespace Labyrinth.Tests {
    public class GameEventsTest {

        [Test]
        public void Test_GameEvent_MazeFinished() {
            var evt = new Events.MazeFinished();
            Assert.NotNull(evt);
        }
    }
}
