using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent (typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{

    PlayerInput input;
    CharacterController controller;

    InputAction moveAction;
    InputAction jumpAction;
    InputAction dashAction;
    InputAction lookAction;
    InputAction backAction;


    [SerializeField]
    Transform playerHead;

    [SerializeField]
    float walkSpeed;

    [SerializeField]
    float jumpHeight;

    [SerializeField]
    float gravityValue = -9.8f;

    [SerializeField]
    float dashSpeed = 10;

    [SerializeField]
    float dashTime = 0.5f;

    bool isDashing = false;

    [SerializeField]
    float mouseSensitivity;

    float xRotation = 0;

    Vector3 playerVelocity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        input = GetComponent<PlayerInput>();
        controller = GetComponent<CharacterController>();
        moveAction = input.actions.FindAction("Move");
        jumpAction = input.actions.FindAction("Jump");
        lookAction = input.actions.FindAction("Look");
        backAction = input.actions.FindAction("Back");
        dashAction = input.actions.FindAction("Dash");

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    Vector2 moveDir;
    Vector2 lookDelta;
    // Update is called once per frame
    void Update()
    {
        moveDir = moveAction.ReadValue<Vector2>().normalized;
        lookDelta = lookAction.ReadValue<Vector2>();

        if (backAction.WasPerformedThisFrame()) ToggleCursor();

        if (jumpAction.WasPerformedThisFrame()) StartJump();

        if (dashAction.WasPerformedThisFrame()) StartCoroutine(DashCoroutine());
    }

    private void FixedUpdate()
    {
        ProcessMovement();
        ProcessLook();
    }

    private void ProcessMovement()
    {
        if (controller.isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;   
        }

        Vector3 movement = new Vector3(moveDir.x, 0, moveDir.y) * walkSpeed;

        if (!isDashing)
        {
            movement = transform.TransformDirection(movement);
            controller.Move(movement * Time.deltaTime);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    private void ProcessLook()
    {
        float mouseX = lookDelta.x * mouseSensitivity * Time.deltaTime;
        float mouseY = lookDelta.y * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        playerHead.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        transform.Rotate(Vector3.up * mouseX);
    }

    public void StartJump()
    {
        //Not on ground, cannot jump
        if (!controller.isGrounded) return;

        playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravityValue);

    }

    private void ToggleCursor()
    {
        Cursor.lockState = Cursor.lockState == CursorLockMode.Locked ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = !Cursor.visible;
    }

    IEnumerator DashCoroutine()
    {
        isDashing = true;
        float startTime = Time.time;
        Vector3 direction = transform.TransformDirection(new Vector3(moveDir.x, 0, moveDir.y)); 

        while(Time.time < startTime + dashTime)
        {
            controller.Move(direction * dashSpeed * Time.deltaTime);
            yield return null;
        }
        isDashing = false;
    }
}