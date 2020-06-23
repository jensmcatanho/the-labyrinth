using UnityEngine;

public class MazeBehaviour : MonoBehaviour {

    #region private variables
    [SerializeField] private MazeSettings _mazeSettings;

    private IMazeFactory _mazeFactory;

    private MazeRenderer _renderer;

    private Maze<Cell> _maze;
    #endregion

    #region private methods
    private void Awake() {
        _mazeFactory = new DFSMazeFactory();
        _maze = _mazeFactory.CreateMaze(_mazeSettings.Width, _mazeSettings.Height, _mazeSettings.CellSize);
        _renderer = transform.GetComponentInChildren<MazeRenderer>();
    }

    private void Start() {
        _renderer.Render(_maze);
    }
    #endregion
}