using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpChoice : MonoBehaviour
{
    [SerializeField] private int index;
    [SerializeField] private Color color;

    public void Enable()
    {
        gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = color;
    }

    public void Disable()
    {
        gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void ChoosePowerUp()
    {
        EventSystem.events.PowerUpChosen(index);
        EventSystem.events.MenuClosed();
    }
}
