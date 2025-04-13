using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour {
    public static InputManager Instance { get; private set; }

    public UserInputActionAsset playerInputAction;
    
    public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; }
    public bool JumpPressed { get; private set; }
    public bool SprintPressed { get; private set; }
    public bool InteractPressed { get; private set; }
    public bool Interact2Pressed { get; private set; }
    public bool ShootPressed { get; private set; }
    
    public event EventHandler menuEvent;

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
        
        playerInputAction.Player.Sprint.performed += context => SprintPressed = true;
        playerInputAction.Player.Sprint.canceled += context => SprintPressed = false;
        
        playerInputAction.Player.Interact.performed += context => InteractPressed = true;
        playerInputAction.Player.Interact.canceled += context => InteractPressed = false;
        
        playerInputAction.Player.Interact2.performed += context => Interact2Pressed = true;
        playerInputAction.Player.Interact2.canceled += context => Interact2Pressed = false;

        playerInputAction.Player.Shoot.performed += context => ShootPressed = true;
        playerInputAction.Player.Shoot.canceled += context => ShootPressed = false;
        
        playerInputAction.UI.Enable();
        playerInputAction.UI.Menu.performed += context => menuEvent?.Invoke(this, EventArgs.Empty);

    }

    private void OnDisable() {
        playerInputAction.Player.Disable();
        playerInputAction.UI.Disable();
    }

    private void Update() {
        
    }
}