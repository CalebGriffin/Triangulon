using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This script was only used for the testing scene and is not present in the final game
public class UIController : MonoBehaviour
{
    // GameObject to control the canvas
    public GameObject startCanvas;

    // Start is called before the first frame update
    void Start()
    {
        // Sets the timescale to 0 so that the game is paused
        Time.timeScale = 0;
    }

    // This can be called by a button to start the game
    public void StartGame()
    {
        // Sets the canvas to false to reveal the game behind it
        startCanvas.SetActive(false);

        // Un-pauses the game by setting the timescale back to 1
        Time.timeScale = 1;
    }
}
