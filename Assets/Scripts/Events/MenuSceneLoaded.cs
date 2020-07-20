namespace Labyrinth.Events {

    public class MenuSceneLoaded : GameEvent {

        public Maze.MazeSettings MazeSettings {
            get;
        }

        public MenuSceneLoaded(Maze.MazeSettings mazeSettings) {
            MazeSettings = mazeSettings;
        }
    }

}