using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //put instance stuff here
    public static PlayerController Instance { get { return _Instance; } }
    private static PlayerController _Instance;

    public GameplayControls inputControls { get; private set; }
    public Animator animator { get; private set; }

    [SerializeField] private float fSpeed = 2.0f;
    [SerializeField] private float fJumpStrength = 3.5f;
    [SerializeField] private float GrabForce = 500;
    [Space]
    [SerializeField] private Transform PlugHolder;
    [SerializeField] private LayerMask groundMask = 0;
    [Space]
    [SerializeField] private PlugDetector PlugDetector;
    [SerializeField] private GameObject ModelObject = null;
    [Space]
    [SerializeField] private Rigidbody Rigidbody;

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
        animator = GetComponent<Animator>();
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
            Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);

            //Add movement speed
            move *= fSpeed;

            //Account for jumping/gravity
            move.y = Rigidbody.velocity.y;

            Rigidbody.AddRelativeForce(move);

            //Rotate model into move direction
            if (ModelObject != null && !isDragged)
            {
                Vector3 temp = Rigidbody.velocity;
                temp.y = 0;
                ModelObject.transform.rotation = Quaternion.LookRotation(temp);
            }
        }

    }
    #endregion

    #region Button Call Functions
    private void Jump()
    {
        if (Grounded())
        {
            Rigidbody.velocity = Vector3.up * fJumpStrength;
        }
    }

    private void Grab_Start()
    {
        draggedPlug = PlugDetector.GrabPlug();
        if (draggedPlug != null)
        {
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
        }
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

    #endregion

    #region Public Extra Functions
    public void PlayAnimation(string _name)
    {
        if (animator != null)
        {

        }
    }
    
    public void PlayerReset()
    {
        transform.position = Vector3.up * 5;
    }
    #endregion

    #region Private Extra Functions
    private bool Grounded()
    {
        Vector3 halfExtend = new Vector3(0.15f, 0.1f, 0.15f);
        if (Physics.CheckBox(transform.position, halfExtend, transform.rotation, groundMask))
        {
            return true;
        }
        return false;
    }
    #endregion
}