using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodMage : PlayerController
{
    private BloodMageAttack attack, attackClone;
    private bool attacking = false;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = GetComponent<Health>();
        body = GetComponent<Rigidbody2D>();
        movement = GetComponent<PlayerMovement>();
        playerCollision = GetComponent<PlayerCollision>();
        attack = Resources.Load<BloodMageAttack>("Prefabs/BloodMageAttack");
    }

    // Update is called once per frame
    void Update()
    {
        if (inputEnabled)
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
