using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    [SerializeField] private Mover Mover;
    [SerializeField] private Jumper Jumper;
    [SerializeField] private Interacter Interacter;
    [SerializeField] private CameraController CameraController;

    public ControlState State = ControlState.Moving;
    private GameplayControls inputControls;

    /// <summary>
    /// Create the controller and set up the instance
    /// </summary>
    private void Awake()
    {
        inputControls = new GameplayControls();
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    /// <summary>
    /// Set events for controller inputs
    /// </summary>
    void Start()
    {
        if (inputControls != null)
        {
            inputControls.Player.Jump.performed += _ => Jump();

            inputControls.Player.Interact.started += _ => StartInteract();
            inputControls.Player.Interact.canceled += _ => StopInteract();

            inputControls.Player.Pause.performed += _ => Pause();
            inputControls.Player.Menu.performed += _ => OpenMenu();
        }
    }

    /// <summary>
    /// Get controller input and send it the relevant components
    /// </summary>
    private void Update()
    {
        //Get the input from the mouseposition or the right stick, and send it to the cameracontroller
        CameraController.RotateInput = inputControls.Player.Camera.ReadValue<Vector2>();

        //Get the input from WASD or the left stick
        Vector3 input = inputControls.Player.Move.ReadValue<Vector2>();
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
        input = Quaternion.AngleAxis(CameraController.GetHorizontalRotation(), Vector3.back) * input;
        Mover.Input = input;
    }

    private void Jump()
    {
        if (State == ControlState.Moving)
        {
            Jumper.Jump();
        }
    }

    /// <summary>
    /// Send the input to the interactable object
    /// </summary>
    private void Interact(Vector3 input)
    {
        Interacter.Input = input;
    }

    private void StartInteract()
    {
        if (Interacter.Interactable())
        {
            State = ControlState.Interacting;
            Interacter.Interact();
        }
    }

    private void StopInteract()
    {
        if (Interacter.Interactable())
        {
            State = ControlState.Moving;
        }
    }

    private void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }

    #region UI Related
    private void Pause()
    {
        Debug.Log("You Paused");
    }
    private void OpenMenu()
    {
        Debug.Log("You want a Menu");
    }
    #endregion
}

public enum ControlState
{
    Moving,
    Interacting,
    Climbing
}