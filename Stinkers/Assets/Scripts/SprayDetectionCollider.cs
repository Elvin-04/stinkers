using UnityEngine;

public class SprayDetectionCollider : MonoBehaviour
{
    [SerializeField] private Spray spray;

    private int enemyInRange = 0;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Enemy")
        {
            enemyInRange++;
            spray.enemyInRange = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.tag == "Enemy")
        {
            enemyInRange--;
            if (enemyInRange == 0)
                spray.enemyInRange = false; 
        }
    }

}
