using UnityEngine;

namespace Labyrinth.Menu {

    public class MenuBehaviour : MonoBehaviour, Core.IEventListener {

        #region private fields
        private StartButton _startButton;
        #endregion

        #region public methods
        public void AddListeners() {
            Core.EventManager.Instance.AddListener<Events.Menu.CameraMoved>(OnCameraMoved);
        }

        public void RemoveListeners() {
            if (Core.EventManager.Instance)
                Core.EventManager.Instance.RemoveListener<Events.Menu.CameraMoved>(OnCameraMoved);
        }
        #endregion

        #region private methods
        private void Awake() {
            _startButton = GetComponentInChildren<StartButton>();
            _startButton.gameObject.SetActive(false);

            AddListeners();
        }

        private void OnDestroy() {
            RemoveListeners();
        }

        private void OnCameraMoved(Events.Menu.CameraMoved e) {
            if (e.NewState == MenuState.MainMenu)
                _startButton.gameObject.SetActive(true);
        }
        #endregion
    }

}