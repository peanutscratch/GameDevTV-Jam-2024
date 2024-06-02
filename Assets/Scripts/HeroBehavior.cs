using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HeroBehavior : MonoBehaviour
{   
    [SerializeField]
    public GameStateInfoSO gameState;
    [SerializeField]
    public GameObject dialogueBox;
    [SerializeField]
    private static int totalGridArea = 50;
    [SerializeField]
    public PlacementSystem placementSystem;

    [SerializeField]
    public float willingToPay = 100.0F;

    [SerializeField]
    private int LookingForPotion = 10;
    [SerializeField]
    private int LookingForWeapon = 6;
    [SerializeField]
    private int LookingForShield = 6;
    [SerializeField]
    private int LookingForMagic = 4;
    [SerializeField]
    private int LookingForArmor = 3;

    [SerializeField]
    private float discountFactor = 1.5F;
    [SerializeField]
    private float premiumFactor = 1.5F;
    [SerializeField]
    private float enchantedFactor = 2.0F;

    [SerializeField]
    private static List<ObjectData> purchasedItems = new();

    public void OnClick() {

        placementSystem = FindObjectOfType<PlacementSystem>();
        List<ObjectData> itemsForSale = placementSystem.getPlacedObjects();
        List<string> linesofDialogue = new();

        if (checkIfTableIsSparse(itemsForSale)) {
            linesofDialogue.Add("Not much of a selection, is it?");
            Debug.Log("Not much of a selection, is it?");
            willingToPay -= 25.0F;
        }

        if (checkIfTableIsLowQuality(itemsForSale)) {
            linesofDialogue.Add("I need better gear than this to, you know, save the entire world.");
            Debug.Log("I need better gear than this to, you know, save the entire world.");
            willingToPay -= 25.0F;
        }

        if (checkIfItemsAreCheap(itemsForSale)) {
            linesofDialogue.Add("I guess you're this dungeon's dollar store? How... quaint.");
            Debug.Log("I guess you're this dungeon's dollar store? How... quaint.");
            willingToPay -= 25.0F;
        }

        while (willingToPay > 0.0F) {
            ObjectData itemToBuy = computePurchasePriorityAndSelectItem(itemsForSale);
            if (itemToBuy == null) {
                linesofDialogue.Add("Looks like there's nothing else I can buy!");
                Debug.Log("Looks like there's nothing else I can buy!");
                break;
            }
            linesofDialogue.Add("I would like to buy this " + itemToBuy.ItemInformation.name + " please!");
            Debug.Log("I would like to buy this " + itemToBuy.ItemInformation.name + " please!");
            willingToPay -= itemToBuy.ItemInformation.basePrice;
            gameState.SellItem(itemToBuy.ItemInformation.basePrice);

            reduceBuyingFactor(itemToBuy.ItemInformation.category.ToString());
            // Add logic to remove from grid as well
            itemsForSale.Remove(itemToBuy);
            purchasedItems.Add(itemToBuy);
        }

        if (willingToPay <= 0.0F) {
            linesofDialogue.Add("I don't have a coin left to spare, thanks!");
            Debug.Log("I don't have a coin left to spare, thanks!");
        }

        linesofDialogue.Add("I think I'm done shopping for now, thanks!");
        Debug.Log("I think I'm done shopping for now, thanks!");

        dialogueBox.SetActive(true);
        dialogueBox.GetComponent<NarrativeSystemScript>().lines = linesofDialogue.ToArray();
    }

    private ObjectData computePurchasePriorityAndSelectItem(List<ObjectData> itemsForSale) {
        float currentMostValued = float.MinValue;
        ObjectData currentMostValuedObject = null;
        foreach (ObjectData item in itemsForSale) {
            if (item.ItemInformation.basePrice > willingToPay) {
                continue;
            }
            
            float perceivedValue = item.ItemInformation.perceivedValue;
            if (item.ItemInformation.isDiscounted) {
                perceivedValue *= discountFactor;
            }
            if (item.ItemInformation.isPremium) {
                perceivedValue *= premiumFactor;
            }
            if (item.ItemInformation.isEnchanted) {
                perceivedValue *= enchantedFactor;
            }

            string itemCategory = item.ItemInformation.category.ToString();
            if (itemCategory.Equals("Shield")) {
                perceivedValue *= LookingForShield;
            } else if (itemCategory.Equals("Weapon")) {
                perceivedValue *= LookingForWeapon;
            } else if (itemCategory.Equals("Armor")) {
                perceivedValue *= LookingForArmor;
            } else if (itemCategory.Equals("Potion")) {
                perceivedValue *= LookingForPotion;
            } else if (itemCategory.Equals("Magic")) {
                perceivedValue *= LookingForMagic;
            }

            if (perceivedValue > currentMostValued) {
                currentMostValued = perceivedValue;
                currentMostValuedObject = item;
            }
        }

        return currentMostValuedObject;
    }

    // If less than 25% of the table is populated, return true
    private bool checkIfTableIsSparse(List<ObjectData> itemsForSale) {
        float usedArea = 0.0F;
        foreach (ObjectData item in itemsForSale) {
            usedArea += (item.Size.x * item.Size.y);
        }

        float percentageUsed = usedArea / totalGridArea;
        return percentageUsed < 0.25F ? true : false;
    }

    // If more than 50% of the items are low-quality, return true
    private bool checkIfTableIsLowQuality(List<ObjectData> itemsForSale) {
        int totalItemsOnTable = itemsForSale.Count;
        if (totalItemsOnTable == 0) {
            return true;
        }
        int lowQualityItemCount = 0;
        foreach (ObjectData item in itemsForSale) {
            if (item.ItemInformation.isEnchanted || item.ItemInformation.isDiscounted || item.ItemInformation.isPremium) {
                continue;
            }
            lowQualityItemCount++;
        }

        float percentageLowQuality = lowQualityItemCount / totalItemsOnTable;
        return percentageLowQuality > 0.5 ? true : false;
    }

    // If the average price of items on the table is less than $10, return true;
    private bool checkIfItemsAreCheap(List<ObjectData> itemsForSale) {
        int totalItemsOnTable = itemsForSale.Count;
        if (totalItemsOnTable == 0) {
            return true;
        }
        float currentTotalPrice = 0.0F;
        foreach (ObjectData item in itemsForSale) {
            currentTotalPrice += item.ItemInformation.basePrice;
        }
        
        float averagePrice = currentTotalPrice / totalItemsOnTable;
        return averagePrice < 10.0F ? true : false;
    }

    private void reduceBuyingFactor(string itemCategory) {
        if (itemCategory.Equals("Shield")) {
            LookingForShield -= 3;
        } else if (itemCategory.Equals("Weapon")) {
            LookingForWeapon -= 3;
        } else if (itemCategory.Equals("Armor")) {
            LookingForArmor -= 3;
        } else if (itemCategory.Equals("Potion")) {
            LookingForPotion -= 3;
        } else if (itemCategory.Equals("Magic")) {
            LookingForMagic -= 3;
        }
    }



    public static List<ObjectData> getPurchasedItemsList() {
        return purchasedItems;
    }

    public static void clearPurchasedItemsList() {
        purchasedItems.Clear();
    }
}
