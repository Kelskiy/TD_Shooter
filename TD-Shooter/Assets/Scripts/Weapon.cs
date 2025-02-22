using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public List<Transform> muzzle = new List<Transform>();

    public Bullet bullet;

    public float fireRate;

    private float lastTimeFired = 0;

    public void Fire()
    {
        //bulletSpawned = Instantiate(bullet.transform, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        if (lastTimeFired + fireRate < Time.time)
        {
            for (int i = 0; i < muzzle.Count; i++)
            {
                Bullet bulletSpawned = Instantiate(bullet, muzzle[i].position, muzzle[i].rotation);
            }

            lastTimeFired = Time.time;
        }
    }
}
