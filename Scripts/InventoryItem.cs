using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InventoryItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject actualItem;

    public string itemName;
    public Sprite itemIcon;
    private Image itemImage;
    private RectTransform rectTransform;
    private TextMeshProUGUI itemNameText;

    public InventorySlot currentSlot;
    public InventorySystem inventorySystem;

    private void Start()
    {
        itemImage = GetComponent<Image>();
        itemNameText = GetComponentInChildren<TextMeshProUGUI>();
        rectTransform = GetComponent<RectTransform>();
        itemImage.sprite = itemIcon;
        itemNameText.text = itemName;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        itemNameText.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        itemNameText.enabled = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        itemImage.raycastTarget = false;
        Color halfAlfa = itemImage.color;
        halfAlfa.a = 0.5f;
        itemImage.color = halfAlfa;
    }
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GameObject target = eventData.pointerCurrentRaycast.gameObject;

        if (target)
        {
            target.TryGetComponent<InventorySlot>(out var slot);
            if (slot)
            {
                if (slot.GetEmptyState() && slot != currentSlot)
                {
                    currentSlot.isEmpty = true;
                    currentSlot = null;
                    currentSlot = slot;
                    transform.SetParent(currentSlot.transform);
                    currentSlot.isEmpty = false;
                }
            }
        }
        else
        {
            inventorySystem.PullOutItem(this);
            if (currentSlot)
                currentSlot.isEmpty = true;
            return;
        }

        rectTransform.localPosition = new Vector2(0, 0);

        itemImage.raycastTarget = true;

        itemImage.color = Color.white;
    }
}
