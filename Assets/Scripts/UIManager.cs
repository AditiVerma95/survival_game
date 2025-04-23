using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {
    [SerializeField] private GameObject menuUI;
    private bool isMenuEnabled = false;

    [SerializeField] private GameObject inventoryUI;
    private bool isInventoryEnabled = false;

    private void Start() {
        InputManager.Instance.menuEvent += MenuUIHandler;
        InputManager.Instance.inventoryEvent += InventoryUIHandler;
    }

    private void InventoryUIHandler(object sender, EventArgs e) {
        if (!isInventoryEnabled) {
            EnableInventoryUI();
        }
        else {
            DisableInventoryUI();
        }
    }

    private void EnableInventoryUI() {
        inventoryUI.SetActive(true);
        InputManager.Instance.playerInputAction.Player.Disable();
        isInventoryEnabled = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    
    private void DisableInventoryUI() {
        inventoryUI.SetActive(false);
        InputManager.Instance.playerInputAction.Player.Enable();
        isInventoryEnabled = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    
    public void DisableMenuUI() {
        menuUI.SetActive(false);
        InputManager.Instance.playerInputAction.Player.Enable();
        isMenuEnabled = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Exit() {
        SceneManager.LoadScene(0);
    }
}
