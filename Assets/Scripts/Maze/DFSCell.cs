namespace Maze {

    public class DFSCell : Cell {

        #region constructor
        public DFSCell(int x, int y, int size)
            : base(x, y, size) {

            IsVisited = false;
        }
        #endregion

        #region public methods
        public bool IsVisited {
            get;
            set;
        }
        #endregion
    }

}