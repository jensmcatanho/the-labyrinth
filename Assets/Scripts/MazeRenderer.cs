using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeRenderer : MonoBehaviour {

    #region public variables
    public GameObject _wallPrefab;

	public GameObject _chestPrefab;
    #endregion

    private GameObject _maze;

	#region private variables
	private readonly Quaternion _leftWallRotation = Quaternion.Euler(90f, 90f, 0f);

	private readonly Quaternion _downWallRotation = Quaternion.Euler(90f, 0f, 0f);

	private readonly Quaternion _upWallRotation = Quaternion.Euler(90f, 0f, 180f);

	private readonly Quaternion _rightWallRotation = Quaternion.Euler(90f, -90f, 0f);

	#endregion

	#region public methods


	public void Render(Maze<Cell> maze) {
		_maze = transform.parent.gameObject;

		InstantiateFloor(maze);
		InstantiateWalls(maze);
		InstantiateFinishTrigger(maze);
	}
    #endregion

    #region private methods
    private void InstantiateFloor(Maze<Cell> maze) {
		GameObject floor = GameObject.CreatePrimitive(PrimitiveType.Plane);

		floor.transform.parent = _maze.transform;
		floor.transform.localScale = new Vector3(maze.Length * 0.2f * maze.CellSize, 1, maze.Width * 0.2f * maze.CellSize);
		floor.transform.position = new Vector3(maze.Length * maze.CellSize, 0, maze.Width * maze.CellSize);
	}

	private void InstantiateWalls(Maze<Cell> maze) {
        /*
		 *  Note that the maze matrix is not traversed like we normally would do. Instead of instantiating every cell of each row in order, we
		 *  first instatiate cells from the diagonal. Next, we instantiate cells from the lower and upper triangular parts of the maze separately.
		 *  This way we ensure that we are not instantiating a wall south of the cell on the same place we have already instantiated a wall north
		 *  of the cell right below.
		 */
        InstantiateDiagonal(maze);

        for (int i = 0; i < maze.Length; i++)
            for (int j = i + 1; j < maze.Width; j++) {
                InstantiateLowerTriangular(maze, i, j);
                InstantiateChest(maze, j, i);

                InstantiateUpperTriangular(maze, i, j);
				InstantiateChest(maze, i, j);
            }
    }

    private void InstantiateUpperTriangular(Maze<Cell> maze, int i, int j) {
        if (maze[i, j].HasWall(Wall.Up))
            InstantiateWall(maze, new Vector3(2 * i * maze.CellSize, 2.0f, (2 * j + 1) * maze.CellSize), _upWallRotation);

        if (maze[i, j].HasWall(Wall.Right))
            InstantiateWall(maze, new Vector3((2 * i + 1) * maze.CellSize, 2.0f, (2 * j + 2) * maze.CellSize), _rightWallRotation);
    }

    private void InstantiateLowerTriangular(Maze<Cell> maze, int i, int j) {
        if (maze[j, i].HasWall(Wall.Left))
            InstantiateWall(maze, new Vector3((2 * j + 1) * maze.CellSize, 2.0f, 2 * i * maze.CellSize), _leftWallRotation);

        if (maze[j, i].HasWall(Wall.Down))
            InstantiateWall(maze, new Vector3((2 * j + 2) * maze.CellSize, 2.0f, (2 * i + 1) * maze.CellSize), _downWallRotation);
    }

    private void InstantiateDiagonal(Maze<Cell> maze) {
        for (int i = 0; i < maze.Length; i++) {
            if (maze[i, i].HasWall(Wall.Left))
                InstantiateWall(maze, new Vector3((2 * i + 1) * maze.CellSize, 2.0f, 2 * i * maze.CellSize), _leftWallRotation);

            if (maze[i, i].HasWall(Wall.Down))
                InstantiateWall(maze, new Vector3((2 * i + 2) * maze.CellSize, 2.0f, (2 * i + 1) * maze.CellSize), _downWallRotation);

            if (maze[i, i].HasWall(Wall.Up))
                InstantiateWall(maze, new Vector3(2 * i * maze.CellSize, 2.0f, (2 * i + 1) * maze.CellSize), _upWallRotation);

            if (maze[i, i].HasWall(Wall.Right))
                InstantiateWall(maze, new Vector3((2 * i + 1) * maze.CellSize, 2.0f, (2 * i + 2) * maze.CellSize), _rightWallRotation);
        }
    }

    private void InstantiateWall(Maze<Cell> maze, Vector3 position, Quaternion rotation) {
		GameObject wall = Instantiate(_wallPrefab, position, rotation);

		wall.transform.parent = _maze.transform;
		wall.transform.localScale *= maze.CellSize;
	}

    private void InstantiateChest(Maze<Cell> maze, int row, int col) {
		if (!maze[row, col].HasChest)
			return;

		Vector3 chestPosition = new Vector3((2 * row + 1) * maze.CellSize, 0.0f, (2 * col + 1) * maze.CellSize);
		Quaternion chestRotation = GetChestRotation(maze[row, col]);

		GameObject chest = Instantiate(_chestPrefab, chestPosition, chestRotation);
		chest.transform.parent = _maze.transform;
	}

	private Quaternion GetChestRotation(Cell cell) {
		switch (cell.DeadEndOpening()) {
			case Wall.Left:
				return Quaternion.Euler(0f, 180f, 0f);

			case Wall.Up:
				return Quaternion.Euler(0f, -90f, 0f);

			case Wall.Down:
				return Quaternion.Euler(0f, 90f, 0f);

			default:
				return Quaternion.identity;
		}

		/*
		 * Switch expressions are not yet supported by the Unity C# compiler. :(
        return (cell.DeadEndOpening()) switch {
            Wall.Left => Quaternion.Euler(0f, 180f, 0f),
            Wall.Up => Quaternion.Euler(0f, -90f, 0f),
            Wall.Down => Quaternion.Euler(0f, 90f, 0f),
            _ => Quaternion.identity
        };
		*/
    }

	private void InstantiateFinishTrigger(Maze<Cell> maze) {
		GameObject finishTrigger = new GameObject("Finish Trigger");
		finishTrigger.AddComponent<FinishTrigger>();
		finishTrigger.AddComponent<BoxCollider>().isTrigger = true;
		finishTrigger.transform.parent = _maze.transform;

		if (maze.Exit.Position.X >= maze.Exit.Position.Y) {
			finishTrigger.transform.position = new Vector3 ((2 * maze.Exit.Position.X + 4) * maze.CellSize - 2 * maze.CellSize, 1.0f, (2 * maze.Exit.Position.Y + 1) * maze.CellSize);
			finishTrigger.transform.localScale = new Vector3(.5f * maze.CellSize, 2f * maze.CellSize, 1.5f * maze.CellSize);

		} else {
			finishTrigger.transform.position = new Vector3 ((2 * maze.Exit.Position.X + 1) * maze.CellSize, 1.0f, (2 * maze.Exit.Position.Y + 4) * maze.CellSize - 2 * maze.CellSize);
			finishTrigger.transform.localScale = new Vector3(2f * maze.CellSize, 2.0f * maze.CellSize, maze.CellSize * 0.5f);
		}
	}
    #endregion
}