using UnityEngine;
using UnityEngine.UI;

namespace Labyrinth.Menu.UIElements {

    public class PressToContinueText : MonoBehaviour {

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