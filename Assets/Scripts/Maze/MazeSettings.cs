using UnityEngine;

[CreateAssetMenu(menuName = "Maze/Settings")]
public class MazeSettings : ScriptableObject {
    #region private variables
    [SerializeField] private int _width = 50;

    [SerializeField] private int _height = 50;

    [SerializeField] private int _cellSize = 5;
    #endregion

    #region public methods
    public int Width {
        get {
            return _width;
        }
    }
    public int Height {
        get {
            return _height;
        }
    }
    public int CellSize {
        get {
            return _cellSize;
        }
    }
    #endregion
}
