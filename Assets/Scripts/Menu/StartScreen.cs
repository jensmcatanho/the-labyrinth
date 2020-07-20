using UnityEngine;

namespace Labyrinth.Menu {

    public class StartScreen : MonoBehaviour {

        #region private fields
        private TitleText _titleText;

        private PressToContinueText _pressToContinueText;
        #endregion

        #region public methods
        public void EnableUIElements() {
            _titleText.gameObject.SetActive(true);
        }

        public void DisableUIElements() {
            _titleText.gameObject.SetActive(false);
        }
        #endregion

        #region private methods
        private void Awake() {
            _titleText = GetComponentInChildren<TitleText>();

            DisableUIElements();
        }

        private void Update() {
            if (Input.anyKeyDown) {
                Core.EventManager.Instance.TriggerEvent(new Events.Menu.AnyButtonClicked());
                Destroy(gameObject);
            }
        }
        #endregion
    }

}