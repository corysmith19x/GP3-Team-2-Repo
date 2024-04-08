using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    public StatsInventoryManager stats;

    public Vector3 dir;
    float xInput, yInput;
    CharacterController controller;
    [SerializeField] float gravity = -9.81f;
    Vector3 velocity;

    [Header("Movement Parameters")]
    public float staminaDrain;
    public bool canSprint;
    public bool isMoving;
    private float timeBeforeRegen  = 3;
    private float staminaIncrement = 2;
    private float staminaTimeIncrement = 0.1f;
    private Coroutine regeneratingStamina;
    [SerializeField] float jumpForce = 6f;
    float moveSpeed;
    public float walkTopSpeed;
    public float sprintTopSpeed;

    [Header("Grounded Parameters")]
    public bool isGrounded;
    public float groundDistance = 0.4f;
    public Transform groundCheck;
    [SerializeField] LayerMask groundMask;
    Vector3 spherePos;

    [Header("Fire Parameters")]
    [SerializeField] GameObject bulletPrefab; 
    [SerializeField] Transform firePos; 
    Ray fireRayCast; 
    private Rigidbody bulletRigidbody;
    public float bulletSpeed = 100f;
    private float bulletTimeToDestroy = 3f;
    [SerializeField] Transform crosshair;
    public Camera playerCam;

    [Header("Capture Parameters")]
    [SerializeField] GameObject net; 
    [SerializeField] Transform throwPos; 
    public float netVelocity;
    public float throwUpwardForce;

    [Header("Stat Parameters")]

    MovementBaseState currentState;

    public IdleState Idle = new IdleState();
    public MovementState moveChar = new MovementState();

    public Animator anim;

    void Awake()
    {
        stats = GetComponent<StatsInventoryManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        controller = GetComponent<CharacterController>();
        SwitchState(Idle);
    }

    // Update is called once per frame
    void Update()
    {
        CheckGrounded();
        GetDirectionAndMove();
        Gravity();
        Sprint();
        Jump();
        Fire();
        ThrowNet();

        anim.SetFloat("hzInput", xInput);
        anim.SetFloat("vInput", yInput);

        currentState.UpdateState(this);
    }

    public void SwitchState(MovementBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    void GetDirectionAndMove()
    {
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");

        dir = transform.forward * yInput + transform.right * xInput;

        controller.Move(Vector3.ClampMagnitude(dir, 1.0f) * moveSpeed * Time.deltaTime);

        if (dir.magnitude > 0.1f)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
    }

    void Gravity()
    {
        if (!isGrounded) velocity.y += gravity * Time.deltaTime;
        else if (velocity.y < 0) velocity.y = -2;

        controller.Move(velocity * Time.deltaTime);
    }

    void CheckGrounded()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }

    void Jump()
    {
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }

    }
    
    public void Fire()
    {   
        if(Input.GetButtonDown("Fire"))
        {
            fireRayCast = playerCam.ScreenPointToRay(crosshair.position);
            RaycastHit hit;
            if(Physics.Raycast(fireRayCast, out hit))
            {
                GameObject bullet = GameObject.Instantiate(bulletPrefab, firePos.position, Quaternion.identity);
                Vector3 shootDirection = hit.point - firePos.position;

                bulletRigidbody = bullet.GetComponent<Rigidbody>();

                if(bulletRigidbody != null)
                {
                    bulletRigidbody.velocity = shootDirection.normalized * bulletSpeed; 
                } 
                Destroy(bullet, bulletTimeToDestroy);
            }
        }
    }

    public void ThrowNet()
    {    
        if (Input.GetButtonDown("Special"))
        {
            GameObject currentNet = Instantiate(net, throwPos.position, throwPos.rotation);
            Rigidbody rb = currentNet.GetComponent<Rigidbody>();
            //rb.AddForce(throwPos.forward * netVelocity);

            Vector3 forceToAdd = throwPos.transform.forward * netVelocity + transform.up * throwUpwardForce;
            rb.AddForce(forceToAdd, ForceMode.Impulse);
        } 
    }

    public void Sprint()
    {
        // Check if the player is moving
        if (xInput != 0 || yInput != 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }


        if(stats.playerStam > 0)
            canSprint = true;


        if (Input.GetButton("Sprint") && canSprint)
        {
            if (Input.GetButton("Sprint") && isMoving)
            {
                if (regeneratingStamina != null)
                {
                    StopCoroutine(regeneratingStamina);
                    regeneratingStamina = null;
                }

                moveSpeed = sprintTopSpeed;


                stats.playerStam -= staminaDrain * Time.deltaTime; 

            }

            if (stats.playerStam < 0)
                stats.playerStam = 0;
        }
        else { moveSpeed = walkTopSpeed; }


        if (stats.playerStam == 0)
            canSprint = false; 

        if (!Input.GetButton("Sprint") && stats.playerStam < stats.playerMaxStam && regeneratingStamina == null)
        {
            regeneratingStamina = StartCoroutine(RegenStamina());
        }
    }

    private IEnumerator RegenStamina()
    {
        yield return new WaitForSeconds(timeBeforeRegen);
        WaitForSeconds timeToWait = new WaitForSeconds(staminaTimeIncrement);


        while(stats.playerStam < stats.playerMaxStam)
        {
            if (stats.playerStam > 0)
                canSprint = true;

            stats.playerStam += staminaIncrement;

            if (stats.playerStam > stats.playerMaxStam)
                stats.playerStam = stats.playerMaxStam;

            yield return timeToWait;
        }

        regeneratingStamina = null;
    }

    

    //DEBUG-- Draws gizmo sphere to see where ground check is, not needed for functionality.
    //private void OnDrawGizmos()
    //{
        //Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(spherePos, controller.radius - 0.05f);
    //}
}
