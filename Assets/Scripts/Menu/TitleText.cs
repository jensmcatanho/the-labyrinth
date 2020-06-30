using UnityEngine;
using UnityEngine.UI;

namespace Menu {

    public class TitleText : MonoBehaviour {

        private Text _button;

        #region public methods
        public void SetActive(bool target) {
            gameObject.SetActive(target);
        }
        #endregion

        #region private methods
        private void Awake() {
            _button = GetComponent<Text>();
        }
        #endregion
    }

}