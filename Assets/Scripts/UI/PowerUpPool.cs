using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public static class PowerUpPool
{
    private static IDictionary<int, List<PowerUp>> powerUps = new Dictionary<int, List<PowerUp>>();

    static PowerUpPool()
    {
        List<PowerUp> warriorPowerUps = new List<PowerUp>
        {
            new PowerUp("Increase attack speed", 0.1f, EventSystem.events.WarriorAttackSpeedChosen),
            new PowerUp("Increase attack size", 0.1f, EventSystem.events.WarriorAttackSizeChosen),
            new PowerUp("Attacks leech health", 5f, EventSystem.events.WarriorLeechChosen),
            new PowerUp("Geting hit / blocking reflects damage", 5f, EventSystem.events.WarriorThornsChosen),
            new PowerUp("Blocking reflects projectiles", 0.5f, EventSystem.events.WarriorReflectChosen)
        };
        List<PowerUp> magePowerUps = new List<PowerUp>
        {
            new PowerUp("Increase mana regeneration", 0.05f, EventSystem.events.MageManaRegenChosen),
            new PowerUp("Increase Arcane Bolt quantity", 1f, EventSystem.events.MageArcaneBoltQuantityChosen),
            new PowerUp("Increase Arcane Blast cast speed", 0.1f, EventSystem.events.MageArcaneBlastSpeedChosen),
            new PowerUp("Increase Arcane Blast damage", 5f, EventSystem.events.MageArcaneBlastDamageChosen),
            new PowerUp("Lower Arcane Shield cooldown", 1f, EventSystem.events.MageArcaneShieldCooldownChosen)
        };
        List<PowerUp> roguePowerUps = new List<PowerUp>
        {
            new PowerUp("Increase attack speed", 0.1f, EventSystem.events.RogueAttackSpeedChosen),
            new PowerUp("Attacks may explode", 0.1f, EventSystem.events.RogueAttackExplosionChosen),
            new PowerUp("Increase movement speed", 0.1f, EventSystem.events.RogueMovementSpeedChosen),
            new PowerUp("Lower Dash cooldown", 0.1f, EventSystem.events.RogueDashCooldownChosen),
            new PowerUp("Increase Parry duration", 0.2f, EventSystem.events.RogueParryDurationChosen),
            new PowerUp("Parrying may restore health", 0.1f, EventSystem.events.RogueParryLeechChosen)
        };
        List<PowerUp> bloodMagePowerUps = new List<PowerUp>
        {
            new PowerUp("Regenerate health over time", 0.05f, EventSystem.events.BloodMageHealthRegenChosen),
            new PowerUp("Increase Life Drain health drain", 0.1f, EventSystem.events.BloodMageLifeDrainStrengthChosen),
            new PowerUp("Blood Orb consumes less health", 0.1f, EventSystem.events.BloodMageBloodOrbCostChosen),
            new PowerUp("Increase Blood Pool duration", 0.1f, EventSystem.events.BloodMageBloodPoolDurationChosen)
        };
        List<PowerUp> warlockPowerUps = new List<PowerUp>
        {
            new PowerUp("Increase Corruption damage", 0.1f, EventSystem.events.WarlockCorruptionDamageChosen),
            new PowerUp("Lower Corruption cooldown", 0.1f, EventSystem.events.WarlockCorruptionCooldownChosen),
            new PowerUp("Lower Explosion cooldown", 0.1f, EventSystem.events.WarlockExplosionCooldownChosen),
            new PowerUp("Increase Demon size", 0.1f, EventSystem.events.WarlockDemonSizeChosen),
            new PowerUp("Increase Demon knockback strength", 0.1f, EventSystem.events.WarlockDemonKnockbackChosen)
        };
        powerUps.Add(0, warriorPowerUps);
        powerUps.Add(1, magePowerUps);
        powerUps.Add(2, roguePowerUps);
        powerUps.Add(3, bloodMagePowerUps);
        powerUps.Add(4, warlockPowerUps);
    }

    private static void Shuffle(List<PowerUp> list)
    {
        int i = list.Count;
        while (i > 1)
        {
            i--;
            int k = UnityEngine.Random.Range(0, i);
            PowerUp buffer = list[k];
            list[k] = list[i];
            list[i] = buffer;
        }
    }

    public static List<PowerUp> Draw3RandomPowerUps()
    {
        Shuffle(powerUps[PlayerPrefs.GetInt("classID")]);
        return powerUps[PlayerPrefs.GetInt("classID")].GetRange(0, 3);
    }
}
