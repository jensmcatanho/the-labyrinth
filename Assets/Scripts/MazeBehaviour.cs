using UnityEngine;
using Maze;

public class MazeBehaviour : MonoBehaviour {

    #region private variables
    [SerializeField] private MazeSettings _mazeSettings;

    private Maze.Factory.IMazeFactory _mazeFactory;

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
                _mazeFactory = new Maze.Factory.DFSFactory();
                break;

            case GenerationAlgorithm.Prim:
                _mazeFactory = new Maze.Factory.PrimFactory();
                break;
        }
    }
    #endregion
}