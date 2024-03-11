using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementStateManager : MonoBehaviour
{
    [Header("Movement Parameters")]
    [SerializeField] private float moveSpeed;
    private float walkTopSpeed = 5;
    private float sprintTopSpeed = 9;
    [HideInInspector] public Vector3 dir; //direction
    float xInput, yInput;
    CharacterController controller;

    [Header("Jump Parameters")]
    public float jumpHeight = 3f;

    [SerializeField] float groundYOffset;
    [SerializeField] LayerMask groundMask;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    //Vector3 spherePos;

    [SerializeField] float gravity = -9.81f;
    Vector3 velocity;

    [Header("Stamina/Sprint Parameters")]
    public float playerStamina; 
    public float maxStamina = 100f; 
    public float staminaDrain;
    bool canSprint;
    bool isMoving;
    private float timeBeforeRegen  = 3;
    private float staminaIncrement = 2;
    private float staminaTimeIncrement = 0.1f;
    private Coroutine regeneratingStamina;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerStamina = maxStamina;
    }

    // Update is called once per frame
    void Update()
    {
        GetDirectionAndMove();
        Gravity();
        Sprint();
        Jump();
    }

    void GetDirectionAndMove()
    {
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");

        dir = transform.forward * yInput + transform.right * xInput;

        controller.Move(dir * moveSpeed * Time.deltaTime);
    }    

    bool IsGrounded()
    {
        /*spherePos = new Vector3(transform.position.x, transform.position.y + groundYOffset, transform.position.z);
        if (Physics.CheckSphere(spherePos, controller.radius - 0.05f, groundMask)) return true;
        return false;*/
        if (Physics.CheckSphere(groundCheck.position, groundDistance, groundMask))
        {
            Debug.Log("Is grounded");
            return true;
        }
        else
        {
            Debug.Log("Not Grounded.");
            return false;
        }
    }

    void Gravity()
    {
        if (!IsGrounded()) velocity.y += gravity * Time.deltaTime;
        else if (velocity.y < 0) velocity.y = -2;

        controller.Move(velocity * Time.deltaTime);
    }

    private void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && IsGrounded() && playerStamina > 10)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            playerStamina -= 10;
        }
    }

    private void Sprint()
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


        if(playerStamina > 0)
            canSprint = true;
        
        
        if (Input.GetKey(KeyCode.LeftShift) && canSprint)
        {
            if (Input.GetKey(KeyCode.LeftShift) && isMoving)
            {   
                if (regeneratingStamina != null)
                {
                    StopCoroutine(regeneratingStamina);
                    regeneratingStamina = null;
                }

                moveSpeed = sprintTopSpeed;

                if (isMoving)
                {
                    playerStamina -= staminaDrain * Time.deltaTime; 
                }
            }    

            if (playerStamina < 0)
                playerStamina = 0;
        }
        else { moveSpeed = walkTopSpeed; }
        

        if (playerStamina == 0)
            canSprint = false; 

        if (!Input.GetKey(KeyCode.LeftShift) && playerStamina < maxStamina && regeneratingStamina == null)
        {
            regeneratingStamina = StartCoroutine(RegenStamina());
        }
    }

    private IEnumerator RegenStamina()
    {
        yield return new WaitForSeconds(timeBeforeRegen);
        WaitForSeconds timeToWait = new WaitForSeconds(staminaTimeIncrement);


        while(playerStamina < maxStamina)
        {
            if (playerStamina > 0)
                canSprint = true;

            playerStamina += staminaIncrement;

            if (playerStamina > maxStamina)
                playerStamina = maxStamina;

            yield return timeToWait;
        }

        regeneratingStamina = null;
    }

    //private void OnDrawGizmos()  --- Gizmo Drawing Debug
    //{
        //Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(spherePos, controller.radius - 0.05f);
    //}
}
