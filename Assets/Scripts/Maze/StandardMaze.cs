using Maze;
using UnityEngine;

namespace Maze {

    public class StandardMaze : MonoBehaviour, IMaze {

        #region private fields
        [SerializeField] private MazeAssets _assets;

        private Maze<Cell> _maze;
        #endregion

        #region public methods
        public void CreateMaze(MazeSettings settings) {
            var mazeFactory = GetFactoryFromSettings(settings);
            _maze = mazeFactory.CreateMaze(settings.Length, settings.Width, settings.CellSize);

            var mazeSpawner = new MazeSpawner(gameObject.transform, _assets);
            mazeSpawner.SpawnMaze(_maze, settings);
        }

        public void SetupChildren() {
            foreach (Transform child in gameObject.transform) {
                if (child == null) {
                    continue;
                }

                child.gameObject.layer = gameObject.layer;

                if (child.TryGetComponent(out Wall _))
                    child.transform.localScale *= _maze.CellSize;
            }
        }
        #endregion

        #region private methods
        private Factory.IMazeFactory GetFactoryFromSettings(MazeSettings settings) {
            switch (settings.Algorithm) {
                case GenerationAlgorithm.DepthFirstSearch:
                    return new Factory.DFSFactory();

                case GenerationAlgorithm.Prim:
                    return new Factory.PrimFactory();

                default:
                    return null;
            }
        }
       #endregion
    }

}
