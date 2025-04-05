using System;
using UnityEngine;

public class UIManager : MonoBehaviour {
    [SerializeField] private GameObject menuUI;
    private bool isMenuEnabled = false;

    private void Start() {
        InputManager.Instance.menuEvent += MenuUIHandler;
    }

    private void MenuUIHandler(object obj, EventArgs eventArgs) {
        if (isMenuEnabled) {
            DisableMenuUI();
        }
        else {
            EnableMenuUI();
        }
    }

    private void EnableMenuUI() {
        menuUI.SetActive(true);
        InputManager.Instance.playerInputAction.Player.Disable();
        isMenuEnabled = true;
        // Enable cursor (show and unlock)
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    
    public void DisableMenuUI() {
        menuUI.SetActive(false);
        InputManager.Instance.playerInputAction.Player.Enable();
        isMenuEnabled = false;
        // Enable cursor (show and unlock)
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Exit() {
        Application.Quit();
    }
}
