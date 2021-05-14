using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    // GameObject to control the flames
    public GameObject enemyFlames;

    // Floats to make random values for x and y
    public float ranX;
    public float ranY;

    // Update is called once per frame
    void Update()
    {
        // Generate a random value for x and y
        ranX = Random.Range(0.8f, 1.0f);
        ranY = Random.Range(2f, 3f);

        // Set the size of the flame based on the random values
        enemyFlames.transform.localScale = new Vector3(ranX, ranY, 1);
    }
}