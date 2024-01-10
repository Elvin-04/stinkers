using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGunBullet : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private Transform destination;

    private void FixedUpdate()
    {
        if (destination != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination.position, speed * Time.deltaTime);
        }
    }


    public void SetDestination(Transform position)
    {
        destination = position;
    }
}
