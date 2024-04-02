using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    public float moveSpeed = 6;
    public Vector3 dir;
    float xInput, yInput;
    CharacterController controller;

    [SerializeField] LayerMask groundMask;
    Vector3 spherePos;

    [SerializeField] float gravity = -9.81f;
    Vector3 velocity;

    [SerializeField] float jumpForce = 3f;

    public bool isGrounded;
    public float groundDistance = 0.4f;
    public Transform groundCheck;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        GetDirectionAndMove();
        Gravity();
        Jump();
    }
    void GetDirectionAndMove()
    {
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");

        dir = transform.forward * yInput + transform.right * xInput;

        controller.Move(Vector3.ClampMagnitude(dir, 1.0f) * moveSpeed * Time.deltaTime);

        
    }

    void Gravity()
    {
        if (!isGrounded) velocity.y += gravity * Time.deltaTime;
        else if (velocity.y < 0) velocity.y = -2;

        controller.Move(velocity * Time.deltaTime);
    }

    void Jump()
    {
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }

    }

    //DEBUG-- Draws gizmo sphere to see where ground check is, not needed for functionality.
    //private void OnDrawGizmos()
    //{
        //Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(spherePos, controller.radius - 0.05f);
    //}
}
