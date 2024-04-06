using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : MovementBaseState
{
    public override void EnterState(SimpleMovement movement)
    {
        
    }

    public override void UpdateState(SimpleMovement movement)
    {
        if(movement.dir.magnitude > 0.1f)
        {
            movement.SwitchState(movement.moveChar);
        }
    }
}
