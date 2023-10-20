using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : MonoBehaviour, PlayerController.PlayerClass
{
    public PlayerController Controller { get; set; }
    public int Health { get; set; }

    private bool blocking = false;
    private WarriorAttack attack;
    private WarriorBlock block, blockClone;

    // Start is called before the first frame update
    void Start()
    {
        attack = Resources.Load<WarriorAttack>("Prefabs/WarriorAttack");
        block = Resources.Load<WarriorBlock>("Prefabs/WarriorBlock");
    }

    // Update is called once per frame
    void Update()
    {
        if (Controller.inputEnabled)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
                Attack(Input.mousePosition);
            if (Input.GetKeyDown(KeyCode.Mouse1))
                BeginBlocking();
            if (Input.GetKey(KeyCode.Mouse1) && blocking)
                Block(Input.mousePosition);
            if (Input.GetKeyUp(KeyCode.Mouse1))
                StopBlocking();
        }
    }

    private void Attack(Vector3 target)
    {
        Vector2 direction = ((Vector2)Camera.main.ScreenToWorldPoint(target) - Controller.body.position).normalized;
        WarriorAttack clone = Instantiate(attack, Controller.body.transform);
        clone.transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);
        clone.GetComponent<Rigidbody2D>().AddForce(direction * 350);
    }

    private void BeginBlocking()
    {
        blocking = true;
        blockClone = Instantiate(block, Controller.body.transform);
    }

    private void Block(Vector3 target)
    {
        Vector2 direction = ((Vector2)Camera.main.ScreenToWorldPoint(target) - Controller.body.position).normalized;
        blockClone.transform.position = Controller.body.position + direction;
        blockClone.transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);
    }

    private void StopBlocking()
    {
        blocking = false;
        Destroy(blockClone.gameObject);
    }
}
