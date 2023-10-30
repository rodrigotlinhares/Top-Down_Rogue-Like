using UnityEngine;

public class BloodMage : Character
{
    [SerializeField]
    private BloodMageAttack attack;

    private BloodMageAttack attackClone;
    private bool attacking = false;

    void Update()
    {
        if (movement.enabled)
        {
            movement.Move();
            if (Input.GetKeyDown(KeyCode.Mouse0))
                BeginAttacking();
            if (Input.GetKey(KeyCode.Mouse0) && attacking)
                Attack(Input.mousePosition);
            if (Input.GetKeyUp(KeyCode.Mouse0) && attacking)
                StopAttacking();
        }
    }
    private void BeginAttacking()
    {
        attacking = true;
        attackClone = Instantiate(attack, body.transform);
    }

    private void Attack(Vector3 target)
    {
        if (attackClone)
            attackClone.Resize(body.position, (Vector2)Camera.main.ScreenToWorldPoint(target));
    }

    private void StopAttacking()
    {
        attacking = false;
        if (attackClone)
            Destroy(attackClone.gameObject);
    }
}
