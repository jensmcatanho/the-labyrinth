using UnityEngine;

namespace Maze {

    public class MazeListener : MonoBehaviour, Core.IEventListener {

        private IMaze _maze;

        #region public methods
        public void AddListeners() {
            Core.EventManager.Instance.AddListenerOnce<Events.GameSceneLoaded>(OnSceneLoaded);
            Core.EventManager.Instance.AddListenerOnce<Events.MenuSceneLoaded>(OnSceneLoaded);
            Core.EventManager.Instance.AddListener<Events.MazeInstanced>(OnMazeInstanced);
        }

        public void RemoveListeners() {
            if (Core.EventManager.Instance) {
                Core.EventManager.Instance.RemoveListener<Events.GameSceneLoaded>(OnSceneLoaded);
                Core.EventManager.Instance.RemoveListener<Events.MenuSceneLoaded>(OnSceneLoaded);
                Core.EventManager.Instance.RemoveListener<Events.MazeInstanced>(OnMazeInstanced);
            }
        }
        #endregion

        #region private methods
        private void Awake() {
            TryGetComponent(out _maze);

            AddListeners();
        }

        private void OnDestroy() {
            RemoveListeners();
        }

        private void OnSceneLoaded(Events.MenuSceneLoaded e) {
            _maze.CreateMaze(e.MazeSettings);
        }

        private void OnSceneLoaded(Events.GameSceneLoaded e) {
            _maze.CreateMaze(e.MazeSettings);
        }

        private void OnMazeInstanced(Events.MazeInstanced e) {
            _maze.SetupChildren();
        }
        #endregion

    }

}