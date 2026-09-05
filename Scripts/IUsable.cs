using UnityEngine;

public interface IUsable
{
    public void Use(IUserContext context);

    public void StopUse();
}
