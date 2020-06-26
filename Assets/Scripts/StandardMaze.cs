using UnityEngine;
using Maze;
using System.Collections;

public class StandardMaze : MonoBehaviour, IMaze {

    #region private variables
    [SerializeField] private MazeSettings _mazeSettings;

    private MazeRenderer _renderer;

    private Maze<Cell> _maze;
    #endregion

    #region private methods
    private void Awake() {
        var mazeFactory = GetFactoryFromSettings();

        _maze = mazeFactory.CreateMaze(_mazeSettings.Width, _mazeSettings.Height, _mazeSettings.CellSize);
        _renderer = transform.GetComponentInChildren<MazeRenderer>();
    }

    private void Start() {
        _renderer.Render(_maze);

    }

    private Maze.Factory.IMazeFactory GetFactoryFromSettings() {
        switch (_mazeSettings.Algorithm) {
            case GenerationAlgorithm.DepthFirstSearch:
                return new Maze.Factory.DFSFactory();

            case GenerationAlgorithm.Prim:
                return new Maze.Factory.PrimFactory();

            default:
                return null;
        }

    }
    #endregion
}