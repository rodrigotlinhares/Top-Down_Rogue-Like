using System;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private ReflectedArrow arrow;
    public bool reflecting = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<EnemyArrow>() && reflecting)
        {
            EnemyArrow eArrow = collision.gameObject.GetComponent<EnemyArrow>();
            Vector3 eRotation = eArrow.transform.rotation.eulerAngles;
            Vector3 eVelocity = eArrow.GetComponent<Rigidbody2D>().velocity;

            Vector3 cloneRotation = new Vector3(eRotation.x, eRotation.y, eRotation.z + 180f);
            ReflectedArrow clone = Instantiate(arrow, eArrow.transform.position, Quaternion.Euler(cloneRotation));
            clone.GetComponent<Rigidbody2D>().AddForce(Vector3.Reflect(eVelocity, eVelocity).normalized * 200);
        }
    }
}
