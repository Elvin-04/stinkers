using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGunBullet : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private Vector3 destination;

    private void FixedUpdate()
    {
        if (destination != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
        }
    }


    public void SetDestination(Vector3 position)
    {
        destination = position;
    }
}
