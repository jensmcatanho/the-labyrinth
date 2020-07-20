namespace Labyrinth.Events {

    public class GameSceneLoaded : GameEvent {

        public Maze.MazeSettings MazeSettings {
            get;
        }

        public GameSceneLoaded(Maze.MazeSettings mazeSettings) {
            MazeSettings = mazeSettings;
        }
    }

}