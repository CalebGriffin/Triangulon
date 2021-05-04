using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] respawns;
    public GameObject enemy;
    public GameObject shooter;
    public GameObject boss;
    public GameObject enemyToSpawn;

    public int spawnPoint;

    public Vector3 spawnPointVec;

    // Start is called before the first frame update
    void Start()
    {
        respawns = GameObject.FindGameObjectsWithTag("Respawn");

        StartCoroutine("Spawn");
    }

    // Update is called once per frame
    void Update()
    {
        gVar.spawnTimer = Random.Range(gVar.gSpawnTimer, gVar.gSpawnTimer - 2);
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(gVar.spawnTimer);

        gVar.enemyRandomiser = Random.Range(1, 30);

        spawnPoint = Random.Range(0, 51);

        spawnPointVec = new Vector3(respawns[spawnPoint].transform.position.x, respawns[spawnPoint].transform.position.y, respawns[spawnPoint].transform.position.z);

        switch (gVar.enemyRandomiser)
        {
            case 1:
                enemyToSpawn = Instantiate(boss, spawnPointVec, Quaternion.identity);
                break;
            
            case int n when (n > 1 && n < 7):
                spawnPointVec = new Vector3(14, 6, 0);
                enemyToSpawn = Instantiate(shooter, spawnPointVec, Quaternion.identity);
                enemyToSpawn.transform.localScale = Vector3.one * Random.Range(0.25f, 0.4f);
                break;

            case int n when (n > 6):
                enemyToSpawn = Instantiate(enemy, spawnPointVec, Quaternion.identity);
                enemyToSpawn.transform.localScale = Vector3.one * Random.Range(0.4f, 0.65f);
                break;
            
            default:
                break;
        }

        gVar.spawnTimer = Random.Range(gVar.gSpawnTimer, gVar.gSpawnTimer - 2);

        StartCoroutine("Spawn");
    }
}
