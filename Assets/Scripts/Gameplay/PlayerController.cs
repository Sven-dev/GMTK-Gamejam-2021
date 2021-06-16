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
    [SerializeField] private AnimationCurve MovementCurve;
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

    // Update is called once per frame
    void Update()
    {
        moveInput = inputControls.Player.Move.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        //Moving
        if (moveInput.sqrMagnitude > 0.0f)
        {
            //Get movement input
            Vector3 move = new Vector3(moveInput.x, 0, moveInput.y) * MoveSpeed;
            move.y = 0;
            Rigidbody.AddRelativeForce(move);

            //Rotate model into move direction
            if (ModelObject != null && !isDragged)
            {
                Vector3 temp = Rigidbody.velocity.normalized;
                temp.y = 0;
                ModelObject.transform.rotation = Quaternion.LookRotation(temp);
            }
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
            draggedPlug.transform.parent = PlugHolder;
            draggedPlug.transform.position = PlugHolder.position;
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

            PlugDetector.PlugCable(draggedPlug);

            draggedPlug.transform.parent = plug.CableHolder;
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