using UnityEngine;
using TMPro;
using System.Collections.Generic;

namespace Labyrinth.Menu.Settings.UIElements {
    
    public class ResolutionSettings : MonoBehaviour {

        [SerializeField] private TMP_Dropdown _dropdown;


        #region private methods
        private void Awake() {
            _dropdown.ClearOptions();

            _dropdown.onValueChanged.AddListener((int _) => {
                Core.DisplayManager.Instance.CurrentResolution = _dropdown.captionText.text;
            });

            var availableResolutions = Core.DisplayManager.Instance.AvailableResolutions;
            _dropdown.AddOptions(availableResolutions);

            _dropdown.value = GetCurrentResolutionIndex(availableResolutions);
            _dropdown.RefreshShownValue();
        }

        private int GetCurrentResolutionIndex(List<string> options) {
            for (var i = 0; i < options.Count; i++)
                if (options[i] == Core.DisplayManager.Instance.CurrentResolution)
                    return i;

            return 0;
        }

        #endregion
    }

}