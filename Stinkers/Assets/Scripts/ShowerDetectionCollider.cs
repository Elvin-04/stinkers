using UnityEngine;

public class ShowerDetectionCollider : MonoBehaviour
{
    [SerializeField] private Shower shower;

    private int enemyInRange = 0;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Enemy")
        {
            shower.ennemiesInRange.Add(collision.transform);
            enemyInRange++;
            shower.enemyInRange = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.tag == "Enemy")
        {
            shower.ennemiesInRange.Remove(collision.transform);
            enemyInRange--;
            if (enemyInRange == 0)
                shower.enemyInRange = false;
        }
    }
}
