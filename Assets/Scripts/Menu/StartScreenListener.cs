using UnityEngine;
using Labyrinth.Menu;

namespace Menu {

    public class StartScreenListener : MonoBehaviour, Core.IEventListener {

        private StartScreen _startScreen;

        #region public methods
        public void AddListeners() {
            Core.EventManager.Instance.AddListener<Events.Menu.CameraMoved>(OnCameraMoved);
        }

        public void RemoveListeners() {
            Core.EventManager.Instance.RemoveListener<Events.Menu.CameraMoved>(OnCameraMoved);
        }
        #endregion


        #region private methods
        private void Awake() {
            AddListeners();

            TryGetComponent(out _startScreen);
        }

        private void OnCameraMoved(Events.Menu.CameraMoved e) {
            if (e.NewState == MenuState.StartScreen)
                _startScreen.EnableUIElements();
        }
        #endregion
    }

}