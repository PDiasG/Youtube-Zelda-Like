using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public Inventory playerInventory;
    public Text coinsText;

    private void Awake()
    {
        UpdateCoinCount();
    }

    public void UpdateCoinCount()
    {
        coinsText.text = playerInventory.coins.ToString("0000");
    }
}
