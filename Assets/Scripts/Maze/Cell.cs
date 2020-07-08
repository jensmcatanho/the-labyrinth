using System.Numerics;

namespace Maze {

    #region enums
    [System.Flags]
    public enum Direction {
        None = 0,
        Left = 1 << 0,
        Up = 1 << 1,
        Right = 1 << 2,
        Down = 1 << 3
    }
    #endregion

    public class Cell {

        #region private fields
        private Vector2 _position;
        #endregion

        #region constructor
        public Cell(int x, int y, int size) {
            _position = new Vector2(x, y);
            Size = size;

            SetAllWalls();
        }
        #endregion

        #region public methods
        public void SetAllWalls() {
            SetWall(Direction.Left | Direction.Up | Direction.Right | Direction.Down);
        }

        public void UnsetAllWalls() {
            UnsetWall(Direction.Left | Direction.Up | Direction.Right | Direction.Down);
        }

        public Vector2 Position {
            get {
                return _position;
            }
            set {
                _position = value;
            }
        }

        public int Size {
            get;
        }

        public bool HasChest {
            get; set;
        }

        public Direction Walls {
            get;
            private set;
        }

        public void SetWall(Direction target) {
            Walls |= target;
        }

        public void UnsetWall(Direction target) {
            Walls &= ~target;
        }

        public void ToggleWall(Direction target) {
            Walls ^= target;
        }

        public bool HasWall(Direction target) {
            return (Walls & target) == target;
        }

        public Direction DeadEndOpening() {
            if (!IsDeadEnd()) {
                return Direction.None;
            }

            if (!HasWall(Direction.Left)) {
                return Direction.Left;

            } else if (!HasWall(Direction.Up)) {
                return Direction.Up;

            } else if (!HasWall(Direction.Right)) {
                return Direction.Right;
            }

            return Direction.Down;
        }

        public bool IsDeadEnd() {
            int numWalls = 0;

            if (HasWall(Direction.Left))
                numWalls++;

            if (HasWall(Direction.Up))
                numWalls++;

            if (HasWall(Direction.Right))
                numWalls++;

            if (HasWall(Direction.Down))
                numWalls++;

            return numWalls == 3;
        }
        #endregion
    }

}