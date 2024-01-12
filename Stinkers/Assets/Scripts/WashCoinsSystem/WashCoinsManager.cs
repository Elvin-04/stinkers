using UnityEngine;

public class WashCoinsManager : MonoBehaviour
{
    private int washCoins;

    public static WashCoinsManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        print("WC = " + washCoins);
    }

    public void AddWashCoins(int washCoinsToAdd)
    {
        washCoins += washCoinsToAdd;
        print("WC = " + washCoins);
    }

    public void RemoveWashCoins(int washCoinsToRemove)
    {
        if(washCoins -  washCoinsToRemove >= 0) 
        {
            washCoins -= washCoinsToRemove;
        }
        else
        {
            washCoins = 0;
        }
        print("WC = " + washCoins);
    }


    public int GetWashCoins() {  return washCoins; }
}