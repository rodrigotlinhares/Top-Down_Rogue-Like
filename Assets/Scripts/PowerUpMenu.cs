using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PowerUpMenu : MonoBehaviour
{
    private void Start()
    {
        EventSystem.events.OnStageCleared += Show;
        EventSystem.events.OnPowerUpChosen += Hide;
    }

    private void OnDestroy()
    {
        EventSystem.events.OnStageCleared -= Show;
        EventSystem.events.OnPowerUpChosen -= Hide;
    }

    private void Show()
    {

        List<PowerUp> powerUps = PowerUpPool.Get3RandomPowerUps();
        for (int i = 0; i < powerUps.Count; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            child.SetActive(true);
            child.GetComponent<PowerUpChoice>().SetPowerUp(powerUps[i]);
        }
    }

    private void Hide()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(false);
    }
}
