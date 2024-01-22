using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PowerUpChoice : MonoBehaviour
{
    [SerializeField] private Color color;
    public PowerUp powerUp;

    public void SetPowerUp(PowerUp pu)
    {
        powerUp = pu;
        GetComponentInChildren<TextMeshProUGUI>().SetText(powerUp.description);
    }

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
        powerUp.eventCall(1f);
        EventSystem.events.PowerUpChosen();
        EventSystem.events.MenuClosed();
    }
}
