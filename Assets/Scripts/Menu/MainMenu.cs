using UnityEngine;
using Labyrinth.Menu.UIElements;

namespace Labyrinth.Menu {

    public class MainMenu : MonoBehaviour {

        #region private fields
        private StartButton _startButton;

        private SettingsButton _settingsButton;

        private ExitButton _exitButton;
        #endregion

        #region public methods
        public void SetActive(bool target) {
            _startButton.gameObject.SetActive(target);
            _settingsButton.gameObject.SetActive(target);
            _exitButton.gameObject.SetActive(target);
        }
        #endregion

        #region private methods
        private void Awake() {
            _startButton = GetComponentInChildren<StartButton>();
            _settingsButton = GetComponentInChildren<SettingsButton>();
            _exitButton = GetComponentInChildren<ExitButton>();

            SetActive(false);
        }
        #endregion

    }

}