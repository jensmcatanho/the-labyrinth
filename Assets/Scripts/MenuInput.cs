using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuInput : IEventListener {

    [SerializeField] private Button _startButton;

    public MenuInput(Button startButton) {
        _startButton = startButton;

        AddListeners();
    }

    ~MenuInput() {
        RemoveListeners();
    }

    public void AddListeners() {
        _startButton.onClick.AddListener(OnStartButtonClicked);
    }

    public void RemoveListeners() {
        _startButton.onClick.RemoveListener(OnStartButtonClicked);
    }

    private void OnStartButtonClicked() {
        EventManager.Instance.QueueEvent(new Events.StartButtonClicked());
    }

}

namespace Events {
    public class StartButtonClicked : GameEvent {
        public StartButtonClicked() { }
    }
}
