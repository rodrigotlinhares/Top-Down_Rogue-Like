using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BloodMageAttack : MonoBehaviour
{
    public void Resize(Vector2 v1, Vector2 v2)
    {
        GetComponent<LineRenderer>().SetPosition(0, v1);
        GetComponent<LineRenderer>().SetPosition(1, v2);
        GetComponent<CircleCollider2D>().transform.position = v2;
    }
}
