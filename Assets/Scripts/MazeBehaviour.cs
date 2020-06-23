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
        SetMazeFactory();

        _maze = _mazeFactory.CreateMaze(_mazeSettings.Width, _mazeSettings.Height, _mazeSettings.CellSize);
        _renderer = transform.GetComponentInChildren<MazeRenderer>();
    }

    private void Start() {
        _renderer.Render(_maze);
    }

    private void SetMazeFactory() {
        switch (_mazeSettings.Algorithm) {
            case GenerationAlgorithm.DepthFirstSearch:
                _mazeFactory = new DFSMazeFactory();
                break;

            case GenerationAlgorithm.Prim:
                _mazeFactory = new PrimMazeFactory();
                break;
        }
    }
    #endregion
}