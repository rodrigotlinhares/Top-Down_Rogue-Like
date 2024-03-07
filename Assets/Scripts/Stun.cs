using UnityEngine;
using static UnityEngine.UI.Image;

public class Stun : MonoBehaviour
{
    [SerializeField] private int force;
    public float duration;

    private Rigidbody2D body;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    public void Activate(Vector2 origin)
    {
        Vector2 direction = (body.position - origin).normalized;
        body.AddForce(direction * force);
    }

    public void Activate(Vector2 origin, float force)
    {
        Vector2 direction = (body.position - origin).normalized;
        body.AddForce(direction * force);
    }
}
