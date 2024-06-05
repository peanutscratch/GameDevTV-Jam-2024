using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NextPage : MonoBehaviour
{
    [SerializeField]
    public MerchantSupplySO merchantSupply;
    [SerializeField]
    public GameObject slot1;
    [SerializeField]
    public GameObject slot2;
    [SerializeField]
    public GameObject slot3;
    [SerializeField]
    public GameObject slot4;
    public int offset = 0;

    public void moveToSellingScene() {
        SceneManager.LoadScene("Hero_animations");
    }

    public void nextPageOfMerchantWares() {
        offset += 1;
        if (offset * 4 >= merchantSupply.merchantStock.Count) {
            offset = 0;
        }
        slot1.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color32(255,255,225,190);
        slot2.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color32(255,255,225,190);
        slot3.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color32(255,255,225,190);
        slot4.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color32(255,255,225,190);

        if (offset * 4 < merchantSupply.merchantStock.Count) {
            slot1.GetComponent<ItemSlotScript>().item = merchantSupply.merchantStock[offset * 4];
            slot1.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = merchantSupply.merchantStock[offset * 4].itemSprite;
        } else {
            slot1.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color32(255,255,225,0);
        }

        if (offset * 4 + 1 < merchantSupply.merchantStock.Count) {
            slot2.GetComponent<ItemSlotScript>().item = merchantSupply.merchantStock[offset * 4 + 1];
            slot2.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = merchantSupply.merchantStock[offset * 4 + 1].itemSprite;
        } else {
            slot2.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color32(255,255,225,0);
        }

        if (offset * 4+ 2 < merchantSupply.merchantStock.Count) {
            slot3.GetComponent<ItemSlotScript>().item = merchantSupply.merchantStock[offset * 4 + 2];
            slot3.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = merchantSupply.merchantStock[offset * 4 + 2].itemSprite;
        } else {
            slot3.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color32(255,255,225,0);
        }

        if (offset * 4 + 3 < merchantSupply.merchantStock.Count) {
            slot4.GetComponent<ItemSlotScript>().item = merchantSupply.merchantStock[offset * 4 + 3];
            slot4.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = merchantSupply.merchantStock[offset * 4 + 3].itemSprite;
        } else {
            slot4.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color32(255,255,225,0);
        }
    }
}

