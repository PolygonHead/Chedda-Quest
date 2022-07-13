using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float walkSpeed = 4.5f;
    public float jumpVelocity = 4f;
    public float mouseSensitivity = 2.5f;
    //public bool playIntroAnimation = false;
    //public bool playWakeAnimation = false;

    [Range(0, 1)] public float crouchSpeedMultiplier = 0.4f;
    [Range(0, 1)] public float crouchHeightMultiplier = 0.5f;

    [HideInInspector]
    public float targetFov;
    private float defaultFov;

    /// <summary>
    /// The current maximum movement speed of the player. This changes depending 
    /// on whether the player is crouching or not.
    /// </summary>
    private float maxSpeed;

    private float defaultBodyHeight;
    private float defaultHeadHeight;

    private Vector3 velocity;
    private float xRotation;
    private bool crouching;
    private bool inputEnabled = true;

    private CharacterController controller;
    private Transform head;

    private new Camera camera;
    public float gravity = -9.81f;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        head = gameObject.transform.Find("Head");

        camera = GetComponentInChildren<Camera>();
        defaultFov = camera.fieldOfView;
        targetFov = defaultFov;

        defaultBodyHeight = controller.height;
        defaultHeadHeight = head.localPosition.y;
        maxSpeed = walkSpeed;

        Cursor.lockState = CursorLockMode.Locked;
    }
    
    void Update()
    {
        HandleGravity();
        if (inputEnabled) HandleInput();
        HandleCrouch();

        camera.fieldOfView = Mathf.Lerp(
            camera.fieldOfView, 
            targetFov, 
            Time.deltaTime * 10f
        );

        controller.Move(velocity * Time.deltaTime);
    }

    void HandleInput()
    {   
        crouching = Input.GetButton("Crouch");
        HandleMovement();
        HandleRotation();
    }

    void HandleMovement() 
    {
        Vector3 movement = new Vector3(
            Input.GetAxis("Horizontal"),
            0,
            Input.GetAxis("Vertical")
        );

        // Stop the player from moving faster diagonally.
        movement = Vector3.ClampMagnitude(movement, 1);
        
        movement = transform.TransformDirection(movement);
        movement *= maxSpeed;

        // Assign the movement values to the player velocity.
        velocity.x = movement.x;
        velocity.z = movement.z;
        
        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            velocity.y = jumpVelocity;
        }
    }

    void HandleRotation() 
    {
        float mouseDeltaX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseDeltaY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        
        xRotation = Mathf.Clamp(xRotation - mouseDeltaY, -90f, 90f);
        head.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        
        transform.Rotate(Vector3.up * mouseDeltaX);
    }
    
    void HandleCrouch() 
    {
        float targetHeadHeight;

        if (crouching) 
        {
            maxSpeed = walkSpeed * crouchSpeedMultiplier;
            controller.height = defaultBodyHeight * crouchHeightMultiplier;
            targetHeadHeight = defaultHeadHeight * crouchHeightMultiplier;
        } 
        else 
        {
            maxSpeed = walkSpeed;
            controller.height = defaultBodyHeight;
            targetHeadHeight = defaultHeadHeight;
        }
        
        float headHeight = Mathf.Lerp(head.localPosition.y, targetHeadHeight, Time.deltaTime * 7f);

        head.localPosition = new Vector3(0, headHeight, 0);
        controller.center = new Vector3(0f, controller.height / 2, 0f);
    }

    void HandleGravity()
    {
        if (!controller.isGrounded) 
        {
            velocity.y += gravity * Time.deltaTime;
        } 
        else 
        {
            velocity.y = -1f;
        }
    }

    public void SetPosition(Vector3 position) {
        transform.position = position;
    }

    public void SetRotation(float yRotation, float xRotation) 
    {
        head.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.rotation = Quaternion.Euler(0f, yRotation, 0f);
    }

    
}
