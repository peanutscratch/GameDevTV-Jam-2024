using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseItem : MonoBehaviour
{   
    [SerializeField]
    public GameStateInfoSO gameState;

    [SerializeField]
    public ObjectsDatabaseSO shopInventory;

    [SerializeField]
    public GameObject dimensions;

    public void purchaseItemFromMerchant() {
        ItemScriptableObject itemToBePurchased = gameState.currentSelectedItem;
        if (gameState.totalCurrencyAvailable >= itemToBePurchased.cost) {
            gameState.totalCurrencyAvailable -= itemToBePurchased.cost;
            Debug.Log("Purchased " + itemToBePurchased.name + " for a cost of: " + itemToBePurchased.cost);
            ObjectData itemToAdd = new();
            itemToAdd.Name = itemToBePurchased.name;
            itemToAdd.isPromotion = false;
            itemToAdd.Size = Vector2Int.one;
            itemToAdd.Prefab = dimensions; 
            itemToAdd.ItemInformation = itemToBePurchased;
            shopInventory.objectsData.Add(itemToAdd);
        }
        else {
            Debug.Log("You cannot afford this item!");
        }
    }
}
