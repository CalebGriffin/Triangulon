using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class BossBaby : MonoBehaviour
{
    public GameObject ship;
    public GameObject enemyOb;
    public GameObject particleSys;
    public SpriteRenderer enemySR;
    public PolygonCollider2D hitbox;
    public Vector3 axis = Vector3.back;

    public float disToShip;
    public float rotatingSpeed;

    void Awake()
    {
        ship = GameObject.Find("Ship");

        enemySR = enemyOb.GetComponent<SpriteRenderer>();
        hitbox = enemyOb.GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        disToShip = Vector3.Distance(ship.transform.position, enemyOb.transform.position);

        enemyOb.transform.Rotate(0f, 0f, 12f);

        if (disToShip <= 5)
        {
            gVar.bossMoveSpeed = 0.4f;
            enemyOb.transform.RotateAround(ship.transform.position, axis, rotatingSpeed * Time.deltaTime);
        }
        else
        {
            gVar.bossMoveSpeed = 6f;
        }

        float step = gVar.bossMoveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, ship.transform.position, step);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Explode();
        }
    }

    public void Explode()
    {
        enemySR.enabled = false;
        hitbox.enabled = false;

        particleSys.SetActive(true);

        StartCoroutine("WaitToDestroy");
    }

    IEnumerator WaitToDestroy()
    {
        gVar.score += gVar.level * 500;
        
        yield return new WaitForSeconds(1f);

        Destroy(enemyOb);
    }
}
