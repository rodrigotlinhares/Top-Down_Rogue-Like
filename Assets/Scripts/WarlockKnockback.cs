using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarlockKnockback : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.CompareTag("Wall"))
            Destroy(gameObject);
    }
}
