using System;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private CharacterController characterController;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float lookSensitivity = 2f;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float jumpForce = 2f;
    [SerializeField] private float gravity = 9.81f;
    [SerializeField] private float sprintMultiplier = 2f;
    private float sprintVal = 1f;

    private float pitch = 0f; // For vertical camera rotation
    private Vector3 velocity;
    
    private void Start() {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update() {
        HandleSprint();
        HandleMovement();
        HandleLook();
        HandleJump();
    }

    private void HandleMovement() {
        Vector3 moveInput = new Vector3(InputManager.Instance.MoveInput.x, 0f, InputManager.Instance.MoveInput.y);
        moveInput = transform.TransformDirection(moveInput); // Convert from local to world space
        moveInput.Normalize();

        velocity.x = moveInput.x * moveSpeed * sprintVal;
        velocity.z = moveInput.z * moveSpeed * sprintVal;
        
        characterController.Move(velocity * Time.deltaTime);
    }

    private void HandleLook() {
        Vector2 lookInput = InputManager.Instance.LookInput * lookSensitivity;

        // Rotate the player horizontally (Yaw)
        transform.Rotate(Vector3.up * lookInput.x);
        
        // Vertical camera rotation (Pitch)
        pitch -= lookInput.y;
        pitch = Mathf.Clamp(pitch, -90f, 90f); // Clamp to prevent flipping
        cameraTransform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }

    private void HandleJump() {
        bool isGrounded = characterController.isGrounded;
        
        if (isGrounded && velocity.y < 0) {
            velocity.y = -2f; // Small downward force to keep player grounded
        }

        if (isGrounded && InputManager.Instance.JumpPressed) {
            velocity.y = Mathf.Sqrt(jumpForce * 2f * gravity); // Jump velocity calculation
        }

        // Apply gravity
        velocity.y -= gravity * Time.deltaTime;
        characterController.Move(new Vector3(0, velocity.y, 0) * Time.deltaTime);
    }

    private void HandleSprint() {
        sprintVal = (InputManager.Instance.SprintPressed) ? sprintMultiplier : 1f;
    }
}
