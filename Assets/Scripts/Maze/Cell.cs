using System.Numerics;

namespace Maze {

#region enums
[System.Flags]
public enum Wall {
    None = 0,
    Left = 1 << 0,
    Up = 1 << 1,
    Right = 1 << 2,
    Down = 1 << 3
}
#endregion

public class Cell {

    #region private fields
    private Wall _walls;

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
        SetWall(Wall.Left | Wall.Up | Wall.Right | Wall.Down);
    }

    public void UnsetAllWalls() {
        UnsetWall(Wall.Left | Wall.Up | Wall.Right | Wall.Down);
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

    public Wall Walls {
        get {
            return _walls;
        }
    }

    public void SetWall(Wall target) {
        _walls |= target;
    }

    public void UnsetWall(Wall target) {
        _walls &= ~target;
    }

    public void ToggleWall(Wall target) {
        _walls ^= target;
    }

    public bool HasWall(Wall target) {
        return (_walls & target) == target;
    }

    public Wall DeadEndOpening() {
        if (!IsDeadEnd()) {
            return Wall.None;
        }

        if (!HasWall(Wall.Left)) {
            return Wall.Left;

        } else if (!HasWall(Wall.Up)) {
            return Wall.Up;

        } else if (!HasWall(Wall.Right)) {
            return Wall.Right;
        }

        return Wall.Down;
    }

    public bool IsDeadEnd() {
        int numWalls = 0;

        if (HasWall(Wall.Left))
            numWalls++;

        if (HasWall(Wall.Up))
            numWalls++;

        if (HasWall(Wall.Right))
            numWalls++;

        if (HasWall(Wall.Down))
            numWalls++;

        return numWalls == 3;
    }
    #endregion
}

}