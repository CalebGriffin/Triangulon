using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour
{
    public GameObject flames;

    public SpriteRenderer flamesSR;

    public float ranX;
    public float ranY;

    // Update is called once per frame
    void Update()
    {
        ranX = Random.Range(0.8f, 1.0f);
        ranY = Random.Range(2f, 3f);

        flames.transform.localScale = new Vector3(ranX, ranY, 1);

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