using UnityEngine;
using Labyrinth.Menu.UIElements;

namespace Labyrinth.Menu {

    public class MainMenu : MonoBehaviour {

        #region private fields
        private Canvas _canvas;

        private StartButton _startButton;

        private SettingsButton _settingsButton;

        private ExitButton _exitButton;
        #endregion

        #region public methods
        public void SetActive(bool target) {
            _canvas.gameObject.SetActive(target);
        }
        #endregion

        #region private methods
        private void Awake() {
            _canvas = GetComponentInChildren<Canvas>(true);
            _startButton = GetComponentInChildren<StartButton>(true);
            _settingsButton = GetComponentInChildren<SettingsButton>(true);
            _exitButton = GetComponentInChildren<ExitButton>(true);

            SetActive(false);
        }
        #endregion

    }

}