using UnityEngine;

public class DeadRbFollowingThisGO : MonoBehaviour
{
    private Transform currentChild;

    void Update()
    {
        FollowGameObject();
    }

    private void FollowGameObject()
    {
        currentChild.position = transform.position;
        currentChild.rotation = transform.rotation;
    }

    public void SetNewChild(Transform newChild)
    {
        currentChild = newChild;
    }
}
