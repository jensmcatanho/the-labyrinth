using UnityEngine;
using UnityEngine.UI;

public class MenuInput : MonoBehaviour, IEventListener {

    [SerializeField] private Button _startButton;

    #region public methods
    public void AddListeners() {
        _startButton.onClick.AddListener(OnStartButtonClicked);
    }

    public void RemoveListeners() {
        _startButton.onClick.RemoveListener(OnStartButtonClicked);
    }
    #endregion

    #region private methods

    private void Awake() {
        AddListeners();
    }

    private void OnDestroy() {
        RemoveListeners();
    }

    private void OnStartButtonClicked() {
        EventManager.Instance.QueueEvent(new Events.StartButtonClicked());
    }
    #endregion
}

namespace Events {
    public class StartButtonClicked : GameEvent {
        public StartButtonClicked() {
        }
    }
}
