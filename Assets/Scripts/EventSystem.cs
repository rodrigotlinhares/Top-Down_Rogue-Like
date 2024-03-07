using System;
using UnityEngine;

public class EventSystem : MonoBehaviour
{
    public static EventSystem events;

    private void Awake()
    {
        events = this;
        DontDestroyOnLoad(gameObject);
    }

    public Action<float> OnPlayerDamageTaken;
    public void PlayerDamageTaken(float amount)
    {
        OnPlayerDamageTaken?.Invoke(amount);
    }

    public Action<float> OnPlayerHealed;
    public void PlayerHealed(float amount)
    {
        OnPlayerHealed?.Invoke(amount);
    }

    public Action<float> OnEnemyLeechDamageTaken;
    public void EnemyLeechDamageTaken(float amount)
    {
        OnEnemyLeechDamageTaken?.Invoke(amount);
    }

    public Action<float> OnPlayerManaSpent;
    public void PlayerManaSpent(float amount)
    {
        OnPlayerManaSpent?.Invoke(amount);
    }

    public Action<float> OnPlayerManaRecovered;
    public void PlayerManaRecovered(float amount)
    {
        OnPlayerManaRecovered?.Invoke(amount);
    }

    public Action OnWarlockExplodeDots;
    public void WarlockExplodeDots()
    {
        OnWarlockExplodeDots?.Invoke();
    }

    public Action OnPlayerDeath;
    public void PlayerDeath()
    {
        OnPlayerDeath?.Invoke();
    }

    public Action OnEnemyDeath;
    public void EnemyDeath()
    {
        OnEnemyDeath?.Invoke();
    }

    public Action OnBloodPoolDissipate;
    public void BloodPoolDissipate()
    {
        OnBloodPoolDissipate?.Invoke();
    }

    public Action<int> OnClassClick;
    public void ClassClick(int id)
    {
        OnClassClick?.Invoke(id);
    }

    public Action OnStageCleared;
    public void StageCleared()
    {
        OnStageCleared?.Invoke();
    }

    public Action OnGamePaused;
    public void GamePaused()
    {
        OnGamePaused?.Invoke();
    }

    public Action OnGameUnpaused;
    public void GameUnpaused()
    {
        OnGameUnpaused?.Invoke();
    }

    public Action OnMenuClosed;
    public void MenuClosed()
    {
        OnMenuClosed?.Invoke();
    }

    public Action OnPowerUpChosen;
    public void PowerUpChosen()
    {
        OnPowerUpChosen?.Invoke();
    }

    public Action<float> OnWarriorAttackSpeedChosen;
    public void WarriorAttackSpeedChosen(float amount)
    {
        OnWarriorAttackSpeedChosen?.Invoke(amount);
    }

    public Action<float> OnWarriorAttackSizeChosen;
    public void WarriorAttackSizeChosen(float amount)
    {
        OnWarriorAttackSizeChosen?.Invoke(amount);
    }

    public Action<float> OnWarriorLeechChosen;
    public void WarriorLeechChosen(float amount)
    {
        OnWarriorLeechChosen?.Invoke(amount);
    }

    public Action<float> OnWarriorThornsChosen;
    public void WarriorThornsChosen(float amount)
    {
        OnWarriorThornsChosen?.Invoke(amount);
    }

    public Action<float> OnWarriorReflectChosen;
    public void WarriorReflectChosen(float amount)
    {
        OnWarriorReflectChosen?.Invoke(amount);
    }

    public Action<float> OnMageManaRegenChosen;
    public void MageManaRegenChosen(float amount)
    {
        OnMageManaRegenChosen?.Invoke(amount);
    }

    public Action<float> OnMageArcaneBoltQuantityChosen;
    public void MageArcaneBoltQuantityChosen(float amount)
    {
        OnMageArcaneBoltQuantityChosen?.Invoke(amount);
    }

    public Action<float> OnMageArcaneBlastSpeedChosen;
    public void MageArcaneBlastSpeedChosen(float amount)
    {
        OnMageArcaneBlastSpeedChosen?.Invoke(amount);
    }

    public Action<float> OnMageArcaneBlastDamageChosen;
    public void MageArcaneBlastDamageChosen(float amount)
    {
        OnMageArcaneBlastDamageChosen?.Invoke(amount);
    }

    public Action<float> OnMageArcaneShieldCooldownChosen;
    public void MageArcaneShieldCooldownChosen(float amount)
    {
        OnMageArcaneShieldCooldownChosen?.Invoke(amount);
    }

    public Action<float> OnRogueAttackSpeedChosen;
    public void RogueAttackSpeedChosen(float amount)
    {
        OnRogueAttackSpeedChosen?.Invoke(amount);
    }

    public Action<float> OnRogueAttackExplosionChosen;
    public void RogueAttackExplosionChosen(float amount)
    {
        OnRogueAttackExplosionChosen?.Invoke(amount);
    }

    public Action<float> OnRogueMovementSpeedChosen;
    public void RogueMovementSpeedChosen(float amount)
    {
        OnRogueMovementSpeedChosen?.Invoke(amount);
    }

    public Action<float> OnRogueDashCooldownChosen;
    public void RogueDashCooldownChosen(float amount)
    {
        OnRogueDashCooldownChosen?.Invoke(amount);
    }

    public Action<float> OnRogueParryDurationChosen;
    public void RogueParryDurationChosen(float amount)
    {
        OnRogueParryDurationChosen?.Invoke(amount);
    }

    public Action<float> OnRogueParryLeechChosen;
    public void RogueParryLeechChosen(float amount)
    {
        OnRogueParryLeechChosen?.Invoke(amount);
    }

    public Action<float> OnBloodMageHealthRegenChosen;
    public void BloodMageHealthRegenChosen(float amount)
    {
        OnBloodMageHealthRegenChosen?.Invoke(amount);
    }

    public Action<float> OnBloodMageLifeDrainStrengthChosen;
    public void BloodMageLifeDrainStrengthChosen(float amount)
    {
        OnBloodMageLifeDrainStrengthChosen?.Invoke(amount);
    }

    public Action<float> OnBloodMageBloodOrbCostChosen;
    public void BloodMageBloodOrbCostChosen(float amount)
    {
        OnBloodMageBloodOrbCostChosen?.Invoke(amount);
    }

    public Action<float> OnBloodMageBloodPoolDurationChosen;
    public void BloodMageBloodPoolDurationChosen(float amount)
    {
        OnBloodMageBloodPoolDurationChosen?.Invoke(amount);
    }

    public Action<float> OnWarlockCorruptionDamageChosen;
    public void WarlockCorruptionDamageChosen(float amount)
    {
        OnWarlockCorruptionDamageChosen?.Invoke(amount);
    }

    public Action<float> OnWarlockCorruptionCooldownChosen;
    public void WarlockCorruptionCooldownChosen(float amount)
    {
        OnWarlockCorruptionCooldownChosen?.Invoke(amount);
    }

    public Action<float> OnWarlockExplosionCooldownChosen;
    public void WarlockExplosionCooldownChosen(float amount)
    {
        OnWarlockExplosionCooldownChosen?.Invoke(amount);
    }

    public Action<float> OnWarlockDemonSizeChosen;
    public void WarlockDemonSizeChosen(float amount)
    {
        OnWarlockDemonSizeChosen?.Invoke(amount);
    }

    public Action<float> OnWarlockDemonKnockbackChosen;
    public void WarlockDemonKnockbackChosen(float amount)
    {
        OnWarlockDemonKnockbackChosen?.Invoke(amount);
    }
}
