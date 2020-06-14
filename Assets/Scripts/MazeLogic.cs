using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class MazeCreatedEvent : UnityEvent<Maze<Cell>>
{

}

public class MazeLogic : MonoBehaviour {

	public MazeCreatedEvent _mazeCreatedEvent;
    
    void Start() {

    }

    void Update() {
        
    }

    public void OnGameStart() {
        DFSMazeFactory mazeFactory = new DFSMazeFactory();
        Maze<Cell> maze = mazeFactory.CreateMaze(50, 50, 5);

        _mazeCreatedEvent.Invoke(maze);
    }
}
