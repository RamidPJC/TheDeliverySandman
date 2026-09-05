using UnityEngine;
using System.Collections.Generic;
using System;

public class InventorySystem : MonoBehaviour
{
    [SerializeField] private List<InventorySlot> slots;
    public Action OnAddedToInventoryHandler;

    public void TryAddItem(GameObject obj)
    {
        Item item = obj.GetComponent<Item>();
        if (!item)
            return;

        foreach (InventorySlot slot in slots)
        {
            if (slot.GetEmptyState())
            {
                item.gameObject.SetActive(false);
                slot.Add(item);
                OnAddedToInventoryHandler?.Invoke();
                break;
            }
        }
    }

    public void PullOutItem(InventoryItem item)
    {
        Vector3 offset = transform.forward * 10;
        offset.y += 5;
        Vector3 instantiatePointWithOffset = transform.position + offset;

        item.actualItem.transform.position = instantiatePointWithOffset;
        item.actualItem.SetActive(true);

        Destroy(item.gameObject);
    }
}
