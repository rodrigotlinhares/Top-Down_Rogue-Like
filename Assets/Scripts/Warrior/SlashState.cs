using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorSlash : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<BoxCollider2D>().enabled = true;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<BoxCollider2D>().enabled = false;
    }
}
