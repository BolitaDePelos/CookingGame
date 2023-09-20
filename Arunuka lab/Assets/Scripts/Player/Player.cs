using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class Player : SingletonMonobehaviour<Player>
{

    [Header("Inputs")]
    [Tooltip("If Device input is Mouse")]
    public bool IsCurrentDeviceMouse;

    [Header("Player")]
    [Tooltip("Move speed of the character in m/s")]
    public float moveSpeed = 4.0f;
    [Tooltip("Rotation speed of the character")]
    public float rotationSpeed = 1.0f;
    [Tooltip("Acceleration and deceleration")]
    public float SpeedChangeRate = 10.0f;


    [Header("Grounded")]
    public bool grounded = false;
    public float groundedOffset = -0.14f;
    public float groundedRadious = 0.5f;
    public LayerMask groundedLayers;


    [Header("Cinemachine")]
    public GameObject CinemachineCameraTarget;
    public float TopClamp = 90.0f; //How far in degrees can you move the camera up
    public float BottomClamp = -90.0f; //How far in degrees can you move the camera down

    //cinemachine
    private float cinemachineTargetPitch;

    [Header("Parameters")]
    private float xInput;
    private float yInput;
    private float xInputMouse;
    private float yInputMouse;

    public Animator animPlayer;


    //Player
    private float speed;
    private float rotationVelocity;
    private float verticalVelocity;

    private GameObject mainCamera;
    private CharacterController characterController;

    private const float _threshold = 0.01f;

    public bool tutorialMode;
    protected override void Awake()
    {
        if(mainCamera == null)
        {
            mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }
    }

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        animPlayer = GetComponent<Animator>();
        Bufferposition = transform.position;
    }

    private void Update()
    {
        PlayerInput();
        GroundedCheck();
        Move();
    }

    private void LateUpdate()
    {
        CameraRotation();
    }

    private void GroundedCheck()
    {
        // Set sphere Position, with offset
        Vector3 spherePositionCheck = new Vector3(transform.position.x, transform.position.y - groundedOffset, transform.position.z);
        grounded = Physics.CheckSphere(spherePositionCheck, groundedRadious, groundedLayers, QueryTriggerInteraction.Ignore);

    }

    Vector3 Bufferposition;
    private void PlayerInput()
    {
        var pos = Bufferposition;
        pos.x = transform.position.x;
        pos.y = transform.position.y;
        transform.position = pos;

        xInput = InputManager.GetInstance().GetMoveDirection().x;
        yInput = InputManager.GetInstance().GetMoveDirection().y;

        xInputMouse = InputManager.GetInstance().GetlookInput().x;
        yInputMouse = InputManager.GetInstance().GetlookInput().y;

        IsCurrentDeviceMouse = InputManager.GetInstance().IsCurrentDeviceMouse;



    }

    private void CameraRotation()
    {
        if(InputManager.GetInstance().GetlookInput().sqrMagnitude >= _threshold)
        {
            //Don't multiply mouse input by Time.deltaTime
            float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;

            cinemachineTargetPitch += yInputMouse * rotationSpeed * deltaTimeMultiplier;
            rotationVelocity = xInputMouse * rotationSpeed * deltaTimeMultiplier;

            // clamp our pitch rotation
            cinemachineTargetPitch = ClampAngle(cinemachineTargetPitch, BottomClamp, TopClamp);

            // Update Cinemachine camera target pitch
            CinemachineCameraTarget.transform.localRotation = Quaternion.Euler(cinemachineTargetPitch, 0.0f, 0.0f);

            // rotate the player left and right
            transform.Rotate(Vector3.up * rotationVelocity);
        }


    }


    private void Move()
    {
        // set target speed based on move speed, sprint speed and if sprint is pressed

        // a simplistic acceleration and deceleration designed to be easy to remove, replace, or iterate upon

        // note: Vector2's == operator uses approximation so is not floating point error prone, and is cheaper than magnitude
        // if there is no input, set the target speed to 0
       /*
        if (InputManager.GetInstance().GetMoveDirection() == Vector2.zero)
        {
            moveSpeed = 0.0f;

        }
       */

        // a reference to the players current horizontal velocity
        float currentHorizontalSpeed = new Vector3(characterController.velocity.x, 0.0f, characterController.velocity.z).magnitude;

        float speedOffset = 0.1f;
        float inputMagnitude = InputManager.GetInstance().analogMovement ? InputManager.GetInstance().GetMoveDirection().magnitude : 1f;

        // accelerate or decelerate to target speed
        if (currentHorizontalSpeed < moveSpeed - speedOffset || currentHorizontalSpeed > moveSpeed + speedOffset)
        {
            // creates curved result rather than a linear one giving a more organic speed change
            // note T in Lerp is clamped, so we don't need to clamp our speed
            speed = Mathf.Lerp(currentHorizontalSpeed, moveSpeed * inputMagnitude, Time.deltaTime * SpeedChangeRate);

            // round speed to 3 decimal places
            speed = Mathf.Round(speed * 1000f) / 1000f;
        }
        else
        {
            speed = moveSpeed;
        }

        // normalise input direction
        Vector3 inputDirection = new Vector3(xInput, 0.0f, yInput).normalized;

        // note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
        // if there is a move input rotate player when the player is moving
        if (InputManager.GetInstance().GetMoveDirection() != Vector2.zero)
        {
            // move
            inputDirection = transform.right * xInput+ transform.forward * yInput;
            if (tutorialMode) {
                TutorialManager.Instance.NextText();
                tutorialMode = false;
            }
        }

        // move the player
        characterController.Move(inputDirection.normalized * (speed * Time.deltaTime) + new Vector3(0.0f, verticalVelocity, 0.0f) * Time.deltaTime);



    }



    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }

    private void OnDrawGizmosSelected()
    {
        Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
        Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

        if (grounded) Gizmos.color = transparentGreen;
        else Gizmos.color = transparentRed;

        // when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
        Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y - groundedOffset, transform.position.z), groundedRadious);
    }
}
