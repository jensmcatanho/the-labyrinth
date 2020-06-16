using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MenuBehaviour : MonoBehaviour {

    [SerializeField] private Button _startButton;

    private MenuInput _menuInput;

    private void Awake() {
        _menuInput = new MenuInput(_startButton);
    }
}
