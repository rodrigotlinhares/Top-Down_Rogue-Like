using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Explosion : PlayerAttack
{
    private void Awake()
    {
        StartCoroutine(Fade());
    }

    private IEnumerator Fade()
    {
        Animator animator = GetComponent<Animator>();
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
    }
}
