using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    #region Instance
    //put instance stuff here
    private static PlayerController _Instance;
    public static PlayerController Instance
    {
        get
        {
            return _Instance;
        }
    }
    #endregion

    #region Base Unity Functions

    #region Base Variables

    public GameplayControls inputControls { get; private set; }
    public CharacterController CC { get; private set; }
    public Animator animator { get; private set; }

    #endregion
    #region Serialized Variables

    [SerializeField] LayerMask groundMask = 0;
    [SerializeField] float fGravity = 9.81f;

    #endregion

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

        CC = GetComponent<CharacterController>();
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

    // Update is called once per frame
    void Update()
    {
        if(CC != null && !CC.enabled) CC.enabled = true;

        if (Grounded()) velocityG = Vector3.zero;
        else velocityG.y -= fGravity * Time.deltaTime;

        if (CC != null) Movement();
        if (draggedPlug != null) Grabbing();
    }

    #endregion

    #region Button Call Functions

    [SerializeField] GameObject ModelObject = null;

    Vector3 velocityG = Vector3.zero;

    [SerializeField] float fSpeed = 2.0f;
    [SerializeField] float fJumpStrength = 3.5f;

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

    private void Movement()
    {
        Vector2 moveInput = inputControls.Player.Move.ReadValue<Vector2>();

        if (moveInput.sqrMagnitude > 0.0f)
        {
            Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);

            // Animation deactivate for now
            if (animator != null) animator.SetFloat("vSpeed", move.magnitude);

            CC.Move(move * fSpeed * Time.deltaTime);

            if(ModelObject != null)
                ModelObject.transform.rotation = Quaternion.LookRotation(move);
        }

        if (velocityG.magnitude > 0.0f) CC.Move(velocityG * Time.deltaTime);
    }

    private void Jump()
    {
        if (CC != null && Grounded())
        {
            velocityG.y = fJumpStrength;
            CC.Move(velocityG * Time.deltaTime);
        }
    }

    #region Plug Variables

    [SerializeField] PlugDetector PlugDetector;
    Rigidbody draggedPlug = null;

    #endregion

    private void Grab_Start()
    {
        if (PlugDetector != null)
        {
            draggedPlug = PlugDetector.GrabPlug();

            // Add also Polish here, like animations
            if (draggedPlug != null)
            {
                Plug plig = draggedPlug.GetComponent<Plug>();
                if (plig != null)
                {
                    plig.Disconnect();
                    draggedPlug.constraints = RigidbodyConstraints.None;
                }
            }
            else
            {

            }
        }
    }
    private void Grab_End()
    {
        if (draggedPlug != null)
        {
            PlugDetector.PlugCable(draggedPlug);
            draggedPlug = null;

            // Add also Polish here, like animations
        }
    }

    private void Grabbing()
    {
        if (draggedPlug != null)
        {
            Vector3 direction = (transform.position + ModelObject.transform.up * 0.15f + ModelObject.transform.forward * 0.15f - draggedPlug.position);

            if (direction.magnitude > 0.01f)
            {
                Vector3 force = direction * 500;
                draggedPlug.velocity = force;

                if (direction.magnitude > 0.4f)
                {
                    ModelObject.transform.LookAt(draggedPlug.position);
                    ModelObject.transform.localEulerAngles =
                        new Vector3(0.0f, ModelObject.transform.localEulerAngles.y, 0.0f);

                    // Grab_End();
                    CC.Move(-direction * fSpeed * 10 * direction.magnitude * Time.deltaTime);
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
        CC.enabled = false;

        transform.position = Vector3.up * 5;
        velocityG = Vector3.zero;
    }
    public bool AreFalling()
    {
        if (velocityG.y < 0.0f)
        {
            return true;
        }
        return false;
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
