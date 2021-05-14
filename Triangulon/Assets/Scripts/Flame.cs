using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour
{
    // GameObject to control the flames
    public GameObject flames;

    // SpriteRenderer to make the flames invisible
    public SpriteRenderer flamesSR;

    // Floats to make random variables for the x and y values
    public float ranX;
    public float ranY;

    // Update is called once per frame
    void Update()
    {
        // Generate random values for x and y
        ranX = Random.Range(0.8f, 1.0f);
        ranY = Random.Range(2f, 3f);

        // Sets the size of the flames based on the size x and y
        flames.transform.localScale = new Vector3(ranX, ranY, 1);

        // If the ship is moving, then make the flames visible
        if (gVar.moving == true)
        {
            flamesSR.enabled = true;
        }
        else
        {
            flamesSR.enabled = false;
        }
    }
}