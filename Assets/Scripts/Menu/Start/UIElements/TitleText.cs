using UnityEngine;
using UnityEngine.UI;

namespace Labyrinth.Menu.Start.UIElements {

    public class TitleText : MonoBehaviour {

        private Text _text;

        #region private methods
        private void Awake() {
            _text = GetComponent<Text>();
        }

        private void OnEnable() {
            // Start animation
        }

        private void OnDisable() {
            // End animation
        }
        #endregion
    }

}