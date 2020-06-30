using UnityEngine;

namespace Menu {

    public class MenuBehaviour : MonoBehaviour, Core.IEventListener {

        #region private fields
        private TitleText _titleText;

        private StartButton _startButton;
        #endregion

        #region public methods
        public void AddListeners() {
            Core.EventManager.Instance.AddListener<Events.MenuCameraPositioned>(OnCameraPositioned);
        }

        public void RemoveListeners() {
            if (Core.EventManager.Instance)
                Core.EventManager.Instance.RemoveListener<Events.MenuCameraPositioned>(OnCameraPositioned);
        }
        #endregion

        #region private methods
        private void Awake() {
            _titleText = GetComponentInChildren<TitleText>();
            _startButton = GetComponentInChildren<StartButton>();

            _titleText.SetActive(false);
            _startButton.SetActive(false);

            AddListeners();
        }

        private void OnDestroy() {
            RemoveListeners();
        }

        private void OnCameraPositioned(Events.MenuCameraPositioned e) {
            _titleText.SetActive(true);
            _startButton.SetActive(true);
        }
        #endregion
    }

}