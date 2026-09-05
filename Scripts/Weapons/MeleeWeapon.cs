using UnityEngine;

public class MeleeWeapon : Weapon
{
    private void OnCollisionEnter(Collision collision)
    {
        if (isAbleToDealDamage)
        {
            if (collision.transform.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.ApplyDamage(amountOfDamageToDeal);
            }
        }
    }
}
