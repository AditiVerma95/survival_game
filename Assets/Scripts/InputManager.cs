using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour {
    public static InputManager Instance { get; private set; }

    private UserInputActionAsset playerInputAction;
    
    public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; }
    public bool JumpPressed { get; private set; }
    public bool FirePressed { get; private set; }
    public bool ReloadPressed { get; private set; }

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
            return;
        }

        playerInputAction = new UserInputActionAsset();
    }

    private void OnEnable() {
        playerInputAction.Player.Enable();

        playerInputAction.Player.Move.performed += context => MoveInput = context.ReadValue<Vector2>();
        playerInputAction.Player.Move.canceled += context => MoveInput = Vector2.zero;
        
        playerInputAction.Player.Look.performed += context => LookInput = context.ReadValue<Vector2>();
        playerInputAction.Player.Look.canceled += context => LookInput = Vector2.zero;

        playerInputAction.Player.Jump.performed += context => JumpPressed = true;
        playerInputAction.Player.Jump.canceled += context => JumpPressed = false;
    }

    private void OnDisable() {
        playerInputAction.Player.Disable();
    }

    private void Update() {
        
    }
}