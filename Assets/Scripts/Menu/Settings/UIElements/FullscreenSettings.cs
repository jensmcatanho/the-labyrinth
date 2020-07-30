using UnityEngine;
using TMPro;

namespace Labyrinth.Menu.Settings.UIElements {

    public class FullscreenSettings : MonoBehaviour {

        [SerializeField] private TMP_Dropdown _dropdown;

        #region private methods
        private void Awake() {
            _dropdown.onValueChanged.AddListener((int fullscreenMode) => {
                Core.DisplayManager.Instance.FullscreenMode = (FullScreenMode)fullscreenMode;
            });
        }
        #endregion
    }

}