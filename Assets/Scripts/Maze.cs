[System.Serializable]
public class Maze<T> where T : Cell {

    #region private variables
    private int _length;

	private int _width;

	private T[,] _cells;

	private T _entrance;

	private T _exit;
    #endregion

    #region constructor
    public Maze(int length, int width, int cellSize) {
		_length = length;
		_width = width;
		_cells = new T[length, width];
		_entrance = null;
		_exit = null;
	}
    #endregion

    #region public methods
    public int Length {
		get { return _length; }
	}

	public int Width {
		get { return _width; }
	}

	public int CellSize {
		get { return _cells[0, 0].Size; }
	}

	public T this[int row, int col] {
		get { return _cells[row, col]; }
		set { _cells[row, col] = value; }
	}

	public T Entrance {
		get { return _entrance; }
		set { _entrance = value; }
	}

	public T Exit {
		get { return _exit; }
		set { _exit = value; }
	}
	#endregion
}