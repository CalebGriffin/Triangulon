using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelSpawner : MonoBehaviour
{
    //GameObjects to get information on the ship and the fuel prefab
    public GameObject fuel;
    public GameObject ship;

    // Start is called before the first frame update
    void Start()
    {
        // Starts the respawn function when the game starts so that there is always a fuel on the screen
        Respawn();
    }

    public void Respawn()
    {
        // A boolean to check if a fuel has been spawned
        bool spawned = false;

        // While a suitable place to spawn the fuel hasn't been found, run this loop
        while (!spawned)
        {
            // Find a random point on the screen
            Vector3 newPosition = new Vector3(Random.Range(10f, -10f), Random.Range(5f, -7f), 0f);

            // If the random position is too close to the ship then run the loop again, otherwise spawn the fuel at the random point
            if ((newPosition - ship.transform.position).magnitude < 5)
            {
                continue;
            }
            else
            {
                Instantiate(fuel, newPosition, Quaternion.identity);
                // Set the boolean to true to stop the loop from running again
                spawned = true;
            }
            
        }
    }
}
