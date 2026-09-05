using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private InventorySystem inventorySystem;

    private Color emptyColor = Color.clear;
    private Color fullColor = Color.white;

    public bool isEmpty = true;

    private InventoryItem currentItem;

    public bool GetEmptyState()
    {
        return isEmpty;
    }

    public void Add(Item item)
    {
        isEmpty = false;
        GameObject itemHolder = Instantiate(Resources.Load<GameObject>("Item"), transform);
        currentItem = itemHolder.AddComponent<InventoryItem>();
        currentItem.actualItem = item.gameObject;
        currentItem.itemIcon = item.itemIcon;
        currentItem.itemName = item.itemName;
        currentItem.currentSlot = this;
        currentItem.inventorySystem = inventorySystem;
    }
}
