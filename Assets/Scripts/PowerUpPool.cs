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
            new PowerUp("Increase attack speed", EventSystem.events.WarriorAttackSpeedChosen),
            new PowerUp("Increase attack size", EventSystem.events.WarriorAttackSizeChosen),
            new PowerUp("Attacks leech health", EventSystem.events.WarriorLeechChosen),
            new PowerUp("Geting hit / blocking reflects damage", EventSystem.events.WarriorThornsChosen),
            new PowerUp("Blocking reflects projectiles", EventSystem.events.WarriorReflectChosen)
        };
        List<PowerUp> magePowerUps = new List<PowerUp>
        {
            new PowerUp("Increase mana regeneration", EventSystem.events.MageManaRegenChosen),
            new PowerUp("Increase mana pick-up range", EventSystem.events.MageManaPickUpChosen),
            new PowerUp("Increase Arcane Bolt damage", EventSystem.events.MageArcaneBoltDamageChosen),
            new PowerUp("Increase Arcane Blast cast speed", EventSystem.events.MageArcaneBlastSpeedChosen),
            new PowerUp("Increase Arcane Blast damage", EventSystem.events.MageArcaneBlastDamageChosen),
            new PowerUp("Increase Arcane Shield duration", EventSystem.events.MageArcaneShieldDurationChosen),
            new PowerUp("Lower Arcane Shield cooldown", EventSystem.events.MageArcaneShieldCooldownChosen),
            new PowerUp("Increase Arcane Shield mana recovery", EventSystem.events.MageArcaneShieldManaRegenChosen)
        };
        List<PowerUp> roguePowerUps = new List<PowerUp>
        {
            new PowerUp("Increase attack speed", EventSystem.events.RogueAttackSpeedChosen),
            new PowerUp("Attacks may explode", EventSystem.events.RogueAttackExplosionChosen),
            new PowerUp("Increase movement speed", EventSystem.events.WarriorAttackSpeedChosen),
            new PowerUp("Lower Dash cooldown", EventSystem.events.RogueDashCooldownChosen),
            new PowerUp("Increase Parry duration", EventSystem.events.RogueParryDurationChosen),
            new PowerUp("Parrying may restore health", EventSystem.events.RogueParryLeechChosen),
            new PowerUp("Parrying may stun enemies", EventSystem.events.RogueParryStunChosen)
        };
        List<PowerUp> bloodMagePowerUps = new List<PowerUp>
        {
            new PowerUp("Regenerate health over time", EventSystem.events.BloodMageHealthRegenChosen),
            new PowerUp("Increase Life Drain health drain", EventSystem.events.BloodMageLifeDrainStrengthChosen),
            new PowerUp("Increase Life Drain area of effect", EventSystem.events.BloodMageLifeDrainAreaChosen),
            new PowerUp("Blood Orb consumes less health", EventSystem.events.BloodMageBloodOrbCostChosen),
            new PowerUp("Increase Blood Pool duration", EventSystem.events.BloodMageBloodPoolDurationChosen),
            new PowerUp("Lower Blood Pool cooldown", EventSystem.events.BloodMageBloodPoolCooldownChosen)
        };
        List<PowerUp> warlockPowerUps = new List<PowerUp>
        {
            new PowerUp("Increase Corruption damage", EventSystem.events.WarlockCorruptionDamageChosen),
            new PowerUp("Lower Corruption cooldown", EventSystem.events.WarlockCorruptionCooldownChosen),
            new PowerUp("Increase Corruption health drain", EventSystem.events.WarlockCorruptionLeechChosen),
            new PowerUp("Increase Explosion size", EventSystem.events.WarlockExplosionSizeChosen),
            new PowerUp("Lower Explosion cooldown", EventSystem.events.WarlockExplosionCooldownChosen),
            new PowerUp("Increase Demon size", EventSystem.events.WarlockDemonSizeChosen),
            new PowerUp("Increase Demon knockback strength", EventSystem.events.WarlockDemonKnockbackChosen)
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
