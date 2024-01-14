using System.IO;
using TMPro;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI sellPrice;
    [SerializeField] private TextMeshProUGUI upgradePrice;

    public void SetPrices(string sell, string upgrade = "")
    {
        sellPrice.text = sell + "wc";
        upgradePrice.text = upgrade + "wc";
    }
}
