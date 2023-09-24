using UnityEngine;

/// <summary>
/// Contains all the mechanics for the player, as such as:
/// <code>
/// - Player Movement.
/// - Camera Movement.
/// - Ground collisions.
/// </code>
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class Player : SingletonMonobehaviour<Player>
{
    [Header("Inputs")] [Tooltip("If Device input is Mouse")]
    public bool IsCurrentDeviceMouse;

    [Header("Player")] [Tooltip("Move speed of the character in m/s")]
    public float moveSpeed = 4.0f;

    [Tooltip("Rotation speed of the character")]
    public float rotationSpeed = 1.0f;

    [Tooltip("Acceleration and deceleration")]
    public float SpeedChangeRate = 10.0f;

    [Header("Grounded")] public bool grounded;

    public float groundedOffset = -0.14f;
    public float groundedRadious = 0.5f;
    public LayerMask groundedLayers;

    [Header("Cinemachine")] public GameObject CinemachineCameraTarget;

    public float TopClamp = 90.0f; //How far in degrees can you move the camera up
    public float BottomClamp = -90.0f; //How far in degrees can you move the camera down

    //cinemachine
    private float cinemachineTargetPitch;

    [Header("Parameters")] public Animator animPlayer;

    public bool tutorialMode;

    // Input values.
    //
    private float xInput;

    private float yInput;
    private float xInputMouse;
    private float yInputMouse;

    //Player.
    //
    private float speed;

    private float rotationVelocity;
    private float verticalVelocity;
    private bool canMove = true;

    private GameObject mainCamera;

    private CharacterController characterController;

    private const float CameraRotationThreshold = 0.01f;

    [Header("Gravity")] private readonly float _gravity = -9.81f;
    [SerializeField] private float gravityMultiplier = 3.0f;
    private float _velocity;

    /// <summary>
    /// Called when the objects are being created.
    /// </summary>
    protected override void Awake()
    {
        // Call base to create the singleton instance.
        //
        base.Awake();

        if (mainCamera == null)
            mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    /// <summary>
    /// Called after the creation of the objects.
    /// </summary>
    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        animPlayer = GetComponent<Animator>();
        currentPosition = transform.position;
    }

    /// <summary>
    /// Called every game frame.
    /// </summary>
    private void Update()
    {
        PlayerInput();
        GroundedCheck();

        ApplyGravity();
        if (canMove)
            Move();
    }

    /// <summary>
    /// Called every game frame, but after the <see cref="Update"/>.
    /// </summary>
    private void LateUpdate()
    {
        if (!RecipesMenu.GameIsPaused) CameraRotation();
    }

    /// <summary>
    /// Sets if the player can move or not.
    /// </summary>
    public void SetCanMove(bool canMove)
    {
        this.canMove = canMove;
    }

    /// <summary>
    /// Checks if the player is on the ground or not.
    /// </summary>
    private void GroundedCheck()
    {
        // Set sphere Position, with offset.
        //
        Vector3 spherePositionCheck = new(
            transform.position.x,
            transform.position.y - groundedOffset,
            transform.position.z);

        grounded = Physics.CheckSphere(
            spherePositionCheck,
            groundedRadious,
            groundedLayers,
            QueryTriggerInteraction.Ignore);
    }

    private Vector3 currentPosition;

    /// <summary>
    /// Gets the input values and saves it in local variables.
    /// </summary>
    private void PlayerInput()
    {
        Vector3 position = currentPosition;
        position.x = transform.position.x;
        position.y = transform.position.y;
        transform.position = position;

        Vector2 moveDirection = InputManager.GetInstance().GetMoveDirection();
        xInput = moveDirection.x;
        yInput = moveDirection.y;

        Vector2 lookDirection = InputManager.GetInstance().GetlookInput();
        xInputMouse = lookDirection.x;
        yInputMouse = lookDirection.y;

        IsCurrentDeviceMouse = InputManager.GetInstance().IsCurrentDeviceMouse;
    }

    /// <summary>
    /// Rotates the camera based on the input values.
    /// </summary>
    private void CameraRotation()
    {
        if (InputManager.GetInstance().GetlookInput().sqrMagnitude < CameraRotationThreshold)
            return;

        //TODO: Don't multiply mouse input by Time.deltaTime.
        //
        float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;
        cinemachineTargetPitch += yInputMouse * rotationSpeed * deltaTimeMultiplier;
        rotationVelocity = xInputMouse * rotationSpeed * deltaTimeMultiplier;

        // Update Cinemachine camera target pitch.
        //
        cinemachineTargetPitch = ClampAngle(cinemachineTargetPitch, BottomClamp, TopClamp);
        CinemachineCameraTarget.transform.localRotation = Quaternion.Euler(cinemachineTargetPitch, 0.0f, 0.0f);

        // Rotate the player left and right.
        //
        transform.Rotate(Vector3.up * rotationVelocity);
    }

    /// <summary>
    /// Moves the player based on the input values.
    /// </summary>
    private void Move()
    {
        // Reference to the players current horizontal velocity.
        //
        float currentHorizontalSpeed = new Vector3(
            characterController.velocity.x,
            0.0f,
            characterController.velocity.z).magnitude;


        float speedOffset = 0.1f;
        float inputMagnitude = InputManager.GetInstance().analogMovement
            ? InputManager.GetInstance().GetMoveDirection().magnitude
            : 1f;

        // Accelerate or decelerate to target speed.
        //
        if (currentHorizontalSpeed < moveSpeed - speedOffset
            || currentHorizontalSpeed > moveSpeed + speedOffset)
        {
            // Creates curved result rather than a linear one giving a more organic speed change.
            // Note: Lerp is clamped, so we don't need to clamp our speed.
            //
            speed = Mathf.Lerp(currentHorizontalSpeed, moveSpeed * inputMagnitude, Time.deltaTime * SpeedChangeRate);
            speed = Mathf.Round(speed * 1000f) / 1000f; // Three decimal rounded.
        }
        else
            speed = moveSpeed; // Desired speed achieved.

        Vector3 inputDirection = new Vector3(xInput, 0.0f, yInput).normalized;

        // Note: Vector2 != operator uses approximation so is not floating point error prone,
        // and is cheaper than magnitude if there is a move input rotate player when the player is moving.
        //
        if (InputManager.GetInstance().GetMoveDirection() != Vector2.zero)
        {
            inputDirection = transform.right * xInput + transform.forward * yInput;
            if (tutorialMode)
            {
                TutorialManager.Instance.NextText();
                tutorialMode = false;
            }
        }

        // Move the player.
        //
        Vector3 motion = inputDirection.normalized * (speed * Time.deltaTime)
                         + new Vector3(0.0f, verticalVelocity, 0.0f) * Time.deltaTime;

        characterController.Move(motion);
    }


    private void ApplyGravity()
    {
        if (!grounded) // Si el jugador no est� en el suelo.
            verticalVelocity += _gravity * gravityMultiplier * Time.deltaTime; // Aplicar gravedad.
        else // Si el jugador est� en el suelo, resetear la velocidad vertical.
            verticalVelocity = 0f; // Puedes ajustar este valor seg�n tus necesidades.
    }

    private void OnDrawGizmosSelected()
    {
        Color transparentGreen = new(0.0f, 1.0f, 0.0f, 0.35f);
        Color transparentRed = new(1.0f, 0.0f, 0.0f, 0.35f);

        if (grounded)
            Gizmos.color = transparentGreen;
        else
            Gizmos.color = transparentRed;

        // When selected, draw a gizmo in the position of, and matching radius of, the grounded collider
        //
        Vector3 position = new(transform.position.x, transform.position.y - groundedOffset, transform.position.z);
        Gizmos.DrawSphere(position, groundedRadious);
    }

    #region Helpers

    /// <summary>
    /// Clamps a angle based on the given one.
    /// </summary>
    /// <remarks>
    /// If the angle is less than -360�, add 360� to only have one full turn.
    /// If the angle is less than 360�, subtract 360� to only have one full turn.
    /// </remarks>
    private static float ClampAngle(float angle, float minimumAngle, float maximumAngle)
    {
        if (angle < -360f)
            angle += 360f;

        if (angle > 360f)
            angle -= 360f;

        return Mathf.Clamp(angle, minimumAngle, maximumAngle);
    }

    #endregion Helpers
}