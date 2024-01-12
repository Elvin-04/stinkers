using System.Collections.Generic;
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
        if(stenchOfSchool.GetStenchOfSchool() >= 100)
        {
            print("u louzeuh");
        }
        stinker.GetComponent<Stinker>().SetWayPoints(wayToGoToHisPlace);
        //stinkersInSchool.Add(stinker);
        //stinkersToCheck.Remove(stinker);
    }

    public int WashCoinsWon(Stinker stinker)
    {
        int result = (int)(stinker.GetMaxValue() * (stinker.GetStartStinkPercentage() - stinker.GetStinkPercentage()) / (stinker.GetStartStinkPercentage()));
        print("WC won = " + result);
        return result;
    }
}