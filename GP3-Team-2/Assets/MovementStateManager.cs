using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementStateManager : MonoBehaviour
{
    public float moveSpeed = 3;
    [HideInInspector] public Vector3 dir; //direction
    float xInput, yInput;
    CharacterController controller;

    [SerializeField] float groundYOffset;
    [SerializeField] LayerMask groundMask;
    Vector3 spherePos;

    [SerializeField] float gravity = -9.81f;
    Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        GetDirectionAndMove();
        Gravity();
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
        spherePos = new Vector3(transform.position.x, transform.position.y + groundYOffset, transform.position.z);
        if (Physics.CheckSphere(spherePos, controller.radius - 0.05f, groundMask)) return true;
        return false;
    }

    void Gravity()
    {
        if (!IsGrounded()) velocity.y += gravity * Time.deltaTime;
        else if (velocity.y < 0) velocity.y = -2;

        controller.Move(velocity * Time.deltaTime);
    }

    //private void OnDrawGizmos()  --- Gizmo Drawing Debug
    //{
        //Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(spherePos, controller.radius - 0.05f);
    //}
}
