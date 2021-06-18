using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    [SerializeField] private Mover Mover;
    [SerializeField] private Jumper Jumper;
    [SerializeField] private Grabber Grabber;

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

            inputControls.Player.Pause.performed += _ => Pause();
            inputControls.Player.Menu.performed += _ => OpenMenu();
        }
    }

    /// <summary>
    /// Get controller input and send it to Mover
    /// </summary>
    private void Update()
    {
        Mover.MoveInput = inputControls.Player.Move.ReadValue<Vector2>();
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