using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameStateInfoSO : ScriptableObject
{
    
    public float totalCurrencyAvailable;
    public int currentLoopCount;
    public bool isDayTime;
    public bool merchantTalking;
    public bool heroTalking;
    public bool playerTalking;

    public void PayForItem(float cost) {
        this.totalCurrencyAvailable -= cost;
    }

    public void SellItem(float basePrice) {
        this.totalCurrencyAvailable += basePrice;
    }
}
