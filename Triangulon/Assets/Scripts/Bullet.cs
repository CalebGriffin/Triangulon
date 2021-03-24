using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject bullet;
    public GameObject target;

    public Vector3 targetVec;

    public float speed;

    void Awake()
    {
        targetVec = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z);

        speed = 25f;
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        bullet.transform.position = Vector3.MoveTowards(bullet.transform.position, targetVec, step);
    }

    void OnBecameInvisible()
    {
        StartCoroutine("Wait");
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);

        Destroy(bullet);
    }
}
