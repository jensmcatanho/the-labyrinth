using UnityEngine;
using UnityEngine.UI;

namespace Labyrinth.Menu.Settings {

    using UIElements;

    public class SettingsMenu : MonoBehaviour {

        [SerializeField] private Button _backButton;

        [SerializeField] private Button _audioButton;

        [SerializeField] private GameObject _panel;

        private AudioSettings _audioSettings;

        #region public fields
        public void SetActive(bool target) {
            _panel.SetActive(target);
            _backButton.gameObject.SetActive(target);
            _audioButton.gameObject.SetActive(target);
        }

        public void Close() {
            _panel.SetActive(false);
            _backButton.gameObject.SetActive(false);
            _audioButton.gameObject.SetActive(false);
            _audioSettings.gameObject.SetActive(false);
        }
        #endregion

        #region private fields
        private void Awake() {
            SetupBackButton();
            SetupAudioSettings();

            Close();
        }

        private void SetupBackButton() {
            _backButton.onClick.AddListener(() => {
                Core.EventManager.Instance.TriggerEvent(new Events.Menu.BackToMenuClicked());
                Close();
            });
        }

        private void SetupAudioSettings() {
            _audioSettings = GetComponentInChildren<UIElements.AudioSettings>();
            _audioButton.onClick.AddListener(() => {
                _audioSettings.gameObject.SetActive(!_audioSettings.gameObject.activeSelf);
            });
        }
        #endregion

    }

}

