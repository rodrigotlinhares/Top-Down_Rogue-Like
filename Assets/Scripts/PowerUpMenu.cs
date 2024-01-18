using System.Collections;
using System.Collections.Generic;
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
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(true);
        transform.GetChild(2).gameObject.SetActive(true);
    }

    private void Hide()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(false);
    }
}
