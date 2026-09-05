using UnityEngine;

public class PlayerUIManager : MonoBehaviour
{
    [SerializeField] private GameObject darkBackground;

    [Header("Mission System")]
    [SerializeField] private GameObject missionNote;
    private bool isNoteOpen;

    [Header("Inventory")]
    [SerializeField] private GameObject inventory;
    private bool isInventoryOpen;

    private void Update()
    {
        if (Input.GetButtonDown("OpenNote"))
        {
            Open(missionNote, ref isNoteOpen);
        }

        if (Input.GetButtonDown("OpenInventory"))
        {
            Open(inventory, ref isInventoryOpen);
        }
    }

    private void Open(GameObject uiToOpen, ref bool isOpened)
    {
        if (!isOpened)
        {
            isOpened = true;
            Cursor.lockState = CursorLockMode.Confined;
            darkBackground.SetActive(true);
            uiToOpen.SetActive(true);
        }
        else
        {
            isOpened = false;
            Cursor.lockState = CursorLockMode.Locked;
            darkBackground.SetActive(false);
            uiToOpen.SetActive(false);
        }
        
    }
}
