using System;
using Unity.VisualScripting;
using UnityEngine;

public class InteractionSystem : MonoBehaviour
{
    [SerializeField] private Camera cam;
    private Vector3 middleOfScreenPos;

    [SerializeField] private GameObject hand;
    private InteractionHand handController;
    private bool isHandActive;

    [SerializeField] private LayerMask interactableMask;

    [SerializeField] private GameObject middleDot;

    private void Start()
    {
        middleOfScreenPos = new Vector3(Screen.width / 2, Screen.height / 2, 0);

        handController = hand.GetComponent<InteractionHand>();
    }

    private void Update()
    {
        Ray ray = cam.ScreenPointToRay(middleOfScreenPos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 4, interactableMask, QueryTriggerInteraction.Collide))
        {
            if (hit.transform.TryGetComponent<Interactable>(out var interactable))
            {
                isHandActive = true;
                handController.SetNewInteractable(interactable);
                AttachHand(hit);
            }
            else
            {
                if (isHandActive)
                {
                    isHandActive = false;
                    DetachHand();
                    handController.SetNewInteractable(null);
                }
            }
        }
        else
        {
            if (isHandActive)
            {
                isHandActive = false;
                DetachHand();
                handController.SetNewInteractable(null);
            }
        }
    }

    private void AttachHand(RaycastHit hit)
    {
        hand.transform.position = hit.point;
        hand.transform.rotation = Quaternion.LookRotation(hit.normal);

        ReplaceMiddleDot(hand, true);
    }

    private void DetachHand()
    {
        ReplaceMiddleDot(hand, false);
    }

    private void ReplaceMiddleDot(GameObject replacingObject, bool isInteracting)
    {
        replacingObject.SetActive(isInteracting);
        middleDot.SetActive(!isInteracting);
    }
}
