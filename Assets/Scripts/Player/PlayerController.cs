using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    [SerializeField] private Mover Mover;
    [SerializeField] private Jumper Jumper;
    [SerializeField] private Interacter Interacter;
    [Space]
    [SerializeField] private CameraController CameraController;
    [Space][Range(0.00f, 1.00f)]
    [SerializeField] private float Deadzone = 0.25f;

    public ControlState State = ControlState.Moving;
    private Input Input;

    /// <summary>
    /// Create the controller and set up the instance
    /// </summary>
    private void Awake()
    {
        Input = new Input();
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    /// <summary>
    /// Set events for controller inputs
    /// </summary>
    void Start()
    {
        Input.Enable();
        if (Input != null)
        {
            Input.Player.Jump.performed += _ => Jump();

            Input.Player.Interact.started += _ => StartInteract();
            Input.Player.Interact.canceled += _ => StopInteract();
        }
    }

    private void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }

    /// <summary>
    /// Get controller input and send it the relevant components
    /// </summary>
    private void Update()
    {
        //Get the input from the mouseposition or the right stick, and send it to the cameracontroller
        CameraController.RotateInput = Input.Camera.Move.ReadValue<Vector2>();

        //Get the input from WASD or the left stick
        Vector2 input = Input.Player.Move.ReadValue<Vector2>();
        switch (State)
        {
            case ControlState.Moving:
                Walk(input);
                break;

            case ControlState.Interacting:
                Interact(input);
                break;

            default:
                throw new System.NotImplementedException("State " + State + " is not implemented");
        }
    }

    /// <summary>
    /// Calculate the movement input based on the camera rotation
    /// (forward has to be the direction the camera is looking at)
    /// </summary>
    private void Walk(Vector3 input)
    {
        if (input.sqrMagnitude > Deadzone)
        {
            input = Quaternion.AngleAxis(CameraController.GetHorizontalRotation(), Vector3.back) * input;
            Mover.Input = input;
        }
        else
        {
            Mover.Input = Vector3.zero;
        }
    }

    /// <summary>
    /// Send the input to the interacter, which sends it to the interactable object
    /// </summary>
    private void Interact(Vector2 input)
    {
        if (input.sqrMagnitude > Deadzone)
        {
            Interacter.Input = input;
        }
        else
        {
            Interacter.Input = Vector2.zero;
        }
    }

    private void Jump()
    {
        if (State == ControlState.Moving)
        {
            Jumper.Jump();
        }
    }

    private void StartInteract()
    {
        if (Interacter.Interactable())
        {
            Mover.Input = Vector3.zero;
            State = ControlState.Interacting;
            Interacter.StartInteract();
        }
    }

    private void StopInteract()
    {
        if (Interacter.Interactable())
        {
            State = ControlState.Moving;
            Interacter.StopInteract();
        }
    }
}

public enum ControlState
{
    Moving,
    Interacting
}