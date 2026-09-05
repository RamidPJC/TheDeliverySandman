using UnityEngine;

public class PlayerActionsInputSystem : ActionsInputSystem
{
    void Update()
    {
        if (Input.GetButtonDown("Remove"))
            OnRemoveActionEnteredHandler?.Invoke();
        if (Input.GetButtonDown("Throw"))
            OnThrowActionEnteredHandler?.Invoke();
        if (Input.GetButtonDown("Select"))
            isUseActionHolded = true;
        if (Input.GetButtonUp("Select"))
            isUseActionHolded = false;
    }
}
