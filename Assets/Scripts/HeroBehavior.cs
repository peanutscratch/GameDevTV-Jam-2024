using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// TODO: Pipe the debug lines from here into the dialogue system
public class HeroBehavior : MonoBehaviour
{   
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

    public void OnClick() {
        placementSystem = FindObjectOfType<PlacementSystem>();
        List<ObjectData> itemsForSale = placementSystem.getPlacedObjects();

        if (checkIfTableIsSparse(itemsForSale)) {
            Debug.Log("Not much of a selection, is it?");
            willingToPay -= 25.0F;
        }

        if (checkIfTableIsLowQuality(itemsForSale)) {
            Debug.Log("I need better gear than this to, you know, save the entire world.");
            willingToPay -= 25.0F;
        }

        if (checkIfItemsAreCheap(itemsForSale)) {
            Debug.Log("I guess you're this dungeon's dollar store? How... quaint.");
            willingToPay -= 25.0F;
        }

        while (willingToPay > 0.0F) {
            ObjectData itemToBuy = computePurchasePriorityAndSelectItem(itemsForSale);
            if (itemToBuy == null) {
                Debug.Log("Looks like there's nothing else I can buy!");
                break;
            }
            Debug.Log("I would like to buy this " + itemToBuy.ItemInformation.name + " please!");
            willingToPay -= itemToBuy.ItemInformation.basePrice;

            reduceBuyingFactor(itemToBuy.ItemInformation.category.ToString());
            // Add logic to remove from grid as well
            itemsForSale.Remove(itemToBuy);
        }

        if (willingToPay <= 0.0F) {
            Debug.Log("I don't have a coin left to spare, thanks!");
        }

        Debug.Log("I think I'm done shopping for now, thanks!");
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
}
