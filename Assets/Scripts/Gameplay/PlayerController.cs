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
        if (Grounded()) velocityG = Vector3.zero;
        else velocityG.y -= fGravity * Time.deltaTime;


        if(CC != null) Movement();
    }

    #endregion

    #region Button Call Functions

    Vector3 velocityG = Vector3.zero;
    float fSpeed = 8.0f;

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

            transform.rotation = Quaternion.LookRotation(move);
        }

        if (velocityG.magnitude > 0.0f) CC.Move(velocityG * Time.deltaTime);
    }

    private void Jump()
    {
        if (CC != null && Grounded())
        {
            velocityG.y = 8.0f;
            CC.Move(velocityG * Time.deltaTime);
        }
    }

    private void Grab_Start()
    {
        Debug.Log("You started Grabbing!");
    }

    private void Grab_End()
    {
        Debug.Log("You stopped Grabbing!");
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

    public bool AreFalling()
    {
        if (true)
        {
            return true;
        }
        return false;
    }

    #endregion
    #region Private Extra Functions

    private bool Grounded()
    {
        float distance = 0.1f;

        if (Physics.CheckSphere(transform.position, distance, groundMask))
        {
            return true;
        }
        return false;
    }

    #endregion

}
