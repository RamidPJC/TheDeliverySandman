using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private const int BASE_AMOUNT_OF_DAMAGE_TO_DEAL = 10;
    private const float BASE_VELOCITY_MAGNITUDE = 350;
    public Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent<IDamageable>(out var damageable))
        {
            float currentVelo = rb.linearVelocity.magnitude;
            float veloDiff = currentVelo / BASE_VELOCITY_MAGNITUDE;
            int resultDamage = Mathf.RoundToInt(BASE_AMOUNT_OF_DAMAGE_TO_DEAL * veloDiff);
            damageable.ApplyDamage(resultDamage);
        }
        Deactivate();
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
