using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsUI : StatsUI
{
    [SerializeField] private Slider staminaBar;
    private PlayerStats playerStats;

    protected override void Start()
    {
        base.Start();
        playerStats = GetComponent<PlayerStats>();
        playerStats.OnStaminaChangedHandler += OnStaminaChanged;

        staminaBar.value = 1;
    }

    private void OnStaminaChanged(float maxStamina, float currentStamina)
    {
        staminaBar.value = currentStamina / maxStamina;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        playerStats.OnStaminaChangedHandler -= OnStaminaChanged;
    }
}
