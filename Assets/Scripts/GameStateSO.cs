using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameStateInfoSO : ScriptableObject
{
    
    public float totalCurrencyAvailable;
    public int currentLoopCount;

    public void PayForItem(float cost) {
        this.totalCurrencyAvailable -= cost;
    }

    public void SellItem(float basePrice) {
        this.totalCurrencyAvailable += basePrice;
    }
}
