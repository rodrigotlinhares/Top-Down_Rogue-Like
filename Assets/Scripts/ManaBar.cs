using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : ResourceBar
{
    private PlayerMana playerMana;

    private void OnEnable()
    {
        playerMana = Object.FindAnyObjectByType<Character>().GetComponent<PlayerMana>();
        SetMax(100); //TODO fix
    }

    private void Start()
    {
        EventSystem.events.OnPlayerManaSpent += Lower;
        EventSystem.events.OnPlayerManaRecovered += Raise;
    }

    private void OnDestroy()
    {
        EventSystem.events.OnPlayerManaSpent -= Lower;
        EventSystem.events.OnPlayerManaRecovered -= Raise;
    }
}
