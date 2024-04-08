using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public CharacterController controller;
    //Set up reference to the player controller.
    public Transform cam;
    //Set up reference to camera transform.
    public Transform groundCheck;
    //Set up reference to groundcheck transform.
    public Camera mainCam;

    [SerializeField] private float moveSpeed;
    public float walkTopSpeed = 5;
    public float sprintTopSpeed = 9;
    //Speed of movement.

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
    float horizontal, vertical;

    [Header("Capture Parameters")]
    [SerializeField] GameObject net; 
    [SerializeField] Transform throwPos; 
    public float netVelocity;
    public float throwUpwardForce;

    [Header("Fire Parameters")]
    [SerializeField] GameObject bulletPrefab; 
    [SerializeField] Transform firePos; 
    Ray fireRayCast; 
    private Rigidbody bulletRigidbody;
    public float bulletSpeed = 100f;
    private float bulletTimeToDestroy = 3f;
    [SerializeField] Transform crosshair;


    public float gravity = -9.81f;
    Vector3 velocity;
    //Sets up gravity.
    public float jumpHeight = 3f;
    
    public float groundDistance = 0.4f;
    //Sets up distance for groundcheck.
    public LayerMask groundMask;
    //Layer for ground.
    bool isGrounded;
    //True/False for if player is grounded.
    int doubleJump = 1;
    //Double Jump Variable.

    public float turnSmoothTime = 0.1f;
    //Time the rotation of the player should take.
    float turnSmoothVelocity;

    public bool hasDoubleJump;
    //Bool Determining if you can Double Jump.

    int maxHealth;
    public int healthLevel = 1;
    int maxStam;
    public int stamLevel = 1;
    int strength;
    public int strLevel = 1;
    //Stats and associated levels

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerStamina = maxStamina;
        if(healthLevel == 0){
            SceneManager.LoadScene("Game Over");
        }
        RefreshStats();
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        //Checks for ground below player with ground tag.

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            doubleJump = 1;
        }
        //Properly resets velocity.

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        //Gets input from horizontal/vertical axes.
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        //Saves movement direction from input, normalizes to account for diagonals.

        transform.rotation = Quaternion.Euler(0f, cam.eulerAngles.y, 0f);

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            //Gets the angle of the direction being moved in.
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            //Smooths out the movement angle over time.
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            //Applies the rotation of the movement angle onto the player.

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            //Ensures the player moves in the correct direction with the camera.
            controller.Move(moveDirection.normalized * moveSpeed * Time.deltaTime);
            //Applies movement to the character controller, based on direction and speed, framerate independent.

        }

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        if(hasDoubleJump && Input.GetButtonDown("Jump") && !isGrounded && doubleJump > 0)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            doubleJump -= 1;
        }

        velocity.y += gravity * Time.deltaTime;
        //Sets velocity based on gravity value.
        controller.Move(velocity * Time.deltaTime);
        //Applies vertical movement based on velocity.

        Sprint();
        ThrowNet();
        Fire();
    }

    public void RefreshStats()
    {
        maxHealth = 100 + (10 * healthLevel);
        maxStam = 100 + (10 * stamLevel);
        strength = 10 + (2 * strLevel);
    }

    public void Sprint()
    {
        // Check if the player is moving
        if (horizontal != 0 || vertical != 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }


        if(playerStamina > 0)
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


                playerStamina -= staminaDrain * Time.deltaTime; 

            }

            if (playerStamina < 0)
                playerStamina = 0;
        }
        else { moveSpeed = walkTopSpeed; }


        if (playerStamina == 0)
            canSprint = false; 

        if (!Input.GetButton("Sprint") && playerStamina < maxStamina && regeneratingStamina == null)
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

    public void ThrowNet()
    {    
        if (Input.GetButtonDown("Sprint"))
        {
            GameObject currentNet = Instantiate(net, throwPos.position, throwPos.rotation);
            Rigidbody rb = currentNet.GetComponent<Rigidbody>();
            //rb.AddForce(throwPos.forward * netVelocity);

            Vector3 forceToAdd = throwPos.transform.forward * netVelocity + transform.up * throwUpwardForce;
            rb.AddForce(forceToAdd, ForceMode.Impulse);
        } 
    }

    public void Fire()
    {   
        if(Input.GetButtonDown("Fire"))
        {
            Debug.Log("Fired!");
            fireRayCast = mainCam.ScreenPointToRay(crosshair.position);
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

        /*if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("Fired");
            GameObject bullet = GameObject.Instantiate(bulletPrefab, firePos.position, firePos.rotation);
        }*/
    
    
    //"Refreshes" the values of your stats based on their level.
}
