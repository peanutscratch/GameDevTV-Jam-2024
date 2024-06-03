using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLoop : MonoBehaviour
{
    [SerializeField]
    private GameStateInfoSO gameState;

    // Start is called before the first frame update
    public void OnClick()
    {
        gameState.currentLoopCount += 1;
        gameState.merchantTalking = true;
        SceneManager.LoadScene("Merchant Restocking");
    }
}
