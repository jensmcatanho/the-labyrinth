using UnityEngine;

namespace Labyrinth.Menu {

    public class MainMenuListener : MonoBehaviour, Core.IEventListener {

        private MainMenu _mainMenu;

        #region public methods
        public void AddListeners() {
            Core.EventManager.Instance.AddListener<Events.Menu.CameraMoved>(OnCameraMoved);
            Core.EventManager.Instance.AddListener<Events.Menu.SettingsButtonClicked>(OnSettingsButtonClicked);
            Core.EventManager.Instance.AddListenerOnce((Events.Menu.ExitButtonClicked e) => {
                Application.Quit();
            });
        }

        public void RemoveListeners() {
            if (Core.EventManager.Instance)
                Core.EventManager.Instance.RemoveListener<Events.Menu.CameraMoved>(OnCameraMoved);
        }
        #endregion

        #region private methods
        private void Awake() {
            if (TryGetComponent(out _mainMenu))
                AddListeners();
        }

        private void OnDestroy() {
            RemoveListeners();
        }

        private void OnCameraMoved(Events.Menu.CameraMoved e) {
            if (e.NewState == MenuState.MainMenu)
                _mainMenu.SetActive(true);
        }

        private void OnSettingsButtonClicked(Events.Menu.SettingsButtonClicked e) {
            _mainMenu.SetActive(false);
        }
        #endregion

    }

}

