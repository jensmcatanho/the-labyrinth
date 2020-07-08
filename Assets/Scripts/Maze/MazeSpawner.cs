using System.Text;
using UnityEngine;

namespace Maze {

    public class MazeSpawner {

        #region private fields
        private Maze<Cell> _maze;

        private MazeSettings _mazeSettings;

        private readonly Transform _parent;

        private readonly MazeAssets _assets = null;
        #endregion

        #region public methods
        public MazeSpawner(Transform parent, MazeAssets assets) {
            _parent = parent;
            _assets = assets;
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
                _assets.Wall,
                new Vector3((2 * i + 1) * _maze.CellSize, 2.0f, 2 * j * _maze.CellSize),
                WallRotation.Left,
                _parent,
                MakeWallObjectName(i, j, Direction.Left));

            Core.AssetLoader.Instance.Spawn(assetReferenceData);
        }

        private void SpawnUpWall(int i, int j) {
            var assetReferenceData = new Core.AssetReferenceData(
                _assets.Wall,
                new Vector3(2 * i * _maze.CellSize, 2.0f, (2 * j + 1) * _maze.CellSize),
                WallRotation.Up,
                _parent,
                MakeWallObjectName(i, j, Direction.Up));

            Core.AssetLoader.Instance.Spawn(assetReferenceData);
        }

        private void SpawnRightWall(int i, int j) {
            var assetReferenceData = new Core.AssetReferenceData(
                _assets.Wall,
                new Vector3((2 * i + 1) * _maze.CellSize, 2.0f, (2 * j + 2) * _maze.CellSize),
                WallRotation.Right,
                _parent,
                MakeWallObjectName(i, j, Direction.Right));

            Core.AssetLoader.Instance.Spawn(assetReferenceData);
        }

        private void SpawnDownWall(int i, int j) {
            var assetReferenceData = new Core.AssetReferenceData(
                _assets.Wall,
                new Vector3((2 * i + 2) * _maze.CellSize, 2.0f, (2 * j + 1) * _maze.CellSize),
                WallRotation.Down,
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
                _assets.Chest,
                new Vector3((2 * row + 1) * _maze.CellSize, 0.0f, (2 * col + 1) * _maze.CellSize),
                 GetChestRotation(_maze[row, col]),
                 _parent.parent,
                 MakeChestObjectName(row, col));

            Core.AssetLoader.Instance.Spawn(assetReferenceData);
        }

        private Quaternion GetChestRotation(Cell cell) {
            // Switch expressions are not yet supported by the Unity C# compiler :(
            switch (cell.DeadEndOpening()) {
                case Direction.Left:
                    return ChestRotation.Left;

                case Direction.Up:
                    return ChestRotation.Up;

                case Direction.Down:
                    return ChestRotation.Down;

                default:
                    return ChestRotation.Right;
            }
        }

        private string MakeChestObjectName(int i, int j) {
            var stringBuilder = new StringBuilder();
            return stringBuilder.Append(" (").Append(i).Append(", ").Append(j).Append(")").Append(" ").Append("Chest").ToString();
        }

        private void SpawnFinishTrigger() {
            var finishTrigger = FinishTrigger.Spawn(_maze.Exit);
            finishTrigger.transform.parent = _parent;
        }
        #endregion
    }

}