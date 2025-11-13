using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent (typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{

    PlayerInput input;
    CharacterController controller;

    InputAction moveAction;
    InputAction lookAction;
    InputAction backAction;

    [SerializeField]
    Transform playerHead;

    [SerializeField]
    float walkSpeed;

    [SerializeField]
    float mouseSensitivity;

    float xRotation = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        input = GetComponent<PlayerInput>();
        controller = GetComponent<CharacterController>();
        moveAction = input.actions.FindAction("Move");
        lookAction = input.actions.FindAction("Look");
        backAction = input.actions.FindAction("Back");

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
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(moveDir.x, 0, moveDir.y) * walkSpeed;

        movement = transform.TransformDirection(movement);

        controller.Move(movement * Time.deltaTime);

        float mouseX = lookDelta.x * mouseSensitivity * Time.deltaTime;
        float mouseY = lookDelta.y * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        playerHead.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        transform.Rotate(Vector3.up * mouseX);
    }

    private void ToggleCursor()
    {
        Cursor.lockState = (Cursor.lockState == CursorLockMode.Locked ? CursorLockMode.None : CursorLockMode.Locked);
        Cursor.visible = !Cursor.visible;
    }
}
