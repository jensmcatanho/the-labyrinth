using UnityEngine;
using Maze;

public class StandardMaze : MonoBehaviour, IMaze, Core.IEventListener {

    #region private fields
    [SerializeField] private MazeSettings _mazeSettings = null;

    [SerializeField] private MazeAssets _assets;

    private Maze<Cell> _maze;
    #endregion

    #region public methods
    public void AddListeners() {
        Core.EventManager.Instance.AddListenerOnce<Events.GameSceneLoaded>(OnSceneLoaded);
        Core.EventManager.Instance.AddListenerOnce<Events.MenuSceneLoaded>(OnSceneLoaded);
        Core.EventManager.Instance.AddListener<Events.SpawnCompleted>(OnSpawnCompleted);
        Core.EventManager.Instance.AddListener<Events.MazeInstanced>(OnMazeInstanced);
    }

    public void RemoveListeners() {
        if (Core.EventManager.Instance) {
            Core.EventManager.Instance.RemoveListener<Events.GameSceneLoaded>(OnSceneLoaded);
            Core.EventManager.Instance.RemoveListener<Events.MenuSceneLoaded>(OnSceneLoaded);
            Core.EventManager.Instance.RemoveListener<Events.SpawnCompleted>(OnSpawnCompleted);
            Core.EventManager.Instance.RemoveListener<Events.MazeInstanced>(OnMazeInstanced);
        }
    }
    #endregion

    #region private methods
    private void Awake() {
        AddListeners();
    }

    private void OnDestroy() {
        RemoveListeners();
    }

    private Maze.Factory.IMazeFactory GetFactoryFromSettings() {
        switch (_mazeSettings.Algorithm) {
            case GenerationAlgorithm.DepthFirstSearch:
                return new Maze.Factory.DFSFactory();

            case GenerationAlgorithm.Prim:
                return new Maze.Factory.PrimFactory();

            default:
                return null;
        }
    }

    private void OnSceneLoaded(Events.MenuSceneLoaded e) {
        CreateMaze(e.MazeSettings);
    }

    private void OnSceneLoaded(Events.GameSceneLoaded e) {
        CreateMaze(e.MazeSettings);
    }

    private void CreateMaze(MazeSettings mazeSettings) {
        var mazeFactory = GetFactoryFromSettings();
        _maze = mazeFactory.CreateMaze(mazeSettings.Length, mazeSettings.Width, mazeSettings.CellSize);

        var mazeSpawner = new MazeSpawner(gameObject.transform, _assets.Wall, _assets.Chest);
        mazeSpawner.SpawnMaze(_maze, mazeSettings);
    }

    private void OnMazeInstanced(Events.MazeInstanced e) {
        foreach (Transform child in gameObject.transform) {
            if (child == null) {
                continue;
            }

            child.gameObject.layer = gameObject.layer;
        }
    }

    private void OnSpawnCompleted(Events.SpawnCompleted e) {
        if (e.Reference == _assets.Wall) {
            e.GameObject.transform.localScale *= _maze.CellSize;
        }
    }
   #endregion
}

/*
public class MazeListener : MonoBehaviour, Core.IEventListener {


    #region public methods
    public void AddListeners() {
        Core.EventManager.Instance.AddListenerOnce<Events.GameSceneLoaded>(OnSceneLoaded);
        Core.EventManager.Instance.AddListenerOnce<Events.MenuSceneLoaded>(OnSceneLoaded);
        Core.EventManager.Instance.AddListener<Events.SpawnCompleted>(OnSpawnCompleted);
        Core.EventManager.Instance.AddListener<Events.MazeInstanced>(OnMazeInstanced);
    }

    public void RemoveListeners() {
        if (Core.EventManager.Instance) {
            Core.EventManager.Instance.RemoveListener<Events.GameSceneLoaded>(OnSceneLoaded);
            Core.EventManager.Instance.RemoveListener<Events.MenuSceneLoaded>(OnSceneLoaded);
            Core.EventManager.Instance.RemoveListener<Events.SpawnCompleted>(OnSpawnCompleted);
            Core.EventManager.Instance.RemoveListener<Events.MazeInstanced>(OnMazeInstanced);
        }
    }
    #endregion

    #region private methods
    private void Awake() {
        AddListeners();
    }

    private void OnDestroy() {
        RemoveListeners();
    }

    private void OnMazeInstanced(Events.MazeInstanced e) {
        foreach (Transform child in gameObject.transform) {
            if (child == null) {
                continue;
            }

            child.gameObject.layer = gameObject.layer;
        }
    }

    private void OnSpawnCompleted(Events.SpawnCompleted e) {
        if (e.Reference == _wallAssetReference) {
            e.GameObject.transform.localScale *= _maze.CellSize;
        }
    }
    #endregion

}

*/