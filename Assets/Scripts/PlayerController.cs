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

    private AudioSource audioSource;
    private AudioClip currentAudio;
    [SerializeField] private AudioClip walkingAudioClip;
    [SerializeField] private AudioClip runningAudioClip;
    private bool isAudioPlaying = false;
    
    private void Start() {
        audioSource = GetComponent<AudioSource>();
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update() {
        HandleSprint();
        HandleMovement();
        HandleAudio();
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
            velocity.y = -2f; // Keeps grounded
        }

        // Only jump if on walkable slope
        if (isGrounded && InputManager.Instance.JumpPressed && IsOnWalkableSlope()) {
            velocity.y = Mathf.Sqrt(jumpForce * 2f * gravity);
        }

        // Apply gravity
        velocity.y -= gravity * Time.deltaTime;
        characterController.Move(new Vector3(0, velocity.y, 0) * Time.deltaTime);
    }
    
    private bool IsOnWalkableSlope() {
        Ray ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 1.5f)) {
            float slopeAngle = Vector3.Angle(hit.normal, Vector3.up);
            return slopeAngle <= characterController.slopeLimit; // default is 45°
        }
        return false;
    }



    private void HandleSprint() {
        sprintVal = (InputManager.Instance.SprintPressed) ? sprintMultiplier : 1f;
        currentAudio = (InputManager.Instance.SprintPressed) ? runningAudioClip : walkingAudioClip;
    }

    private void HandleAudio() {
        bool isMoving = new Vector3(velocity.x, 0, velocity.z).magnitude > 0.1f;
        bool isGrounded = characterController.isGrounded;
        if (isMoving) {
            if (!audioSource.isPlaying || audioSource.clip != currentAudio) {
                audioSource.clip = currentAudio;
                audioSource.loop = true;
                audioSource.Play();
            }
        } else {
            if (audioSource.isPlaying) {
                audioSource.Stop();
            }
        }
    }

}
