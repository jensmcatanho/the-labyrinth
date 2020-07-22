using UnityEngine;

namespace Labyrinth.Menu.Settings {

    public class SettingsMenuListener : MonoBehaviour, Core.IEventListener {

        private SettingsMenu _settingsMenu;

        #region public methods
        public void AddListeners() {
            Core.EventManager.Instance.AddListener<Events.Menu.CameraMoved>(OnCameraMoved);
        }

        public void RemoveListeners() {
            if (Core.EventManager.Instance)
                Core.EventManager.Instance.AddListener<Events.Menu.CameraMoved>(OnCameraMoved);

        }
        #endregion

        #region private methods
        private void Awake() {
            if (TryGetComponent(out _settingsMenu))
                AddListeners();
        }

        private void OnCameraMoved(Events.Menu.CameraMoved e) {
            if (e.NewState == MenuState.Settings)
                _settingsMenu.SetActive(true);
        }
        #endregion

    }

}