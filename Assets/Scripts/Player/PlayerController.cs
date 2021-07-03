using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    [SerializeField] private Mover Mover;
    [SerializeField] private Jumper Jumper;
    [SerializeField] private Grabber Grabber;
    [SerializeField] private Puncher Puncher;
    [SerializeField] private CameraRotator CameraRotator;

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
            inputControls.Player.Jump.performed += _ => Jumper.Jump();

            inputControls.Player.Grab.started += _ => Grabber.Grab();
            inputControls.Player.Grab.canceled += _ => Grabber.LetGo();

            inputControls.Player.Punch.performed += _ => Puncher.Punch();

            inputControls.Player.Pause.performed += _ => Pause();
            inputControls.Player.Menu.performed += _ => OpenMenu();
        }
    }

    /// <summary>
    /// Get controller input and send it the relevant components
    /// </summary>
    private void Update()
    {
        CameraRotator.RotateInput = inputControls.Player.Camera.ReadValue<Vector2>();

        //Calculate the camera rotation into the movement input
        Vector3 input = inputControls.Player.Move.ReadValue<Vector2>();
        input = Quaternion.AngleAxis(CameraRotator.transform.eulerAngles.y, Vector3.back) * input;

        Mover.MoveInput = input;
    }

    private void OnEnable()
    {
        inputControls.Player.Enable();
    }

    private void OnDisable()
    {
        inputControls.Player.Disable();
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