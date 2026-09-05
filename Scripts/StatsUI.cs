using UnityEngine;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour
{
    [SerializeField] private Slider healthBar;

    private DamageableStats stats;

    protected virtual void Start()
    {
        stats = GetComponent<DamageableStats>();
        stats.OnTookDamageHandler += OnTookDamage;

        healthBar.value = 1;
    }

    private void OnTookDamage(int maxHP, int currentHP)
    {
        healthBar.value = (float)currentHP / (float)maxHP;
    }

    protected virtual void OnDestroy()
    {
        stats.OnTookDamageHandler -= OnTookDamage;
    }
}
