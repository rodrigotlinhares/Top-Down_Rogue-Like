using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorSlashIdle : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<SpriteRenderer>().flipY = !animator.GetComponent<SpriteRenderer>().flipY;
    }
}
