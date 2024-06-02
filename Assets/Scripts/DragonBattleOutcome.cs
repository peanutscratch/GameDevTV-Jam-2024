using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonBattleOutcome : MonoBehaviour
{
    float totalAttack = 0.0F;
    float totalDefense = 0.0F;
    float totalMagic = 0.0F;

    [SerializeField]
    GameObject scorchedSprite;
    [SerializeField]
    GameObject chompSprite;
    [SerializeField]
    GameObject yamchaDeathSprite;
    //[SerializeField]
    // GameObject victorySprite;


    // Start is called before the first frame update
    void Start()
    {   
        scorchedSprite.SetActive(false);
        chompSprite.SetActive(false);
        yamchaDeathSprite.SetActive(false);
        // victorySprite.SetActive(false);

        List<ObjectData> purchasedItems = HeroBehavior.getPurchasedItemsList();
        Debug.Log("Current list of purchased items is " + purchasedItems.Count + " long");
        foreach (ObjectData item in purchasedItems) {
            Debug.Log("Evaluating the stats of: " + item.ItemInformation.name);
            totalAttack += item.ItemInformation.attack;
            totalDefense += item.ItemInformation.defense;
            totalMagic += item.ItemInformation.magic;
        }

        // Purge hero's inventory for the next iteration
        HeroBehavior.clearPurchasedItemsList();

        // First phase evaluation
        if (totalDefense < 20.0F) {
            Debug.Log("The hero's defenses were overwhelmed and they were cooked to a crisp.");
            scorchedSprite.SetActive(true);
            return;
        } else if (totalAttack < 10.0F) {
            Debug.Log("The hero's attack power was too weak, and they were cooked to a crisp.");
            scorchedSprite.SetActive(true);
            return;
        }  else if (totalMagic < 5.0F) {
            Debug.Log("The hero's equipment couldn't survive the magic of the dragon's breath...");
            scorchedSprite.SetActive(true);
            return;
        }
            

        // Second phase evaluation
        if (totalDefense < 40.0F) {
            Debug.Log("The dragon's claws pierced the hero's defenses");
            chompSprite.SetActive(true);
            return;
        } else if (totalAttack < 30.0F) {
            Debug.Log("Not much blood from the creature... seems the hero couldn't pierce the dragon's hide.");
            chompSprite.SetActive(true);
            return;
        } else if (totalMagic < 20.0F) {
            Debug.Log("The hero's magical protections were shattered in the heat of battle.");
            chompSprite.SetActive(true);
            return;
        }

        // Third phase evaluation
        if (totalDefense < 50.0F) {
            Debug.Log("At the battle's end, the hero's defenses faltered, and the dragon narrowly defeated them.");
            yamchaDeathSprite.SetActive(true);
            return;
        } else if (totalAttack < 50.0F) {
            Debug.Log("The hero hacked at the dragon's head, but couldn't pierce its mighty skull.");
            yamchaDeathSprite.SetActive(true);
            return;
        } else if (totalMagic < 40.0F) {
            Debug.Log("The hero didn't have enough magic to sunder the beast's protections.");
            yamchaDeathSprite.SetActive(true);
            return;
        }

        // TODO: Add image in the case of success on the hero's part
        Debug.Log("The hero fought valiantly... and won! Blissfully unaware of how we toiled for their victory, they set onwards to the next town.");
        // victorySprite.SetActive(true);
    }
}
