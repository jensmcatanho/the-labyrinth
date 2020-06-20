using UnityEngine;
using UnityEngine.Events;

public class MazeBehaviour : MonoBehaviour {

    #region private variables
    [SerializeField] private MazeSettings _mazeSettings;

    private MazeRenderer _renderer;

    private Maze<Cell> _maze;
    #endregion

    #region private methods
    private void Awake() {
        _maze = DFSMazeFactory.CreateMaze(_mazeSettings.Width, _mazeSettings.Height, _mazeSettings.CellSize);

        _renderer = transform.GetComponentInChildren<MazeRenderer>();
    }

    private void Start() {
        _renderer.Render(_maze);
    }
    #endregion
}