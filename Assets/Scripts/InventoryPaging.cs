using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPaging : MonoBehaviour
{
    [SerializeField]
    public ObjectsDatabaseSO shopInventory;
    [SerializeField]
    public GameObject slot1;
    [SerializeField]
    public GameObject slot2;
    [SerializeField]
    public GameObject slot3;
    
    private int offset = -1;

    public void displayNextItems() {
        offset += 1;
        if (offset * 3 >= shopInventory.objectsData.Count) {
            offset = 0;
        }
        slot1.GetComponent<Image>().color = new Color32(255,255,225,190);
        slot2.GetComponent<Image>().color = new Color32(255,255,225,190);
        slot3.GetComponent<Image>().color = new Color32(255,255,225,190);

        if (offset * 3 < shopInventory.objectsData.Count) {
            slot1.GetComponent<TableMetadata>().Name = shopInventory.objectsData[offset * 3].Name;
            slot1.GetComponent<Image>().sprite = shopInventory.objectsData[offset * 3].ItemInformation.itemSprite;
        } else {
            slot1.GetComponent<Image>().color = new Color32(255,255,225,0);
        }

        if (offset * 3 + 1 < shopInventory.objectsData.Count) {
            slot2.GetComponent<TableMetadata>().Name = shopInventory.objectsData[offset * 3 + 1].Name;
            slot2.GetComponent<Image>().sprite = shopInventory.objectsData[offset * 3 + 1].ItemInformation.itemSprite;
        } else {
            slot2.GetComponent<Image>().color = new Color32(255,255,225,0);
        }

        if (offset * 3 + 2 < shopInventory.objectsData.Count) {
            slot3.GetComponent<TableMetadata>().Name = shopInventory.objectsData[offset * 3 + 2].Name;
            slot3.GetComponent<Image>().sprite = shopInventory.objectsData[offset * 3 + 2].ItemInformation.itemSprite;
        } else {
            slot3.GetComponent<Image>().color = new Color32(255,255,225,0);
        }
    }
}
