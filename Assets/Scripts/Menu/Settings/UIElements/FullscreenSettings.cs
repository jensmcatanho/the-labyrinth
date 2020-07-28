using UnityEngine;
using UnityEngine.UI;

namespace Labyrinth.Menu.Settings.UIElements {

    public class FullscreenSettings : MonoBehaviour {

        [SerializeField] private Toggle _toggle;

        #region private methods
        private void Awake() {
            _toggle.isOn = Core.DisplayManager.Instance.Fullscreen;
            _toggle.onValueChanged.AddListener((bool target) => {
                Core.DisplayManager.Instance.Fullscreen = target;
            });
        }
        #endregion
    }

}