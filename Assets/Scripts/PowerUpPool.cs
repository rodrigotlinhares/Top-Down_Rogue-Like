using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PowerUpPool
{
    private static IDictionary<int, List<PowerUp>> powerUps = new Dictionary<int, List<PowerUp>>();

    static PowerUpPool()
    {
        List<PowerUp> warriorPowerUps = new List<PowerUp>
        {
            new PowerUp("Increase attack speed", EventSystem.events.WarriorAttackSpeedChosen),
            new PowerUp("Increase attack size", EventSystem.events.WarriorAttackSpeedChosen),
            new PowerUp("Increase leech", EventSystem.events.WarriorAttackSpeedChosen)
        };
        List<PowerUp> magePowerUps = new List<PowerUp>
        {
            new PowerUp("Increase attack speed", EventSystem.events.WarriorAttackSpeedChosen),
            new PowerUp("Increase attack size", EventSystem.events.WarriorAttackSpeedChosen),
            new PowerUp("Increase leech", EventSystem.events.WarriorAttackSpeedChosen)
        };
        List<PowerUp> roguePowerUps = new List<PowerUp>
        {
            new PowerUp("Increase attack speed", EventSystem.events.WarriorAttackSpeedChosen),
            new PowerUp("Increase attack size", EventSystem.events.WarriorAttackSpeedChosen),
            new PowerUp("Increase leech", EventSystem.events.WarriorAttackSpeedChosen)
        };
        List<PowerUp> bloodMagePowerUps = new List<PowerUp>
        {
            new PowerUp("Increase attack speed", EventSystem.events.WarriorAttackSpeedChosen),
            new PowerUp("Increase attack size", EventSystem.events.WarriorAttackSpeedChosen),
            new PowerUp("Increase leech", EventSystem.events.WarriorAttackSpeedChosen)
        };
        List<PowerUp> warlockPowerUps = new List<PowerUp>
        {
            new PowerUp("Increase attack speed", EventSystem.events.WarriorAttackSpeedChosen),
            new PowerUp("Increase attack size", EventSystem.events.WarriorAttackSpeedChosen),
            new PowerUp("Increase leech", EventSystem.events.WarriorAttackSpeedChosen)
        };
        powerUps.Add(0, warriorPowerUps);
        powerUps.Add(1, magePowerUps);
        powerUps.Add(2, roguePowerUps);
        powerUps.Add(3, bloodMagePowerUps);
        powerUps.Add(4, warlockPowerUps);
    }

    public static List<PowerUp> Get3RandomPowerUps()
    {
        return powerUps[PlayerPrefs.GetInt("classID")];
    }
}
