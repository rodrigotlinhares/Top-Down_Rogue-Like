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
    public void PlayerDamageTaken(int amount)
    {
        OnPlayerDamageTaken?.Invoke(amount);
    }

    public Action<float> OnEnemyLeechDamageTaken;
    public void EnemyLeechDamageTaken(float amount)
    {
        OnEnemyLeechDamageTaken?.Invoke(amount);
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
}
