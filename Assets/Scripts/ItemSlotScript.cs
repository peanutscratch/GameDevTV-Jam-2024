using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ItemSlotScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField]
    public GameObject previewImageSlot;
    [SerializeField]
    public TextMeshProUGUI attackText;
    [SerializeField]
    public TextMeshProUGUI defenseText;
    [SerializeField]
    public TextMeshProUGUI magicText;
    [SerializeField]
    public TextMeshProUGUI costText;
    [SerializeField]
    public ItemScriptableObject item;
    [SerializeField]
    public GameObject ownImage;
    [SerializeField]
    public GameStateInfoSO gameState;

    void Start() {
        ownImage.GetComponent<Image>().sprite = item.itemSprite;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        transform.localScale += new Vector3(0.1F, 0.1F, 0);
    }

    public void OnPointerExit(PointerEventData eventData) {
        transform.localScale -= new Vector3(0.1F, 0.1F, 0);
    }

    public void populatePreview() {
        previewImageSlot.GetComponent<Image>().sprite = item.itemSprite;
        attackText.text = "Attack is: " + item.attack;
        defenseText.text = "Def is: " + item.defense;
        magicText.text = "Magic is: " + item.magic;
        costText.text = "Cost is: " + item.cost;
        gameState.currentSelectedItem = item;
    }
}
