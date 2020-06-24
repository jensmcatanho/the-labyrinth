﻿using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour, Core.IEventListener {
    
    private Button _button;

    #region public methods
    public void AddListeners() {
        _button.onClick.AddListener(OnClicked);
    }

    public void RemoveListeners() {
        _button.onClick.RemoveListener(OnClicked);
    }
    #endregion

    #region private methods
    private void Awake() {
        _button = GetComponent<Button>();

        AddListeners();
    }

    private void OnClicked() {
        Core.EventManager.Instance.QueueEvent(new Events.StartButtonClicked());
    }
    #endregion
}


namespace Events {
    public class StartButtonClicked : Core.GameEvent {
        public StartButtonClicked() {
        }
    }
}
