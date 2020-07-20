﻿using UnityEngine;
using UnityEngine.UI;

namespace Labyrinth.Menu {

    public class ExitButton : MonoBehaviour, Core.IEventListener {

        private Button _button;

        #region public methods
        public void AddListeners() {
            _button.onClick.AddListener(() => {
                Application.Quit();
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