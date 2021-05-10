using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelSpawner : MonoBehaviour
{
    public GameObject fuel;
    public GameObject ship;

    // Start is called before the first frame update
    void Start()
    {
        Respawn();
    }

    public void Respawn()
    {
        bool spawned = false;
        while (!spawned)
        {
            Vector3 newPosition = new Vector3(Random.Range(10f, -10f), Random.Range(5f, -7f), 0f);
            if ((newPosition - ship.transform.position).magnitude < 5)
            {
                continue;
            }
            else
            {
                Instantiate(fuel, newPosition, Quaternion.identity);
                spawned = true;
            }
            
        }
    }
}
