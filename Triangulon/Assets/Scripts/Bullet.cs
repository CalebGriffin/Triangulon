using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject bullet;
    public GameObject target;

    public Vector3 targetVec;

    public float speed = 5f;

    void Awake()
    {
        targetVec = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        bullet.transform.position = Vector3.MoveTowards(bullet.transform.position, targetVec, step);
    }
}
