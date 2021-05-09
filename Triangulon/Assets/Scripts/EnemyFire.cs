using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    public GameObject enemyFlames;

    public float ranX;
    public float ranY;

    // Update is called once per frame
    void Update()
    {
        ranX = Random.Range(0.8f, 1.0f);
        ranY = Random.Range(2f, 3f);

        enemyFlames.transform.localScale = new Vector3(ranX, ranY, 1);
    }
}