using TMPro;
using UnityEngine;

namespace Labyrinth.Menu.Settings.UIElements {

    public class VSyncSettings : MonoBehaviour {

        [SerializeField] private TMP_Dropdown _dropdown;

        #region private methods
        private void Awake() {
            _dropdown.onValueChanged.AddListener((int _) => {
                Core.DisplayManager.Instance.VSyncCount = _dropdown.value;
                _dropdown.Hide();
            });

            _dropdown.value = Core.DisplayManager.Instance.VSyncCount;
            _dropdown.RefreshShownValue();
        }
        #endregion
    }

}