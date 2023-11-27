using System;
using System.Collections;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    protected IEnumerator Cooldown(Action<bool> flag, float time)
    {
        flag(true);
        yield return new WaitForSeconds(time);
        flag(false);
    }
}
