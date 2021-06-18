using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    #region Variable Declaration
    // An array of gameobjects that will store all of the different spawn points
    public GameObject[] respawns;
    // GameObjects so that the script can spawn different prefabs
    public GameObject enemy;
    public GameObject shooter;
    public GameObject boss;
    public GameObject enemyToSpawn;

    // The reference within the array so that the script knows which spawn point to use
    public int spawnPoint;

    // Vector3 of the spawn position
    public Vector3 spawnPointVec;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        // Fills the GameObject array with all of the objects that are tagged with "Respawn"
        respawns = GameObject.FindGameObjectsWithTag("Respawn");

        // Starts an infintie loop coroutine that spawns enemies
        StartCoroutine("Spawn");
    }

    IEnumerator Spawn()
    {
        // Sets the timer to a random number based on the global variables
        gVar.spawnTimer = Random.Range(gVar.gSpawnTimer, gVar.gSpawnTimer - 2);

        // Waits for the random period of time
        yield return new WaitForSeconds(gVar.spawnTimer);

        // Picks a random number between 1 and 24 to control which enemy will be spawned
        gVar.enemyRandomiser = Random.Range(1, 24);

        // Picks a random spawn point
        spawnPoint = Random.Range(0, 51);

        // Sets the position of the random spawn point
        spawnPointVec = new Vector3(respawns[spawnPoint].transform.position.x, respawns[spawnPoint].transform.position.y, respawns[spawnPoint].transform.position.z);

        switch (gVar.enemyRandomiser)
        {
            // Spawns the boss if the random number is between 0 and 5
            case int n when (n > 0 && n < 5):
                enemyToSpawn = Instantiate(boss, spawnPointVec, Quaternion.identity);
                break;
            
            // Spawns the shooter at a random size if the random number is between 5 and 10
            case int n when (n > 4 && n < 11):
                spawnPointVec = new Vector3(17, 6, 0);
                enemyToSpawn = Instantiate(shooter, spawnPointVec, Quaternion.identity);
                enemyToSpawn.transform.localScale = Vector3.one * Random.Range(0.25f, 0.4f);
                break;

            // Spawns an enemy at a random size if the random number is greater than 10
            case int n when (n > 10):
                enemyToSpawn = Instantiate(enemy, spawnPointVec, Quaternion.identity);
                enemyToSpawn.transform.localScale = Vector3.one * Random.Range(0.3f, 0.5f);
                break;
            
            default:
                break;
        }

        // Sets the timer to a random number based on the global variables
        gVar.spawnTimer = Random.Range(gVar.gSpawnTimer, gVar.gSpawnTimer - 2);

        // Repeats the coroutine indefinitely
        StartCoroutine("Spawn");
    }
}
