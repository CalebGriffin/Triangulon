using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuShip : MonoBehaviour
{
    public GameObject ship;
    public GameObject quitText;
    public GameObject playText;
    public GameObject instructionsText;
    public GameObject backText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ship.transform.Rotate(0, 0, 90);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ship.transform.Rotate(0, 0, -90);
        }

        if (Input.GetKeyDown("space"))
        {
            switch (ship.transform.rotation.z)
            {
                case 0:
                    if (playText.active == true)
                    {
                        SceneManager.LoadScene("MainScene");
                    }
                    break;
                
                case float n when (n < 0):
                    if (quitText.active == true)
                    {
                        Application.Quit();
                    }
                    else
                    {
                        instructionsText.SetActive(false);
                        backText.SetActive(false);

                        playText.SetActive(true);
                        quitText.SetActive(true);
                    }
                    break;
                
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
