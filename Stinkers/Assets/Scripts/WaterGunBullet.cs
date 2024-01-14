using UnityEngine;

public class WaterGunBullet : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private Transform destination;
    public WaterGun gun;

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

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            other.transform.GetComponent<Stinker>().UpdateStinkPercentage(gun.damages);
            if(other.transform.GetComponent<Stinker>().IsClean())
            {
                gun.SetEnemy(null);
            }
            Destroy(this.gameObject);
        }
    }
}
