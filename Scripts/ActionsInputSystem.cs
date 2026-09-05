using System;
using UnityEngine;

public abstract class ActionsInputSystem : MonoBehaviour
{
    public Action OnRemoveActionEnteredHandler;
    public Action OnThrowActionEnteredHandler;
    public bool isUseActionHolded;
}
