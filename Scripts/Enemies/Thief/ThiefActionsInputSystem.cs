using UnityEngine;

public class ThiefActionsInputSystem : ActionsInputSystem
{
    private ThiefAI ai;

    private void Awake()
    {
        ai = GetComponent<ThiefAI>();
        ai.OnReturnedToBaseWithStolenHandler += EnterThrowAction;
    }

    private void EnterThrowAction()
    {
        Debug.Log("return");
        OnThrowActionEnteredHandler?.Invoke();
    }
}
