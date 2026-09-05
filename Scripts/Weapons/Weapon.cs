using UnityEngine;

public class Weapon : Grabable
{
    [SerializeField] protected int amountOfDamageToDeal;

    protected bool isAbleToDealDamage;

    public override void GetGrabbed()
    {
        base.GetGrabbed();

        isAbleToDealDamage = true;
    }

    public override void GetReleased()
    {
        base.GetReleased();

        isAbleToDealDamage = false;
    }
}
