﻿using UnityEngine;

[System.Flags]
public enum Wall {
	None = 0,
	Left = 1 << 0,
	Up = 1 << 1,
	Right = 1 << 2,
	Down = 1 << 3

}

[System.Flags]
public enum CellType {
	None = 0,
	Entrance = 1 << 0,
	Exit = 1 << 1
}

public abstract class Cell {
	private CellType _type;

	private Wall _walls;

	private Vector2 _position;

    public Cell(int x, int y, int size) {
		_position = new Vector2(x, y);
		Size = size;
		_type = CellType.None;

		SetAllWalls();
	}

	public void SetAllWalls() {
		SetWall(Wall.Left);
		SetWall(Wall.Up);
		SetWall(Wall.Right);
		SetWall(Wall.Down);
	}

	public Vector2 Position {
		get { return _position; }
		set { _position = value; }
	}

    public int Size { get; }

    public bool HasChest { get; set; }

    public void SetWall(Wall target) {
		_walls |= target;
	}

	public void UnsetWall(Wall target) {
		_walls &= ~target;
	}

	public void ToggleWall(Wall target) {
		_walls ^= target;
	}

	public bool HasWall(Wall target) {
		return (_walls & target) == target;
	}

	public void SetType(CellType target) {
		_type |= target;
	}

	public void UnsetType(CellType target) {
		_type &= (~target);
	}

	public void ToggleType(CellType target) {
		_type ^= target;
	}

	public Wall DeadEndOpening() {
		if (!IsDeadEnd()) {
			return Wall.None;
		}

		if (!HasWall(Wall.Left)) {
			return Wall.Left;

		} else if (!HasWall(Wall.Up)) {
			return Wall.Up;

		} else if (!HasWall(Wall.Right)) {
			return Wall.Right;

		} else if (!HasWall(Wall.Down)) {
			return Wall.Down;
		}

		return Wall.None;
	}

	public bool IsDeadEnd() {
		int numWalls = 0;

		if (HasWall(Wall.Left))
			numWalls++;

		if (HasWall(Wall.Up))
			numWalls++;

		if (HasWall(Wall.Right))
			numWalls++;

		if (HasWall(Wall.Down))
			numWalls++;

		return numWalls == 3;
	}
}