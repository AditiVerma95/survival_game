using System;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private CharacterController characterController;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float lookSensitivity = 2f;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float jumpForce = 2f;
    [SerializeField] private float gravity = 9.81f;

    private float pitch = 0f;
    private Vector3 moveInput;
    private Vector3 velocity;
    
    private void Start() {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;

        // Subscribe to InputManager events
        InputManager.Instance.OnMove += HandleMoveInput;
        InputManager.Instance.OnLook += HandleLookInput;
        InputManager.Instance.OnJump += HandleJumpInput;
    }

    private void OnDestroy() {
        // Unsubscribe to prevent memory leaks
        InputManager.Instance.OnMove -= HandleMoveInput;
        InputManager.Instance.OnLook -= HandleLookInput;
        InputManager.Instance.OnJump -= HandleJumpInput;
    }

    private void Update() {
        ApplyMovement();
        ApplyGravity();
    }

    private void HandleMoveInput(Vector2 input) {
        moveInput = new Vector3(input.x, 0f, input.y);
        moveInput = transform.TransformDirection(moveInput); // Convert to world space
        moveInput.Normalize();
    }

    private void HandleLookInput(Vector2 input) {
        // Rotate the player horizontally (Yaw)
        transform.Rotate(Vector3.up * input.x * lookSensitivity);

        // Vertical camera rotation (Pitch)
        pitch -= input.y * lookSensitivity;
        pitch = Mathf.Clamp(pitch, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }

    private void HandleJumpInput() {
        if (characterController.isGrounded) {
            velocity.y = Mathf.Sqrt(jumpForce * 2f * gravity);
        }
        Debug.Log("Jump function called");
    }

    private void ApplyMovement() {
        Vector3 move = moveInput * moveSpeed;
        characterController.Move(move * Time.deltaTime);
    }

    private void ApplyGravity() {
        if (characterController.isGrounded && velocity.y < 0) {
            velocity.y = -2f;
        }

        velocity.y -= gravity * Time.deltaTime;
        characterController.Move(new Vector3(0, velocity.y, 0) * Time.deltaTime);
    }
}
