
/*
 * This status specifies if the cell was already visited by the algorithm,
 * or if it is a neighbor of a visited cell.
 */
public enum PrimStatus {
    None = 0,
    Visited = 1,
    Neighbor = 2,
}

public class PrimCell : Cell {

    private PrimStatus _status;

    public bool IsVisited() => _status == PrimStatus.Visited;

    public bool IsNone() => _status == PrimStatus.None;

    public bool IsNeighbor() => _status == PrimStatus.Neighbor;

    public PrimStatus Status {
        set {
            _status = value;
        }
    }
    
    public PrimCell(int x, int y, int size)
        : base(x, y, size) {

        _status = PrimStatus.None;
    }
}