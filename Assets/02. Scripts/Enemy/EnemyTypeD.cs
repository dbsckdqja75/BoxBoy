using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTypeD : MonoBehaviour
{

    public float bulletSpeed = 500;

    public bool[] fireActive; // Up Down Left Right
    public Transform[] firePoint; // Up Down Left Right

    public GameObject bulletPrefab;

    public float fireInterval = 1;
    private float timer;

    void Start()
    {
        timer = fireInterval;
    }

    void Update()
    {
        if(timer <= 0)
            Attack();

        if (timer > 0)
            timer -= Time.deltaTime;
    }

    void Attack()
    {
        if (fireActive[0])
            Fire(firePoint[0]);

        if (fireActive[1])
            Fire(firePoint[1]);

        if (fireActive[2])
            Fire(firePoint[2]);

        if (fireActive[3])
            Fire(firePoint[3]);

        timer = fireInterval;
    }

    void Fire(Transform firePoint)
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * bulletSpeed);
    }
}
