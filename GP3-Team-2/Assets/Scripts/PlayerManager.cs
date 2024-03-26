using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public CharacterController controller;
    //Set up reference to the player controller.
    public Transform cam;
    //Set up reference to camera transform.
    public Transform groundCheck;
    //Set up reference to groundcheck transform.

    public float speed = 6f;
    //Speed of movement.
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

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        //Gets input from horizontal/vertical axes.
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        //Saves movement direction from input, normalizes to account for diagonals.

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
            controller.Move(moveDirection.normalized * speed * Time.deltaTime);
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


    }

    public void RefreshStats()
    {
        maxHealth = 100 + (10 * healthLevel);
        maxStam = 100 + (10 * stamLevel);
        strength = 10 + (2 * strLevel);
    }
    //"Refreshes" the values of your stats based on their level.
}
