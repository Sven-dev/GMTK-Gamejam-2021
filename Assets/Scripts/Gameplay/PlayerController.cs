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

    [SerializeField] float fSpeed = 2.0f;
    [SerializeField] float fJumpStrength = 3.5f;
    [SerializeField] private float GrabForce = 500;
    [Space]
    [SerializeField] LayerMask groundMask = 0;
    [Space]
    [SerializeField] private PlugDetector PlugDetector;
    [SerializeField] private GameObject ModelObject = null;
    [Space]
    [SerializeField] private Rigidbody Rigidbody;

    private Rigidbody draggedPlug = null;
    private bool isDragged = false;

    private Vector3 velocityG = Vector3.zero;
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
        //Grabbing
        if (draggedPlug != null)
        {
            Grabbing();
        }

        //Moving
        if (moveInput.sqrMagnitude > 0.0f)
        {
            //Get movement input
            Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);

            //Rotate model into move direction
            if (ModelObject != null && !isDragged)
                ModelObject.transform.rotation = Quaternion.LookRotation(move);

            //Add movement speed
            move *= fSpeed * Time.fixedDeltaTime;

            //Account for jumping/gravity
            move.y = Rigidbody.velocity.y;

            Rigidbody.velocity = move;
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
            plug.CharacterJoint.connectedBody = Rigidbody;

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
            plug.CharacterJoint.connectedBody = plug.SelfConnector;

            PlugDetector.PlugCable(draggedPlug);
            draggedPlug = null;
        }
    }

    private void Grabbing()
    {
        if (draggedPlug != null)
        {
            Vector3 direction = (transform.position + ModelObject.transform.up * 0.15f + ModelObject.transform.forward * 0.15f - draggedPlug.position);

            if (direction.magnitude > 0.01f)
            {
                Vector3 force = direction * GrabForce;
                draggedPlug.velocity = force;

                if (direction.magnitude > 0.4f)
                {
                    isDragged = true;

                    //ModelObject.transform.LookAt(draggedPlug.position);
                    //ModelObject.transform.localEulerAngles =
                        //new Vector3(0.0f, ModelObject.transform.localEulerAngles.y, 0.0f);

                    //Rigidbody.position += (-direction * fSpeed * 10 * direction.magnitude * Time.fixedDeltaTime);
                }
                else
                {
                    isDragged = false;
                }
            }
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