using System;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    private GameObject bulletPrefab;

    [SerializeField] private int amountOfBullets;
    private Bullet[] bullets;
    private int bulletsLeft;

    private void Awake()
    {
        bulletsLeft = amountOfBullets;
        bulletPrefab = Resources.Load<GameObject>("bullet");
        bullets = new Bullet[amountOfBullets];
        for (int i = 0; i < amountOfBullets; i++)
        {
            GameObject bulletGO = Instantiate(bulletPrefab);
            bulletGO.SetActive(false);
            bullets[i] = bulletGO.GetComponent<Bullet>();
        }
    }

    public Bullet TryGetBullet()
    {
        if (bulletsLeft > 0)
        {
            bulletsLeft--;
            return bullets[bulletsLeft];
        }
        return null;
    }
}
