using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour {
    public static InputManager Instance { get; private set; }

    private UserInputActionAsset playerInputAction;

    public event Action<Vector2> OnMove;
    public event Action<Vector2> OnLook;
    public event Action OnJump;

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

        playerInputAction.Player.Move.performed += context => OnMove?.Invoke(context.ReadValue<Vector2>());
        playerInputAction.Player.Move.canceled += context => OnMove?.Invoke(Vector2.zero);

        playerInputAction.Player.Look.performed += context => OnLook?.Invoke(context.ReadValue<Vector2>());
        playerInputAction.Player.Look.canceled += context => OnLook?.Invoke(Vector2.zero);

        playerInputAction.Player.Jump.performed += context => OnJump?.Invoke();
    }

    private void OnDisable() {
        playerInputAction.Player.Disable();
    }
}