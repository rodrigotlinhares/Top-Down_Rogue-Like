using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class LifeDrain : PlayerAttack
{
    public void Resize(Vector2 p1, Vector2 p2)
    {
        GetComponent<LineRenderer>().SetPosition(0, p1);
        GetComponent<LineRenderer>().SetPosition(1, p2);
        GetComponent<CircleCollider2D>().transform.position = p2;
    }
}
