using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HeroBehavior : MonoBehaviour, IPointerEnterHandler
{
    // Hero's starting purchasing power
    public float willingToPay = 100.00F;

    // Grabs table items and then assigns the hero's purchasing priority
    public void OnPointerEnter(PointerEventData eventData) {
        Transform[] childArray = GetComponentsInChildren<Transform>();

        foreach (Transform a in childArray) {
            Debug.Log(a.ToString() + "\n");
        }
    }
}
