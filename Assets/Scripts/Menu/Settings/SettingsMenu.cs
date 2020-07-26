using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Labyrinth.Menu.Settings {

    public class SettingsMenu : MonoBehaviour {

        [SerializeField] private Button _backButton;

        [SerializeField] private GameObject _panel;

        [SerializeField] private GenericDictionary<Button, SettingsGroup> _settings;

        private SettingsGroup _currentSettings = null;

        #region public fields
        public void SetActive(bool target) {
            _backButton.gameObject.SetActive(target);

            foreach (var key in _settings.Keys)
                key.gameObject.SetActive(target);
        }

        public void Close() {
            _panel.SetActive(false);
            _backButton.gameObject.SetActive(false);

            foreach (var pair in _settings) {
                pair.Key.gameObject.SetActive(false);
                pair.Value.gameObject.SetActive(false);
            }
        }
        #endregion

        #region private fields
        private void Awake() {
            SetupBackButton();
            SetupSettingsButtons();

            Close();
        }

        private void SetupBackButton() {
            _backButton.onClick.AddListener(() => {
                Core.EventManager.Instance.TriggerEvent(new Events.Menu.BackToMenuClicked());
                Close();
            });
        }

        private void SetupSettingsButtons() {
            foreach (var entry in _settings) {
                var button = entry.Key;
                var settings = entry.Value;

                button.onClick.AddListener(() => {
                    
                    if (_panel.activeSelf) {
                        if (_currentSettings == settings) {
                            _currentSettings.gameObject.SetActive(false);
                            _currentSettings = null;

                            _panel.SetActive(false);

                        } else {
                            _currentSettings.gameObject.SetActive(false);
                            settings.gameObject.SetActive(true);
                            _currentSettings = settings;
                        }

                    } else {
                        _panel.SetActive(true);
                        settings.gameObject.SetActive(true);
                        _currentSettings = settings;
                    }

                });
            }
        }
        #endregion

    }

}

