using UnityEngine;
using UnityEngine.Events;

public class MazeBehaviour : MonoBehaviour {

    [SerializeField] private MazeSettings _mazeSettings;

    private MazeRenderer _renderer;
    private Maze<Cell> _maze;

    private void Awake() {
        DFSMazeFactory mazeFactory = new DFSMazeFactory();
        _maze = mazeFactory.CreateMaze(_mazeSettings.Width, _mazeSettings.Height, _mazeSettings.CellSize);

        _renderer = transform.GetComponentInChildren<MazeRenderer>();
    }

    private void Start() {
        _renderer.Render(_maze);
    }
}