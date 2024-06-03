using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    public GameStateInfoSO gameState;

    public void PlayGame() {
        gameState.currentLoopCount = 1;
        gameState.totalCurrencyAvailable = 500;
        SceneManager.LoadScene("Hero_animations");
    }

    public void QuitGame() {
        Debug.Log("The game has been quit");
        Application.Quit();
    }
}
