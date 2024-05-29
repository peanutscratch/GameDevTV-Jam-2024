using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlotScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData) {
        Debug.Log("This slot is being hovered over!");
        transform.localScale += new Vector3(0.1F, 0.1F, 0);
    }

    public void OnPointerExit(PointerEventData eventData) {
        Debug.Log("Goodbye forever :( no longer hovering");
        transform.localScale -= new Vector3(0.1F, 0.1F, 0);
    }
}
