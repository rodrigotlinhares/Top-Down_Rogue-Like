using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
    public static IEnumerator Cooldown(Action<bool> flag, float time)
    {
        flag(true);
        yield return new WaitForSeconds(time);
        flag(false);
    }
}
