using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGun : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private Transform canon;

    [Header("Parameters")]
    [SerializeField] private float range = 10f;


    //temp
    [SerializeField] private Transform enemy;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Shoot();

        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.transform.position = spawnPosition.position;
        bullet.GetComponent<WaterGunBullet>().SetDestination(enemy.position);
    }
}
