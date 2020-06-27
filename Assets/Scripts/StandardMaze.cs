using UnityEngine;
using UnityEngine.AddressableAssets;
using Maze;
using Core;

public class StandardMaze : MonoBehaviour, IMaze, IEventListener {

    #region private variables
    [SerializeField] private MazeSettings _mazeSettings = null;

    [SerializeField] private AssetReference _wallAssetReference = null;

    [SerializeField] private AssetReference _chestAssetReference = null;

    private Maze<Cell> _maze;
    #endregion

    #region public methods
    public void AddListeners() {
        EventManager.Instance.AddListener<Core.Events.InstantiationCompleted>(OnInstantiationCompleted);
    }

    public void RemoveListeners() {
        EventManager.Instance.RemoveListener<Core.Events.InstantiationCompleted>(OnInstantiationCompleted);
    }
    #endregion

    #region private methods
    private void Awake() {
        AddListeners();

        var mazeFactory = GetFactoryFromSettings();
        _maze = mazeFactory.CreateMaze(_mazeSettings.Width, _mazeSettings.Height, _mazeSettings.CellSize);

        var mazeSpawner = new MazeSpawner(gameObject.transform, _wallAssetReference, _chestAssetReference);
        mazeSpawner.SpawnMaze(_maze);
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

    private void OnInstantiationCompleted(Core.Events.InstantiationCompleted e) {
        if (e.Reference == _wallAssetReference) {
            e.GameObject.transform.localScale *= _maze.CellSize;
        }
    }
    #endregion
}