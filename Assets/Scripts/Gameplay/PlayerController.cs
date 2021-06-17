using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //put instance stuff here
    public static PlayerController Instance { get { return _Instance; } }
    private static PlayerController _Instance;

    public GameplayControls inputControls { get; private set; }

    [SerializeField] private float MoveSpeed = 2.0f;
    [SerializeField] private float JumpStrength = 3.5f;
    [Space]
    [SerializeField] private Transform PlugHolder;
    [SerializeField] private LayerMask groundMask = 0;
    [Space]
    [SerializeField] private PlugDetector PlugDetector;
    [SerializeField] private GameObject ModelObject = null;
    [Space]
    [SerializeField] private Rigidbody Rigidbody;
    [SerializeField] private Animator Animator;
    [Space]
    [SerializeField] private AudioSource PlugPickup;
    [SerializeField] private AudioSource PlugDrop;

    private Rigidbody draggedPlug = null;
    private bool isDragged = false;
    private Vector2 moveInput = Vector2.zero;

    #region Base Unity Functions
    private void Awake()
    {
        if (_Instance == null)
        {
            _Instance = this;
        }
        else
        {
            Destroy(this);
        }

        inputControls = new GameplayControls();
    }

    private void OnDestroy()
    {
        if (_Instance == this) _Instance = null;
    }

    private void OnEnable()
    {
        inputControls.Player.Enable();
    }

    private void OnDisable()
    {
        inputControls.Player.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        SetButtonCalls();
    }

    private void SetButtonCalls()
    {
        // Main Inputs
        if (inputControls != null)
        {
            inputControls.Player.Grab.started += _ => Grab_Start();
            inputControls.Player.Grab.canceled += _ => Grab_End();

            inputControls.Player.Jump.performed += _ => Jump();

            inputControls.Player.Pause.performed += _ => Pause();
            inputControls.Player.Menu.performed += _ => OpenMenu();
        }
    }
    #endregion

    #region Move
    // Update is called once per frame
    void Update()
    {
        moveInput = inputControls.Player.Move.ReadValue<Vector2>();

        //Rotating
        if (moveInput.sqrMagnitude > 0.0f)
        {
            Vector3 move = new Vector3(moveInput.x, 0, moveInput.y) * MoveSpeed * Time.fixedDeltaTime;
            Quaternion toRotation = Quaternion.LookRotation(move, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 720 * Time.fixedDeltaTime);
        }
    }

    void FixedUpdate()
    {
        //Moving
        if (moveInput.sqrMagnitude > 0.0f)
        {
            //Get movement input
            Vector3 move = new Vector3(moveInput.x, 0, moveInput.y) * MoveSpeed * Time.fixedDeltaTime;
            transform.position += move;
        }
    }

    #endregion

    #region Jump
    private void Jump()
    {
        if (Grounded())
        {
            Rigidbody.velocity += Vector3.up * JumpStrength;
        }
    }

    private bool Grounded()
    {
        Vector3 halfExtend = new Vector3(0.15f, 0.1f, 0.15f);
        return Physics.CheckBox(transform.position, halfExtend, transform.rotation, groundMask);
    }
    #endregion

    #region Grab
    private void Grab_Start()
    {
        draggedPlug = PlugDetector.GrabPlug();
        if (draggedPlug != null)
        {
            PlugPickup.Play();

            Plug plug = draggedPlug.GetComponent<Plug>();
            plug.Joint.connectedAnchor = (Vector3.up + Vector3.back) * 0.15f;

            draggedPlug.transform.parent = PlugHolder;
            draggedPlug.transform.localPosition = Vector3.zero;
            draggedPlug.transform.localRotation = Quaternion.Euler(Vector3.zero);
            plug.Joint.connectedBody = Rigidbody;


            if (plug != null)
            {
                plug.Disconnect();
            }
        }
    }

    private void Grab_End()
    {
        if (draggedPlug != null)
        {
            Plug plug = draggedPlug.GetComponent<Plug>();
            plug.Joint.connectedBody = plug.SelfConnector;
            plug.Joint.connectedAnchor = Vector3.zero;
            draggedPlug.transform.eulerAngles = Vector3.zero;

            PlugDetector.PlugCable(draggedPlug);

            draggedPlug.transform.parent = plug.CableHolder;
            draggedPlug.transform.localPosition = new Vector3(draggedPlug.transform.localPosition.x, 0, draggedPlug.transform.localPosition.z);
            draggedPlug = null;

            PlugDrop.Play();
        }
    }
    #endregion

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