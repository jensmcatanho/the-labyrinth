using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuInput : IEventListener {

    [SerializeField] private Button _startButton;

    public MenuInput(Button startButton) {
        _startButton = startButton;

        SetListeners();
    }

    public void SetListeners() {
        _startButton.onClick.AddListener(OnStartButtonClicked);
    }

    private void OnStartButtonClicked() {
        EventManager.Instance.QueueEvent(new StartButtonClicked());
    }

}

public class StartButtonClicked : GameEvent {
    public StartButtonClicked() { }
}