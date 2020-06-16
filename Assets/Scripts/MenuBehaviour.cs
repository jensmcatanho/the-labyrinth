using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MenuBehaviour : MonoBehaviour {

    #region private variables
    [SerializeField] private Button _startButton;

    private MenuInput _menuInput;
    #endregion

    #region private methods
    private void Awake() {
        _menuInput = new MenuInput(_startButton);
    }
    #endregion
}
