using Maze;

namespace Events {

    public class GameSceneLoaded : GameEvent {

        public MazeSettings MazeSettings {
            get;
        }

        public GameSceneLoaded(MazeSettings mazeSettings) {
            MazeSettings = mazeSettings;
        }
    }

}