using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// This script is used to control the ship on the menu screen
public class MenuShip : MonoBehaviour
{
    // GameObject to get information about different objects in the menu
    public GameObject ship;
    public GameObject quitText;
    public GameObject playText;
    public GameObject instructionsText;
    public GameObject backText;

    // Update is called once per frame
    void Update()
    {
        // Rotates the ship if the player presses the left and right keys
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ship.transform.Rotate(0, 0, 90);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ship.transform.Rotate(0, 0, -90);
        }

        // When the player presses the space bar it will check which way the menu ship is facing
        if (Input.GetKeyDown("space"))
        {
            switch (ship.transform.rotation.z)
            {
                // Checks if the player is looking at the play button and then plays the game
                case 0:
                    if (playText.activeSelf == true)
                    {
                        SceneManager.LoadScene("MainScene");
                    }
                    break;
                
                // Checks if the player is trying to quit or go back
                case float n when (n < 0):
                    if (quitText.activeSelf == true)
                    {
                        // Quits the game
                        Application.Quit();
                    }
                    else
                    {
                        // Goes back to the main menu from the controls screen by hiding and showing different text elements
                        instructionsText.SetActive(false);
                        backText.SetActive(false);

                        playText.SetActive(true);
                        quitText.SetActive(true);
                    }
                    break;
                
                // Shows and hides text elements to show the controls menu
                case float n when (n > 0 && n < 180):
                    playText.SetActive(false);
                    quitText.SetActive(false);

                    instructionsText.SetActive(true);
                    backText.SetActive(true);
                    break;
            }
        }
    }
}
