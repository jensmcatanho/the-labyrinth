public class DFSCell : Cell {
    public DFSCell(int x, int y, int size)
        : base(x, y, size) {

        IsVisited = false;
    }

    public bool IsVisited { get; set; }
}