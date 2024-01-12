using TMPro;
using UnityEngine;

public class WashCoinsManager : MonoBehaviour
{
    private int washCoins;

    public TextMeshProUGUI washCoinsText;

    private void Start()
    {
        washCoins = 0;
        UpdateText();
    }

    public void AddWashCoins(int washCoinsToAdd)
    {
        washCoins += washCoinsToAdd;
        UpdateText();
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
        UpdateText();
    }

    public void UpdateText()
    {
        washCoinsText.text = "Wash Coins : " + washCoins;
    }

    public int GetWashCoins() {  return washCoins; }
}