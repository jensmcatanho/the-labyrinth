using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Text;
using Maze;

public class MazeSpawner {

    #region private fields
    private Maze<Cell> _maze;

    private MazeSettings _mazeSettings;

    private readonly Transform _parent;

    private readonly AssetReference _wallAssetReference = null;

    private readonly AssetReference _chestAssetReference = null;

    private readonly Quaternion _leftWallRotation = Quaternion.Euler(90f, 90f, 0f);

    private readonly Quaternion _downWallRotation = Quaternion.Euler(90f, 0f, 0f);

    private readonly Quaternion _upWallRotation = Quaternion.Euler(90f, 0f, 180f);

    private readonly Quaternion _rightWallRotation = Quaternion.Euler(90f, -90f, 0f);
    #endregion

    #region public methods
    public MazeSpawner(Transform parent, AssetReference wallAssetReference, AssetReference chestAssetReference) {
        _parent = parent;
        _wallAssetReference = wallAssetReference;
        _chestAssetReference = chestAssetReference;
    }

    public void SpawnMaze(Maze<Cell> maze, MazeSettings settings) {
        _maze = maze;
        _mazeSettings = settings;

        SpawnFloor();
        SpawnWalls();
        SpawnFinishTrigger();
    }
    #endregion

    #region private methods
    private void SpawnFloor() {
        GameObject floor = GameObject.CreatePrimitive(PrimitiveType.Plane);

        floor.name = "Floor";
        floor.transform.parent = _parent;
        floor.transform.localScale = new Vector3(_maze.Length * 0.2f * _maze.CellSize, 1, _maze.Width * 0.2f * _maze.CellSize);
        floor.transform.position = new Vector3(_maze.Length * _maze.CellSize, 0, _maze.Width * _maze.CellSize);
    }

    private void SpawnWalls() {
        SpawnFirstCell();
        SpawnFirstRow();
        SpawnFirstColumn();

        for (int i = 1; i < _maze.Length; i++) {
            for (int j = 1; j < _maze.Width; j++) {
                SpawnChest(i, j);

                if (_maze[i, j].HasWall(Direction.Right))
                    SpawnRightWall(i, j);

                if (_maze[i, j].HasWall(Direction.Down))
                    SpawnDownWall(i, j);
            }
        }
    }

    private void SpawnFirstCell() {
        if (_maze[0, 0].HasWall(Direction.Left))
            SpawnLeftWall(0, 0);

        if (_maze[0, 0].HasWall(Direction.Down))
            SpawnDownWall(0, 0);

        if (_maze[0, 0].HasWall(Direction.Up))
            SpawnUpWall(0, 0);

        if (_maze[0, 0].HasWall(Direction.Right))
            SpawnRightWall(0, 0);
    }

    private void SpawnFirstColumn() {
        for (int i = 1; i < _maze.Length; i++) {
            SpawnChest(i, 0);

            if (_maze[i, 0].HasWall(Direction.Left))
                SpawnLeftWall(i, 0);

            if (_maze[i, 0].HasWall(Direction.Down))
                SpawnDownWall(i, 0);

            if (_maze[i, 0].HasWall(Direction.Right))
                SpawnRightWall(i, 0);
        }
    }

    private void SpawnFirstRow() {
        for (int i = 1; i < _maze.Width; i++) {
            SpawnChest(0, i);

            if (_maze[0, i].HasWall(Direction.Up))
                SpawnUpWall(0, i);

            if (_maze[0, i].HasWall(Direction.Down))
                SpawnDownWall(0, i);

            if (_maze[0, i].HasWall(Direction.Right))
                SpawnRightWall(0, i);
        }
    }

    private void SpawnLeftWall(int i, int j) {
        var assetReferenceData = new Core.AssetReferenceData(
            _wallAssetReference,
            new Vector3((2 * i + 1) * _maze.CellSize, 2.0f, 2 * j * _maze.CellSize),
            _leftWallRotation,
            _parent,
            MakeWallObjectName(i, j, Direction.Left));

        Core.AssetLoader.Instance.Spawn(assetReferenceData);
    }

    private void SpawnUpWall(int i, int j) {
        var assetReferenceData = new Core.AssetReferenceData(
            _wallAssetReference,
            new Vector3(2 * i * _maze.CellSize, 2.0f, (2 * j + 1) * _maze.CellSize),
            _upWallRotation,
            _parent,
            MakeWallObjectName(i, j, Direction.Up));

        Core.AssetLoader.Instance.Spawn(assetReferenceData);
    }

    private void SpawnRightWall(int i, int j) {
        var assetReferenceData = new Core.AssetReferenceData(
            _wallAssetReference,
            new Vector3((2 * i + 1) * _maze.CellSize, 2.0f, (2 * j + 2) * _maze.CellSize),
            _rightWallRotation,
            _parent,
            MakeWallObjectName(i, j, Direction.Right));

        Core.AssetLoader.Instance.Spawn(assetReferenceData);
    }

    private void SpawnDownWall(int i, int j) {
        var assetReferenceData = new Core.AssetReferenceData(
            _wallAssetReference,
            new Vector3((2 * i + 2) * _maze.CellSize, 2.0f, (2 * j + 1) * _maze.CellSize),
            _downWallRotation,
            _parent,
            MakeWallObjectName(i, j, Direction.Down));

        Core.AssetLoader.Instance.Spawn(assetReferenceData);
    }

    private string MakeWallObjectName(int i, int j, Direction direction) {
        var stringBuilder = new StringBuilder();
        return stringBuilder.Append(" (").Append(i).Append(", ").Append(j).Append(") ").Append(direction).Append(" Direction").ToString();
    }

    private void SpawnChest(int row, int col) {
        if (!_maze[row, col].HasChest || !_mazeSettings.IsPlayable)
            return;

        var assetReferenceData = new Core.AssetReferenceData(
            _chestAssetReference,
            new Vector3((2 * row + 1) * _maze.CellSize, 0.0f, (2 * col + 1) * _maze.CellSize),
             GetChestRotation(_maze[row, col]),
             _parent.parent,
             MakeChestObjectName(row, col));

        Core.AssetLoader.Instance.Spawn(assetReferenceData);
    }

    private Quaternion GetChestRotation(Cell cell) {
        switch (cell.DeadEndOpening()) {
            case Direction.Left:
                return Quaternion.Euler(0f, 180f, 0f);

            case Direction.Up:
                return Quaternion.Euler(0f, -90f, 0f);

            case Direction.Down:
                return Quaternion.Euler(0f, 90f, 0f);

            default:
                return Quaternion.identity;
        }

        /*
		 * Switch expressions are not yet supported by the Unity C# compiler. :(
        return (cell.DeadEndOpening()) switch {
            Direction.Left => Quaternion.Euler(0f, 180f, 0f),
            Direction.Up => Quaternion.Euler(0f, -90f, 0f),
            Direction.Down => Quaternion.Euler(0f, 90f, 0f),
            _ => Quaternion.identity
        };
		*/
    }

    private string MakeChestObjectName(int i, int j) {
        var stringBuilder = new StringBuilder();
        return stringBuilder.Append(" (").Append(i).Append(", ").Append(j).Append(")").Append(" ").Append("Chest").ToString();
    }

    private void SpawnFinishTrigger() {
        GameObject finishTrigger = new GameObject("Finish Trigger");
        finishTrigger.AddComponent<FinishTrigger>();
        finishTrigger.AddComponent<BoxCollider>().isTrigger = true;
        finishTrigger.transform.parent = _parent;

        if (_maze.Exit.Position.X >= _maze.Exit.Position.Y) {
            finishTrigger.transform.position = new Vector3((2 * _maze.Exit.Position.X + 4) * _maze.CellSize - 2 * _maze.CellSize, 1.0f, (2 * _maze.Exit.Position.Y + 1) * _maze.CellSize);
            finishTrigger.transform.localScale = new Vector3(.5f * _maze.CellSize, 2f * _maze.CellSize, 1.5f * _maze.CellSize);

        } else {
            finishTrigger.transform.position = new Vector3((2 * _maze.Exit.Position.X + 1) * _maze.CellSize, 1.0f, (2 * _maze.Exit.Position.Y + 4) * _maze.CellSize - 2 * _maze.CellSize);
            finishTrigger.transform.localScale = new Vector3(2f * _maze.CellSize, 2.0f * _maze.CellSize, _maze.CellSize * 0.5f);
        }
    }
    #endregion
}