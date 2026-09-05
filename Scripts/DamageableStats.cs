using System;
using UnityEngine;

public abstract class DamageableStats : MonoBehaviour, IDamageable
{
    [SerializeField] private int HP;
    private int maxHP;

    public Action OnDiedHandler;
    public Action OnGotHitHandler;
    public Action<int, int> OnTookDamageHandler;

    private void Start()
    {
        maxHP = HP;
    }

    public void ApplyDamage(int amountOfDamage)
    {
        HP -= amountOfDamage;
        Debug.Log("HP: " + HP);
        OnTookDamageHandler?.Invoke(maxHP, HP);

        if (HP <= 0)
        {
            OnDiedHandler?.Invoke();
        }
        else
        {
            OnGotHitHandler?.Invoke();
        }
    }
}
