using UnityEngine;

namespace Labyrinth.Menu {

    public class MainMenu : MonoBehaviour, Core.IEventListener {

        #region private fields
        private StartButton _startButton;

        private SettingsButton _settingsButton;

        private ExitButton _exitButton;
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

            _settingsButton = GetComponentInChildren<SettingsButton>();
            _settingsButton.gameObject.SetActive(false);

            _exitButton = GetComponentInChildren<ExitButton>();
            _exitButton.gameObject.SetActive(false);

            AddListeners();
        }

        private void OnDestroy() {
            RemoveListeners();
        }

        private void OnCameraMoved(Events.Menu.CameraMoved e) {
            if (e.NewState == MenuState.MainMenu) {
                _startButton.gameObject.SetActive(true);
                _settingsButton.gameObject.SetActive(true);
                _exitButton.gameObject.SetActive(true);
            }
        }
        #endregion
    }

}