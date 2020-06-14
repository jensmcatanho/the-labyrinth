using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeRenderer : MonoBehaviour {

	public GameObject _wallPrefab;
	public GameObject _chestPrefab;

	private GameObject _maze;

	public void Render(Maze<Cell> maze) {
		_maze = transform.parent.gameObject;

		InstantiateFloor(maze);

		for (int i = 0; i < maze.Length; i++) {
			if (maze[i, i].HasWall(Wall.Left))
				InstantiateWall(maze, new Vector3((2 * i + 1) * maze.CellSize, 2.0f, 2 * i * maze.CellSize), new Vector3(90.0f, 90.0f, 0.0f));

			if (maze[i, i].HasWall(Wall.Down))
				InstantiateWall(maze, new Vector3((2 * i + 2) * maze.CellSize, 2.0f, (2 * i + 1) * maze.CellSize), new Vector3(90.0f, 0.0f, 0.0f));

			if (maze[i, i].HasWall(Wall.Up))
				InstantiateWall(maze, new Vector3(2 * i * maze.CellSize, 2.0f, (2 * i + 1) * maze.CellSize), new Vector3(90.0f, 0.0f, 180.0f));

			if (maze[i, i].HasWall(Wall.Right))
				InstantiateWall(maze, new Vector3((2 * i + 1) * maze.CellSize, 2.0f, (2 * i + 2) * maze.CellSize), new Vector3(90.0f, -90.0f, 0.0f));
		}

		for (int i = 0; i < maze.Length; i++)
		{
			for (int j = i + 1; j < maze.Width; j++)
			{
				// Create walls and chests in the lower triangular part of the maze.
				if (maze[j, i].HasWall(Wall.Left))
					InstantiateWall(maze, new Vector3((2 * j + 1) * maze.CellSize, 2.0f, 2 * i * maze.CellSize), new Vector3(90.0f, 90.0f, 0.0f));

				if (maze[j, i].HasWall(Wall.Down))
					InstantiateWall(maze, new Vector3((2 * j + 2) * maze.CellSize, 2.0f, (2 * i + 1) * maze.CellSize), new Vector3(90.0f, 0.0f, 0.0f));

				if (maze[j, i].HasChest) {
					//CreateChest(maze, j, i);
				}

				// Create walls in the upper triangular part of the maze.
				if (maze[i, j].HasWall(Wall.Up))
					InstantiateWall(maze, new Vector3(2 * i * maze.CellSize, 2.0f, (2 * j + 1) * maze.CellSize), new Vector3(90.0f, 0.0f, 180.0f));

				if (maze[i, j].HasWall(Wall.Right))
					InstantiateWall(maze, new Vector3((2 * i + 1) * maze.CellSize, 2.0f, (2 * j + 2) * maze.CellSize), new Vector3(90.0f, -90.0f, 0.0f));

				if (maze[i, j].HasChest) {
					//CreateChest(maze, i, j);
				}
			}
		}

		//CreateFinish(maze);
		//Core.EventManager.Instance.QueueEvent(new Events.MazeRendered(mazeObject.GetComponent<Maze>()));
	}

	private void InstantiateFloor(Maze<Cell> maze) {
		GameObject floor = GameObject.CreatePrimitive(PrimitiveType.Plane);

		floor.transform.parent = _maze.transform;
		floor.transform.localScale = new Vector3(maze.Length * 0.2f * maze.CellSize, 1, maze.Width * 0.2f * maze.CellSize);
		floor.transform.position = new Vector3(maze.Length * maze.CellSize, 0, maze.Width * maze.CellSize);
	}

	private void InstantiateWall(Maze<Cell> maze, Vector3 position, Vector3 rotation) {
		Quaternion r = Quaternion.identity;
		r.eulerAngles = rotation;

		GameObject wall = Instantiate(_wallPrefab, position, r) as GameObject;
		wall.transform.parent = _maze.transform;
		wall.transform.localScale *= maze.CellSize;
	}
}


/*
	public void RenderMaze(Gameplay.Events.MazeGenerated e)
	{
		CreateMaze(e.m_Maze);
#if UNITY_EDITOR
		ASCIIFactory.Render(e.m_Maze, "x");
#endif
	}

	void CreateMaze(Gameplay.Maze<Gameplay.Cell> maze)
	{
		mazeObject = new GameObject("Labyrinth");
		mazeObject.AddComponent<Maze>();
		CreateFloor(maze);

		// Create walls in the diagonal part of the maze.
		for (int i = 0; i < maze.Length; i++)
		{
			if (maze[i, i].HasWall(Gameplay.Wall.Left))
				CreateWall(maze, new Vector3((2 * i + 1) * maze.CellSize, 2.0f, 2 * i * maze.CellSize), new Vector3(90.0f, 90.0f, 0.0f));

			if (maze[i, i].HasWall(Gameplay.Wall.Down))
				CreateWall(maze, new Vector3((2 * i + 2) * maze.CellSize, 2.0f, (2 * i + 1) * maze.CellSize), new Vector3(90.0f, 0.0f, 0.0f));

			if (maze[i, i].HasWall(Gameplay.Wall.Up))
				CreateWall(maze, new Vector3(2 * i * maze.CellSize, 2.0f, (2 * i + 1) * maze.CellSize), new Vector3(90.0f, 0.0f, 180.0f));

			if (maze[i, i].HasWall(Gameplay.Wall.Right))
				CreateWall(maze, new Vector3((2 * i + 1) * maze.CellSize, 2.0f, (2 * i + 2) * maze.CellSize), new Vector3(90.0f, -90.0f, 0.0f));
		}

		for (int i = 0; i < maze.Length; i++)
		{
			for (int j = i + 1; j < maze.Width; j++)
			{
				// Create walls and chests in the lower triangular part of the maze.
				if (maze[j, i].HasWall(Gameplay.Wall.Left))
					CreateWall(maze, new Vector3((2 * j + 1) * maze.CellSize, 2.0f, 2 * i * maze.CellSize), new Vector3(90.0f, 90.0f, 0.0f));

				if (maze[j, i].HasWall(Gameplay.Wall.Down))
					CreateWall(maze, new Vector3((2 * j + 2) * maze.CellSize, 2.0f, (2 * i + 1) * maze.CellSize), new Vector3(90.0f, 0.0f, 0.0f));

				if (maze[j, i].HasChest)
				{
					CreateChest(maze, j, i);
					mazeObject.GetComponent<Maze>().NChests++;
				}

				// Create walls in the upper triangular part of the maze.
				if (maze[i, j].HasWall(Gameplay.Wall.Up))
					CreateWall(maze, new Vector3(2 * i * maze.CellSize, 2.0f, (2 * j + 1) * maze.CellSize), new Vector3(90.0f, 0.0f, 180.0f));

				if (maze[i, j].HasWall(Gameplay.Wall.Right))
					CreateWall(maze, new Vector3((2 * i + 1) * maze.CellSize, 2.0f, (2 * j + 2) * maze.CellSize), new Vector3(90.0f, -90.0f, 0.0f));

				if (maze[i, j].HasChest)
				{
					CreateChest(maze, i, j);
					mazeObject.GetComponent<Maze>().NChests++;
				}
			}
		}

		CreateFinish(maze);
		Core.EventManager.Instance.QueueEvent(new Events.MazeRendered(mazeObject.GetComponent<Maze>()));
		GameObject.Destroy(gameObject);
	}

	void CreateFloor(Gameplay.Maze<Gameplay.Cell> maze)
	{
		GameObject floor = GameObject.CreatePrimitive(PrimitiveType.Plane);
		floor.transform.parent = mazeObject.transform;
		floor.transform.localScale = new Vector3(maze.Length * 0.2f * maze.CellSize, 1, maze.Width * 0.2f * maze.CellSize);
		floor.transform.position = new Vector3(maze.Length * maze.CellSize, 0, maze.Width * maze.CellSize);
	}

	void CreateWall(Gameplay.Maze<Gameplay.Cell> maze, Vector3 position, Vector3 rotation)
	{
		Quaternion r = Quaternion.identity;
		r.eulerAngles = rotation;

		GameObject wall = Instantiate(wallPrefab, position, r) as GameObject;
		wall.transform.parent = mazeObject.transform;
		wall.transform.localScale *= maze.CellSize;
	}

	void CreateChest(Gameplay.Maze<Gameplay.Cell> maze, int row, int col)
	{
		Quaternion r = Quaternion.identity;
		Vector3 chestPosition = new Vector3((2 * row + 1) * maze.CellSize, 0.0f, (2 * col + 1) * maze.CellSize);
		Vector3 chestRotation = new Vector3();

		// Check which direction the chest is facing (which direction of the cell is open).
		switch (maze[row, col].DeadEndOpening())
		{
			case Gameplay.Wall.Left:
				chestRotation = new Vector3(0.0f, 180.0f, 0.0f);
				break;

			case Gameplay.Wall.Up:
				chestRotation = new Vector3(0.0f, -90.0f, 0.0f);
				break;

			case Gameplay.Wall.Right:
				chestRotation = new Vector3(0.0f, 0.0f, 0.0f);
				break;

			case Gameplay.Wall.Down:
				chestRotation = new Vector3(0.0f, 90.0f, 0.0f);
				break;

		}

		r.eulerAngles = chestRotation;
		GameObject chest = Instantiate(chestPrefab, chestPosition, r);
		chest.transform.parent = mazeObject.transform;
	}

	void CreateFinish(Gameplay.Maze<Gameplay.Cell> maze)
	{
		GameObject finishTrigger = new GameObject("FinishTrigger");
		finishTrigger.AddComponent<FinishPoint>();
		finishTrigger.AddComponent<BoxCollider>().isTrigger = true;
		finishTrigger.transform.parent = mazeObject.transform;

		if (maze.Exit.Position.x >= maze.Exit.Position.y)
		{
			finishTrigger.transform.position = new Vector3((2 * maze.Exit.Position.x + 4) * maze.CellSize - 2 * maze.CellSize, 1.0f, (2 * maze.Exit.Position.y + 1) * maze.CellSize);
			finishTrigger.transform.localScale = new Vector3(.5f * maze.CellSize, 2.0f * maze.CellSize, 1.5f * maze.CellSize);

		}
		else
		{
			finishTrigger.transform.position = new Vector3((2 * maze.Exit.Position.x + 1) * maze.CellSize, 1.0f, (2 * maze.Exit.Position.y + 4) * maze.CellSize - 2 * maze.CellSize);
			finishTrigger.transform.localScale = new Vector3(1.5f * maze.CellSize, 2.0f * maze.CellSize, maze.CellSize * 0.5f);
		}
	}
}

namespace Events
{
	public class MazeRendered : Core.Events.GameEvent
	{
		public Maze maze
		{
			get;
			private set;
		}

		public MazeRendered(Maze m)
		{
			maze = m;
		}
	}
}

}

*/