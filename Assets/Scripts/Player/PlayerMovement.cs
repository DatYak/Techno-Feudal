using System.Collections;
using NUnit.Framework;
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
    InputAction parryAction;
    InputAction goopAction;

    [SerializeField]
    Transform playerHead;

    [SerializeField]
    float walkSpeed;

    [SerializeField]
    float jumpHeight;

    [SerializeField]
    float doubleJumpHeight;

    bool hasDoubleJump;

    [SerializeField]
    HumorType doubleJumpHumor;

    [SerializeField]
    float doubleJumpCost;

    [SerializeField]
    float gravityValue = -9.8f;

    [SerializeField]
    float dashSpeed = 10;

    [SerializeField]
    float dashTime = 0.5f;

    [SerializeField]
    HumorType dashHumor;

    [SerializeField]
    float dashHumorCost;

    bool isDashing = false;

    // PARRYING
    [SerializeField]
    float parryCooldown = 7.0f;
    [SerializeField]
    HumorType parryHumor;
    [SerializeField]
    float parryHumorCost;
    float lastParryAttempt = 0;
    public float parryPerfectWindow = 0.5f;
    public float lastParryRelease = 0;
    public int parryDamageBoost = 1000;
    public float parryImmuneTime = 2;

    //DUPING
    public HumorType goopHumor;
    public float goopHumorCost;
    public float goopCooldown = 14;
    float lastGoopLaunch = -100000;
    public GameObject goopBomb;


    [SerializeField]
    float mouseSensitivity;

    float xRotation = 0;

    Vector3 playerVelocity;

    Player player;
    HumorTracker humorTracker;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GetComponent<Player>();
        input = GetComponent<PlayerInput>();
        controller = GetComponent<CharacterController>();
        humorTracker = GetComponent<HumorTracker>();

        moveAction = input.actions.FindAction("Move");
        jumpAction = input.actions.FindAction("Jump");
        lookAction = input.actions.FindAction("Look");
        backAction = input.actions.FindAction("Back");
        dashAction = input.actions.FindAction("Dash");
        goopAction = input.actions.FindAction("Goop");
        parryAction = input.actions.FindAction("Parry");

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

        if (parryAction.WasPerformedThisFrame())
        {
            if (lastParryAttempt + parryCooldown < Time.time)
            {
                StartParry();
            }
        }
        if (parryAction.WasReleasedThisFrame())
        {
            StopParry();
        }
        if (goopAction.WasPerformedThisFrame())
        {
            if (lastGoopLaunch + goopCooldown < Time.time)
            {
                ThrowGoopBomb();
                lastGoopLaunch = Time.time;
            }
        }
    }

    private void FixedUpdate()
    {
        ProcessMovement();
        ProcessLook();

        if (player.isParrying)
        {
            humorTracker.ModifyBalance(parryHumor, parryHumorCost * Time.deltaTime);
        }
    }

    private void ProcessMovement()
    {
        if (controller.isGrounded)
        {
            hasDoubleJump = true;
        }

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
        if (!controller.isGrounded)
        {
            if (!hasDoubleJump)
            {
                return;
            }
            else
            {
                humorTracker.ModifyBalance(doubleJumpHumor, doubleJumpCost);
                playerVelocity.y = Mathf.Sqrt(doubleJumpHeight * -2f * gravityValue);
                hasDoubleJump = false;
            }
        } 

        playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravityValue);

    }

    private void ToggleCursor()
    {
        Cursor.lockState = Cursor.lockState == CursorLockMode.Locked ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = !Cursor.visible;
    }

    private void StartParry()
    {
        player.isParrying = true;
    }

    private void StopParry()
    {
        player.isParrying = false;
        lastParryRelease = Time.time;
    }

    public void FailParry()
    {
        lastParryAttempt = Time.time;
    }
    public void SucceedParry()
    {
        lastParryAttempt = Time.time;
        player.damageBoost += parryDamageBoost;
        player.AddImmunity(parryImmuneTime);
    }

    public void ThrowGoopBomb()
    {
        if (humorTracker.ModifyBalance(goopHumor, goopHumorCost))
        {
            Instantiate(goopBomb, playerHead.position + (playerHead.transform.forward), playerHead.transform.rotation);
        }
    }

    IEnumerator DashCoroutine()
    {
        humorTracker.ModifyBalance(dashHumor, dashHumorCost);
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