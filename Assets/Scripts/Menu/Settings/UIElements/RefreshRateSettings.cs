using UnityEngine;
using TMPro;
using System.Collections.Generic;

namespace Labyrinth.Menu.Settings.UIElements {

    public class RefreshRateSettings : MonoBehaviour {

        [SerializeField] private TMP_Dropdown _dropdown;

        #region private methods
        private void Awake() {
            _dropdown.ClearOptions();

            _dropdown.onValueChanged.AddListener((int _) => {
                Core.DisplayManager.Instance.CurrentResolution = _dropdown.captionText.text;
                _dropdown.Hide();
            });

            var availableRefreshRates = Core.DisplayManager.Instance.AvailableRefreshRates;
            _dropdown.AddOptions(availableRefreshRates);

            _dropdown.value = GetCurrentRefreshRateIndex(availableRefreshRates);
            _dropdown.RefreshShownValue();
        }

        private int GetCurrentRefreshRateIndex(List<string> options) {
            for (var i = 0; i < options.Count; i++)
                if (options[i] == Core.DisplayManager.Instance.CurrentRefreshRate.ToString())
                    return i;

            return 0;
        }

        #endregion
    }

}