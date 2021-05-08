using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public GameObject bullet;
    public GameObject target;
    //public GameObject particleSys;

    public Vector3 targetVec;

    public SpriteRenderer bulletSR;

    public PolygonCollider2D hitbox;

    void Awake()
    {
        targetVec = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        float step = gVar.enemyBulletSpeed * Time.deltaTime;
        bullet.transform.position = Vector3.MoveTowards(bullet.transform.position, targetVec, step);
    }

    void OnBecameInvisible()
    {
        StartCoroutine("WaitToDestroy");
    }

    public void Explode()
    {
        bulletSR.enabled = false;
        hitbox.enabled = false;

        //particleSys.SetActive(true);

        StartCoroutine("WaitToDestroy");
    }

    IEnumerator WaitToDestroy()
    {
        yield return new WaitForSeconds(1f);

        Destroy(bullet);
    }
}
