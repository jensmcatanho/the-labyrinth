using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuInput : IEventListener {

    [SerializeField] private Button _startButton;

    #region constructor
    public MenuInput(Button startButton) {
        _startButton = startButton;

        AddListeners();
    }

    ~MenuInput() {
        RemoveListeners();
    }
    #endregion

    #region public methods
    public void AddListeners() {
        _startButton.onClick.AddListener(OnStartButtonClicked);
    }

    public void RemoveListeners() {
        _startButton.onClick.RemoveListener(OnStartButtonClicked);
    }
    #endregion

    #region private methods
    private void OnStartButtonClicked() {
        EventManager.Instance.QueueEvent(new Events.StartButtonClicked());
    }
    #endregion
}

namespace Events {
    public class StartButtonClicked : GameEvent {
        public StartButtonClicked() { }
    }
}
