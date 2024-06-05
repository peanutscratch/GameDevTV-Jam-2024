using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroDialogue : MonoBehaviour
{
    [SerializeField]
    public GameObject heroDialogue;
    [SerializeField]
    public GameStateInfoSO gameState;

    private List<string> diag = new();

    void Start()
    {
        if (gameState.heroTalking && gameState.currentLoopCount == 1) {
            diag.Add("Wow this inventory is lame, I'm not interested in buying anything you have.");
            diag.Add("I'll come back later after I've killed the dragon, hopefully you have something good by then!"); 
            heroDialogue.GetComponent<NarrativeSystemScript>().lines = diag.ToArray();
        } else {
            diag.Add("Hey these wears aren't so bad! Normally stores like yours don't have anything good, but you got some stuff to show!");
            diag.Add("It's rare for a store to make such a good first impression!");
            heroDialogue.GetComponent<NarrativeSystemScript>().lines = diag.ToArray();
        }
    }
}
