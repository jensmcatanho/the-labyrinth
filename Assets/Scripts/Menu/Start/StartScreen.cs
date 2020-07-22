using UnityEngine;

namespace Labyrinth.Menu.Start {

    using UIElements;

    public class StartScreen : MonoBehaviour {

        #region private fields
        private TitleText _titleText;

        private PressToContinueText _pressToContinueText;

        private bool _canPressToContinue = false;
        #endregion

        #region public methods
        public void EnableUIElements() {
            _titleText.gameObject.SetActive(true);
            _pressToContinueText.gameObject.SetActive(true);
            _canPressToContinue = true;
        }

        public void DisableUIElements() {
            _titleText.gameObject.SetActive(false);
            _pressToContinueText.gameObject.SetActive(false);
            _canPressToContinue = false;
        }
        #endregion

        #region private methods
        private void Awake() {
            _titleText = GetComponentInChildren<TitleText>(true);
            _pressToContinueText = GetComponentInChildren<PressToContinueText>(true);

            DisableUIElements();
        }

        private void Update() {
            if (Input.anyKeyDown && _canPressToContinue) {
                Core.EventManager.Instance.TriggerEvent(new Events.Menu.AnyButtonClicked());
                Destroy(gameObject);
            }
        }
        #endregion
    }

}