using UnityEngine;

public class BloodMage : Player
{
    [SerializeField]
    private BloodMageAttack attack;
    [SerializeField]
    private BloodMageProjectile projectile;
    private BloodMageAttack attackClone;
    private int projectileForce = 250;
    private bool attacking = false;

    void Update()
    {
        if (movement.enabled)
        {
            movement.Move();
            if (Input.GetKeyDown(KeyCode.Mouse0))
                BeingBeam();
            if (Input.GetKey(KeyCode.Mouse0) && attacking)
                Beam(Input.mousePosition);
            if (Input.GetKeyUp(KeyCode.Mouse0) && attacking)
                StopBeam();
            if (Input.GetKeyDown(KeyCode.Mouse1))
                Shoot(Input.mousePosition);
        }
    }
    private void BeingBeam()
    {
        attacking = true;
        attackClone = Instantiate(attack, body.transform);
    }

    private void Beam(Vector3 target)
    {
        if (attackClone)
            attackClone.Resize(body.position, (Vector2)Camera.main.ScreenToWorldPoint(target));
    }

    private void StopBeam()
    {
        attacking = false;
        Destroy(attackClone.gameObject);
    }
    private void Shoot(Vector3 target)
    {
        Vector2 direction = ((Vector2)Camera.main.ScreenToWorldPoint(target) - body.position).normalized;
        BloodMageProjectile clone = Instantiate(projectile, body.transform);
        clone.GetComponent<Rigidbody2D>().AddForce(direction * projectileForce);
        health.Lower(10);
        EventSystem.events.OnPlayerDamageTaken(10);
    }
}
