using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CheckSystem : MonoBehaviour
{
    private List<GameObject> stinkersToCheck;
    private List<GameObject> stinkersInSchool;

    [SerializeField]
    private List<GameObject> wayToGoToHisPlace;

    [SerializeField]
    private StenchOfSchool stenchOfSchool;

    [SerializeField]
    private WashCoinsManager washCoinsManager;

    private bool endSpawn = false;
    private List<GameObject> stinkersSpawned;

    private void Awake()
    {
        stinkersSpawned = new List<GameObject>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Enemy")
        {
            //stinkersToCheck.Add(collision.gameObject);
            Check(collision.gameObject);
        }
    }

    public void Check(GameObject stinker)
    {
        stenchOfSchool.UpdateStenchOfSchool(stinker.GetComponent<Stinker>().GetStinkPercentage());
        washCoinsManager.AddWashCoins(WashCoinsWon(stinker.GetComponent<Stinker>()));
        stinker.GetComponent<Stinker>().UpdateStinkPercentage(stinker.GetComponent<Stinker>().GetStinkPercentage());
        stinkersSpawned.Remove(stinker);
        if (stenchOfSchool.GetStenchOfSchool() >= 100)
        {
            GameManager.instance.EndLevel(false, (int)stenchOfSchool.GetStenchOfSchool());
        }
        else if (endSpawn && stinkersSpawned.Count == 0)
        {
            GameManager.instance.EndLevel(true, (int)stenchOfSchool.GetStenchOfSchool());
        }
        stinker.GetComponent<Stinker>().SetWayPoints(wayToGoToHisPlace);
        //stinkersInSchool.Add(stinker);
        //stinkersToCheck.Remove(stinker);
    }

    public int WashCoinsWon(Stinker stinker)
    {
        int result = (int)(stinker.GetMaxValue() * (stinker.GetStartStinkPercentage() - stinker.GetStinkPercentage()) / (stinker.GetStartStinkPercentage())+5);
        return result;
    }

    public void AddStinkerSpawn(GameObject stinker)
    {
        stinkersSpawned.Add(stinker);
    }

    public void SetEndSpawn() { endSpawn = true; }
}