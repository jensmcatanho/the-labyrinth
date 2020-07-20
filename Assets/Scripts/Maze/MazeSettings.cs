using UnityEngine;

namespace Labyrinth.Maze {

    public enum GenerationAlgorithm {
        DepthFirstSearch,
        Prim
    }

    [CreateAssetMenu(menuName = "Maze/Settings")]
    public class MazeSettings : ScriptableObject {
        #region private fields
        [SerializeField] private int _length;

        [SerializeField] private int _width;

        [SerializeField] private int _cellSize;

        [SerializeField] private GenerationAlgorithm _algorithm;

        [SerializeField] private bool _isPlayable;
        #endregion

        #region public methods
        public int Length {
            get {
                return _length;
            }
        }

        public int Width {
            get {
                return _width;
            }
        }

        public int CellSize {
            get {
                return _cellSize;
            }
        }

        public GenerationAlgorithm Algorithm {
            get {
                return _algorithm;
            }
        }

        public bool IsPlayable {
            get {
                return _isPlayable;
            }
        }
        #endregion
    }

}