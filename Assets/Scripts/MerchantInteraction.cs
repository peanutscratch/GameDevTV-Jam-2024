using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantInteraction : MonoBehaviour
{   
    [SerializeField]
    public GameObject merchantImage;
    [SerializeField]
    public GameObject heroImage;
    [SerializeField]
    public GameObject heroDialogue;
    [SerializeField]
    public ObjectsDatabaseSO shopInventory;
    [SerializeField]
    public GameObject merchantDialogue;
    [SerializeField]
    public GameObject playerDialogue;
    [SerializeField]
    public GameStateInfoSO gameState;
    [SerializeField]
    public ItemScriptableObject potionISO;
    [SerializeField]
    public GameObject merchantPanels;
    [SerializeField]
    public GameObject potionPrefab;

    private List<string> diag = new();
    private bool startBuying = false;

    // Start is called before the first frame update
    void Start()
    {   
        // Nightly merchant shopping time
        if (gameState.merchantTalking) {
            merchantDialogue.SetActive(true);
            merchantImage.SetActive(true);
            diag.Add("You don't have much stock left. You can buy anything from my catalog that you want to sell! Don't worry, I only charge wholesale prices.");
            diag.Add("And every order comes with a free healing potion. You look like you need it."); 
            merchantDialogue.GetComponent<NarrativeSystemScript>().lines = diag.ToArray();
            ObjectData potion = new();
            potion.Name = "Health Potion";
            potion.isPromotion = false;
            potion.Size = Vector2Int.one;
            potion.Prefab = potionPrefab;
            potion.ItemInformation = potionISO;
            shopInventory.objectsData.Add(potion);
        }
    }

    void Update() {
        if (!gameState.merchantTalking) {
            if (!startBuying) {
                startBuying = true;
                Debug.Log("Starting buying phase");
                merchantPanels.SetActive(true);
            }            
        }
    }

    void OpenUpShopMenu() {
        Debug.Log("Opening shop menu");
    }
}
