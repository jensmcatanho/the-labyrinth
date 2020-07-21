using UnityEngine;
using UnityEngine.UI;

namespace Labyrinth.Menu.UIElements {

    public class StartButton : MonoBehaviour, Core.IEventListener {

        private Button _button;

        #region public methods
        public void AddListeners() {
            _button.onClick.AddListener(() => {
                Core.EventManager.Instance.QueueEvent(new Events.Menu.StartButtonClicked());
            });
        }

        public void RemoveListeners() {
            _button.onClick.RemoveAllListeners();
        }
        #endregion

        #region private methods
        private void Awake() {
            _button = GetComponent<Button>();

            AddListeners();
        }
        #endregion
    }

}