using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoredState : StateMachineBehaviour
{
    [SerializeField]
    private float timeUntilBored;

    [SerializeField]
    private int numberOfBoredAnims;

    private bool isBored;
    private float idleTime;
    private int boredAnimNumber;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ResetIdle();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(isBored == false)
        {
            idleTime += Time.deltaTime;

            if(idleTime > timeUntilBored && stateInfo.normalizedTime % 1 < 0.02f)
            {
                isBored = true;
                boredAnimNumber = Random.Range(1, numberOfBoredAnims + 1);

                
            }
        }
        else if (stateInfo.normalizedTime % 1 > 0.98)
        {
            ResetIdle();
        }

        animator.SetFloat("BoredAnim", boredAnimNumber, 0.5f, Time.deltaTime);
    }

    private void ResetIdle()
    {
        isBored = false;
        idleTime = 0;

        boredAnimNumber = 0;

    }

}
