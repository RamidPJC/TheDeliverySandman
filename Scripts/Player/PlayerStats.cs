using System;
using UnityEngine;

public class PlayerStats : DamageableStats
{
    [SerializeField] private float maxStamina;
    private float currentStamina;

    [SerializeField] private float amountOfStaminaToDecreasePerFrame;
    [SerializeField] private float amountOfStaminaToIncreasePerFrame;

    private bool isRunning;

    public event Action OnStaminaOverHandler;

    public event Action<float, float> OnStaminaChangedHandler;

    private Rigidbody rb;
    [SerializeField] private Camera cam;

    private void Awake()
    {
        currentStamina = maxStamina;
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (isRunning)
        {
            if (currentStamina > 0)
            {
                DecreaseStamina();
                OnStaminaChangedHandler?.Invoke(maxStamina, currentStamina);
            }
            else
            {
                currentStamina = 0;
                OnStaminaOverHandler?.Invoke();
            }
        }
        else
        {
            if (currentStamina < maxStamina)
            {
                IncreaseStamina();
                OnStaminaChangedHandler?.Invoke(maxStamina, currentStamina);
            }
            else
            {
                currentStamina = maxStamina;
            }
        }
    }

    private void IncreaseStamina()
    {
        currentStamina += amountOfStaminaToIncreasePerFrame;
    }

    private void DecreaseStamina()
    {
        currentStamina -= amountOfStaminaToDecreasePerFrame;
    }

    public void SetRunningState(bool running)
    {
        isRunning = running;
    }

    public bool GetRunningState()
    {
        return isRunning;
    }

    public bool CanRun()
    {
        return currentStamina > 0;
    }
}
