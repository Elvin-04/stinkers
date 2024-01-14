using System.Collections.Generic;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;

public class SprayDetectionCollider : MonoBehaviour
{
    [SerializeField] private Spray spray;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Enemy" && !collision.transform.GetComponent<Stinker>().IsClean())
        {
            spray.ennemiesInRange.Add(collision.transform);
            spray.enemyInRange = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.tag == "Enemy")
        {
            spray.ennemiesInRange.Remove(collision.transform);
            if (spray.ennemiesInRange.Count == 0)
                spray.enemyInRange = false; 
        }
    }

}
