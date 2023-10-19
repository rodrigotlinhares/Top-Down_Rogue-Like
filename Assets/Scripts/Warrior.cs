using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : MonoBehaviour, PlayerController.PlayerClass
{
    public PlayerController Controller { get; set; }
    public int Health { get; set; }
    private WarriorAttack mainAttack;

    // Start is called before the first frame update
    void Start()
    {
        mainAttack = Resources.Load<WarriorAttack>("Prefabs/WarriorAttack");
        Debug.Log(mainAttack);
    }

    // Update is called once per frame
    void Update()
    {
        if (Controller.inputEnabled)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
                MainAttack(Input.mousePosition);
        }
    }

    private void MainAttack(Vector3 target)
    {
        Vector2 direction = (Vector2)Camera.main.ScreenToWorldPoint(target) - Controller.body.position;
        direction.Normalize();
        WarriorAttack clone = Instantiate(mainAttack, Controller.body.transform);
        clone.transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);
        clone.GetComponent<Rigidbody2D>().AddForce(direction * 350);
    }
}
