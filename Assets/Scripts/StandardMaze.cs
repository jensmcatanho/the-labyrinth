using UnityEngine;
using UnityEngine.AddressableAssets;
using Maze;

public class StandardMaze : MonoBehaviour, IMaze, Core.IEventListener {

    #region private fields
    [SerializeField] private MazeSettings _mazeSettings = null;

    [SerializeField] private AssetReference _wallAssetReference = null;

    [SerializeField] private AssetReference _chestAssetReference = null;

    private Maze<Cell> _maze;
    #endregion

    #region public methods
    public void AddListeners() {
        Core.EventManager.Instance.AddListener<Events.SpawnCompleted>(OnSpawnCompleted);
    }

    public void RemoveListeners() {
        if (Core.EventManager.Instance)
            Core.EventManager.Instance.RemoveListener<Events.SpawnCompleted>(OnSpawnCompleted);
    }
    #endregion

    #region private methods
    private void Awake() {
        AddListeners();

        var mazeFactory = GetFactoryFromSettings();
        _maze = mazeFactory.CreateMaze(_mazeSettings.Length, _mazeSettings.Width, _mazeSettings.CellSize);

        var mazeSpawner = new MazeSpawner(gameObject.transform, _wallAssetReference, _chestAssetReference);
        mazeSpawner.SpawnMaze(_maze, _mazeSettings);
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

    private void OnSpawnCompleted(Events.SpawnCompleted e) {
        if (e.Reference == _wallAssetReference) {
            e.GameObject.transform.localScale *= _maze.CellSize;
        }
    }
   #endregion
}