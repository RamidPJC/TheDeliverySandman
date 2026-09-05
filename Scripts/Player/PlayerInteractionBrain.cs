using UnityEngine;

public class PlayerInteractionBrain : InteractionBrain
{
    [SerializeField] private InteractionHand interactionHand;
    [SerializeField] private InventorySystem inventorySystem;
    [SerializeField] private MissionSystem missionSystem;

    protected new void Awake()
    {
        base.Awake();
        interactionHand.OnCatchInteractableHandler += OnCatchInteractable;
        ownerInputSystem.OnRemoveActionEnteredHandler += OnRemoveEntered;
        inventorySystem.OnAddedToInventoryHandler += OnItemAddedToInventory;
        missionSystem.OnMissionCompleted += OnThrowEntered;
    }

    protected override void OnCatchInteractable(Interactable interactable)
    {
        base.OnCatchInteractable(interactable);

        if (currentGrabable)
            Debug.Log("try reg mission");
            missionSystem.TryRegisterGrabable(currentGrabable);
    }

    private void OnRemoveEntered()
    {
        Grabable item = graspingHand.GetCurrentItem();
        if (!item)
            return;

        inventorySystem.TryAddItem(item.gameObject);
    }

    private void OnItemAddedToInventory()
    {
        graspingHand.DismantleCurrentGrabable();
    }
}
