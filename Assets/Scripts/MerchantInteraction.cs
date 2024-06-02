using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantInteraction : MonoBehaviour
{   
    [SerializeField]
    public GameObject merchantDialogue;
    [SerializeField]
    public GameObject playerDialogue;
    [SerializeField]
    public GameStateInfoSO gameState;

    private List<string> linesOfDialogue = new();

    // Start is called before the first frame update
    void Start()
    {   
        // Prep the correct dialogue here
        if (gameState.currentLoopCount == 1) {

        } 
        
        // Do the rest of the dialogue here
        else if (false) {

        }

        merchantDialogue.GetComponent<NarrativeSystemScript>().lines = linesOfDialogue.ToArray();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
