using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementState : MovementBaseState
{
    public override void EnterState(SimpleMovement movement)
    {
        movement.anim.SetBool("isMoving", true);
    }

    public override void UpdateState(SimpleMovement movement)
    {
        if (movement.dir.magnitude > 0.1f)
        {
            movement.SwitchState(movement.moveChar);
        }
    }
}