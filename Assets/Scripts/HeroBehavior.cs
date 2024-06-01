using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroBehavior : MonoBehaviour
{

    public PlacementSystem placementSystem;
    public float willingToPay = 100.0F;

    public void OnClick() {
        placementSystem = FindObjectOfType<PlacementSystem>();
        List<GameObject> itemsForSale = placementSystem.getPlacedObjects();

        foreach (GameObject item in itemsForSale) {
            Debug.Log(item.name);
        }
    }
}
