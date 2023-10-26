using UnityEngine;

public class PlayerController : MonoBehaviour
{
    protected Health currentHealth;
    protected PlayerCollision playerCollision;
    protected PlayerMovement movement;
    protected Rigidbody2D body;
    protected static bool inputEnabled = true;
    protected private int iFrames = 250, knockbackForce = 500;

    public static string className;

    // Start is called before the first frame update
    void Start()
    {
        if (className == "Warrior")
            gameObject.AddComponent<Warrior>();
        else if (className == "Mage")
            gameObject.AddComponent<Mage>();
        else if (className == "Rogue")
            gameObject.AddComponent<Rogue>();
        else if (className == "Blood Mage")
            gameObject.AddComponent<BloodMage>();

        body = GetComponent<Rigidbody2D>();
        GetComponent<SpriteRenderer>().color = ClassStats.stats[className].color;
    }

    //protected IEnumerator TakeDamage(GameObject other)
    //{
    //    inputEnabled = false;
    //    currentHealth.TakeDamage(1);
    //    Vector2 direction = (body.position - other.GetComponent<Rigidbody2D>().position).normalized;
    //    body.AddForce(direction * knockbackForce);
    //    DateTime start = DateTime.Now;
    //    while ((DateTime.Now - start).TotalMilliseconds < iFrames)
    //        yield return null;
    //    inputEnabled = true;
    //}
}