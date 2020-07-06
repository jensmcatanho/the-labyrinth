using Maze;

namespace Events {

    public class MenuSceneLoaded : GameEvent {

        public MazeSettings MazeSettings {
            get;
        }

        public MenuSceneLoaded(MazeSettings mazeSettings) {
            MazeSettings = mazeSettings;
        }
    }

}