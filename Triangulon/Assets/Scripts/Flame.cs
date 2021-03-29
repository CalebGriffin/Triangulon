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
        ranX = Random.Range(0.3f, 0.4f);
        ranY = Random.Range(0.2f, 0.4f);

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
