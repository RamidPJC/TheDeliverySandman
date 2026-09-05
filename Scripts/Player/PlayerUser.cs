using UnityEngine;

public class PlayerUser : User
{
    [SerializeField] Camera cam;

    public override RaycastHit GetRay()
    {
        Ray ray = cam.ScreenPointToRay(
        new Vector3(Screen.width * 0.5f, Screen.height * 0.5f)
    );

        if (Physics.Raycast(ray, out var hit, 1000f))
            return hit;

        RaycastHit fakeHit = new RaycastHit();
        fakeHit.point = ray.origin + ray.direction * 1000f;
        return fakeHit;
    }
}
