using System;
using UnityEngine;

public class Openable : Interactable
{
    [SerializeField] private Animator animator;

    protected bool isOpened;

    public override void Interact()
    {
        if (!isOpened)
        {
            Open();
        }
        else
        {
            Close();
        }
    }

    public virtual void Open()
    {
        isOpened = true;
        animator.SetBool("isOpened", true);
    }

    public virtual void Close()
    {
        isOpened = false;
        animator.SetBool("isOpened", false);
    }

    public bool GetOpenedState()
    {
        return isOpened;
    }

}
