using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : ResourceBar
{
    private PlayerMana playerMana;

    private void Start()
    {
        playerMana = Object.FindAnyObjectByType<Character>().GetComponent<PlayerMana>();
        SetMax(playerMana.maxMana);
        EventSystem.events.OnPlayerManaSpent += Lower;
        EventSystem.events.OnPlayerManaRecovered += Raise;
    }

    private void OnDestroy()
    {
        EventSystem.events.OnPlayerManaSpent -= Lower;
        EventSystem.events.OnPlayerManaRecovered -= Raise;
    }
}
