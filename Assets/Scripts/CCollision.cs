using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCollision : MonoBehaviour
{
    [SerializeField] protected int duration;
    protected Stun stun;
    protected Movement movement;

    protected void Awake()
    {
        stun = GetComponent<Stun>();
    }
}
