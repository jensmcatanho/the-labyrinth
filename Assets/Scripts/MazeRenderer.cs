using UnityEngine;
using UnityEngine.AddressableAssets;
using Maze;
using Core;
using System.Collections.Generic;

public class MazeRenderer : MonoBehaviour, IEventListener {

    #region private variables
    private GameObject _mazeObject;

    private List<GameObject> _wallObjects = new List<GameObject>();

    private List<GameObject> _chestObjects = new List<GameObject>();

    [SerializeField] private AssetReference _wallReference;

    [SerializeField] private AssetReference _chestReference;

    private readonly Quaternion _leftWallRotation = Quaternion.Euler(90f, 90f, 0f);

    private readonly Quaternion _downWallRotation = Quaternion.Euler(90f, 0f, 0f);

    private readonly Quaternion _upWallRotation = Quaternion.Euler(90f, 0f, 180f);

    private readonly Quaternion _rightWallRotation = Quaternion.Euler(90f, -90f, 0f);

    private Maze<Cell> _maze;
    #endregion

    #region public methods
    public void Render(Maze<Cell> maze) {
        _mazeObject = transform.parent.gameObject;
        _maze = maze;

        InstantiateFloor();
        InstantiateWalls();
        InstantiateFinishTrigger();
    }

    public void AddListeners() {
        EventManager.Instance.AddListener<Core.Events.InstantiationCompleted>(OnInstantiationCompleted);
        EventManager.Instance.AddListener<Core.Events.InstanceDestroyed>(OnInstanceDestroyed);
    }

    public void RemoveListeners() {
        EventManager.Instance.RemoveListener<Core.Events.InstantiationCompleted>(OnInstantiationCompleted);
        EventManager.Instance.RemoveListener<Core.Events.InstanceDestroyed>(OnInstanceDestroyed);
    }
    #endregion

    #region private methods
    private void Awake() {
        AddListeners();
    }

    private void OnDestroy() {
        RemoveListeners();
    }

    private void InstantiateFloor() {
        GameObject floor = GameObject.CreatePrimitive(PrimitiveType.Plane);

        floor.transform.parent = _mazeObject.transform;
        floor.transform.localScale = new Vector3(_maze.Length * 0.2f * _maze.CellSize, 1, _maze.Width * 0.2f * _maze.CellSize);
        floor.transform.position = new Vector3(_maze.Length * _maze.CellSize, 0, _maze.Width * _maze.CellSize);
    }

    private void InstantiateWalls() {
        /*
		 *  Note that the maze matrix is not traversed like we normally would do. Instead of instantiating every cell of each row in order, we
		 *  first instatiate cells from the diagonal. Next, we instantiate cells from the lower and upper triangular parts of the maze separately.
		 *  This way we ensure that we are not instantiating a wall south of the cell on the same place we have already instantiated a wall north
		 *  of the cell right below.
		 */
        InstantiateDiagonal();

        for (int i = 0; i < _maze.Length; i++)
            for (int j = i + 1; j < _maze.Width; j++) {
                InstantiateLowerTriangular(i, j);
                InstantiateChest(j, i);

                InstantiateUpperTriangular( i, j);
                InstantiateChest(i, j);
            }
    }

    private void InstantiateUpperTriangular(int i, int j) {
        if (_maze[i, j].HasWall(Wall.Up))
            InstantiateWall(new Vector3(2 * i * _maze.CellSize, 2.0f, (2 * j + 1) * _maze.CellSize), _upWallRotation);

        if (_maze[i, j].HasWall(Wall.Right))
            InstantiateWall(new Vector3((2 * i + 1) * _maze.CellSize, 2.0f, (2 * j + 2) * _maze.CellSize), _rightWallRotation);
    }

    private void InstantiateLowerTriangular(int i, int j) {
        if (_maze[j, i].HasWall(Wall.Left))
            InstantiateWall(new Vector3((2 * j + 1) * _maze.CellSize, 2.0f, 2 * i * _maze.CellSize), _leftWallRotation);

        if (_maze[j, i].HasWall(Wall.Down))
            InstantiateWall(new Vector3((2 * j + 2) * _maze.CellSize, 2.0f, (2 * i + 1) * _maze.CellSize), _downWallRotation);
    }

    private void InstantiateDiagonal() {
        for (int i = 0; i < _maze.Length; i++) {
            if (_maze[i, i].HasWall(Wall.Left))
                InstantiateWall(new Vector3((2 * i + 1) * _maze.CellSize, 2.0f, 2 * i * _maze.CellSize), _leftWallRotation);

            if (_maze[i, i].HasWall(Wall.Down))
                InstantiateWall(new Vector3((2 * i + 2) * _maze.CellSize, 2.0f, (2 * i + 1) * _maze.CellSize), _downWallRotation);

            if (_maze[i, i].HasWall(Wall.Up))
                InstantiateWall(new Vector3(2 * i * _maze.CellSize, 2.0f, (2 * i + 1) * _maze.CellSize), _upWallRotation);

            if (_maze[i, i].HasWall(Wall.Right))
                InstantiateWall(new Vector3((2 * i + 1) * _maze.CellSize, 2.0f, (2 * i + 2) * _maze.CellSize), _rightWallRotation);
        }
    }

    private void InstantiateWall(Vector3 position, Quaternion rotation) {
        var assetReferenceData = new AssetReferenceData(_wallReference, position, rotation);
        AssetLoader.Instance.Spawn(assetReferenceData);
    }

    private void InstantiateChest(int row, int col) {
        if (!_maze[row, col].HasChest)
            return;

        var position = new Vector3((2 * row + 1) * _maze.CellSize, 0.0f, (2 * col + 1) * _maze.CellSize);
        var rotation = GetChestRotation(_maze[row, col]);
        var assetReferenceData = new AssetReferenceData(_chestReference, position, rotation);

        AssetLoader.Instance.Spawn(assetReferenceData);
    }

    /*
    private void InstantiateWall(Maze<Cell> maze, Vector3 position, Quaternion rotation) {
        GameObject wall = Instantiate(_wallPrefab, position, rotation);

        wall.transform.parent = _maze.transform;
        wall.transform.localScale *= maze.CellSize;
    }

    private void InstantiateChest(Maze<Cell> maze, int row, int col) {
        if (!maze[row, col].HasChest)
            return;

        Vector3 chestPosition = new Vector3((2 * row + 1) * maze.CellSize, 0.0f, (2 * col + 1) * maze.CellSize);
        Quaternion chestRotation = GetChestRotation(maze[row, col]);

        GameObject chest = Instantiate(_chestPrefab, chestPosition, chestRotation);
        chest.transform.parent = _maze.transform;
    }
    */

    private Quaternion GetChestRotation(Cell cell) {
        switch (cell.DeadEndOpening()) {
            case Wall.Left:
                return Quaternion.Euler(0f, 180f, 0f);

            case Wall.Up:
                return Quaternion.Euler(0f, -90f, 0f);

            case Wall.Down:
                return Quaternion.Euler(0f, 90f, 0f);

            default:
                return Quaternion.identity;
        }

        /*
		 * Switch expressions are not yet supported by the Unity C# compiler. :(
        return (cell.DeadEndOpening()) switch {
            Wall.Left => Quaternion.Euler(0f, 180f, 0f),
            Wall.Up => Quaternion.Euler(0f, -90f, 0f),
            Wall.Down => Quaternion.Euler(0f, 90f, 0f),
            _ => Quaternion.identity
        };
		*/
    }

    private void InstantiateFinishTrigger() {
        GameObject finishTrigger = new GameObject("Finish Trigger");
        finishTrigger.AddComponent<FinishTrigger>();
        finishTrigger.AddComponent<BoxCollider>().isTrigger = true;
        finishTrigger.transform.parent = _mazeObject.transform;

        if (_maze.Exit.Position.X >= _maze.Exit.Position.Y) {
            finishTrigger.transform.position = new Vector3((2 * _maze.Exit.Position.X + 4) * _maze.CellSize - 2 * _maze.CellSize, 1.0f, (2 * _maze.Exit.Position.Y + 1) * _maze.CellSize);
            finishTrigger.transform.localScale = new Vector3(.5f * _maze.CellSize, 2f * _maze.CellSize, 1.5f * _maze.CellSize);

        } else {
            finishTrigger.transform.position = new Vector3((2 * _maze.Exit.Position.X + 1) * _maze.CellSize, 1.0f, (2 * _maze.Exit.Position.Y + 4) * _maze.CellSize - 2 * _maze.CellSize);
            finishTrigger.transform.localScale = new Vector3(2f * _maze.CellSize, 2.0f * _maze.CellSize, _maze.CellSize * 0.5f);
        }
    }

    private void OnInstantiationCompleted(Core.Events.InstantiationCompleted e) {
        if (e.Reference == _wallReference) {
            e.GameObject.transform.parent = _mazeObject.transform;
            e.GameObject.transform.localScale *= _maze.CellSize;

            _wallObjects.Add(e.GameObject);

        } else if (e.Reference == _chestReference) {
            e.GameObject.transform.parent = _mazeObject.transform;

            _chestObjects.Add(e.GameObject);

        }
    }

    private void OnInstanceDestroyed(Core.Events.InstanceDestroyed e) {
        if (e.Reference == _wallReference) {
            _wallObjects.Remove(e.GameObject);

        } else if (e.Reference == _chestReference) {
            _chestObjects.Remove(e.GameObject);
        }
    }
    #endregion
}