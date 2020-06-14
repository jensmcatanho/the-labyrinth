using UnityEngine;
using UnityEngine.Events;

public class MazeBehaviour : MonoBehaviour {

    [SerializeField] private MazeSettings _mazeSettings;

    private MazeRenderer _renderer;
    private Maze<Cell> _maze;

    public MazeCreatedEvent _mazeCreatedEvent = null;

    void Awake() {
        DFSMazeFactory mazeFactory = new DFSMazeFactory();
        _maze = mazeFactory.CreateMaze(_mazeSettings.Width, _mazeSettings.Height, _mazeSettings.CellSize);

        _renderer = transform.GetComponentInChildren<MazeRenderer>();
        _renderer.Render(_maze);
    }

    void Start() {

    }

    void Update() {
        
    }

    public MazeCreatedEvent MazeCreated {
        get { return _mazeCreatedEvent; }
    }
}


[System.Serializable]
public class MazeCreatedEvent : UnityEvent<Maze<Cell>>
{

}