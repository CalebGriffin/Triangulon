using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Boss : MonoBehaviour
{
    public GameObject ship;
    public GameObject enemyOb;
    public GameObject particleSys;
    public GameObject bossBabyOb;
    public GameObject cameraOb;

    public SpriteRenderer enemySR;

    public PolygonCollider2D hitbox;

    public Vector3 axis = Vector3.back;

    public float disToShip;
    public float rotatingSpeed = 50f;

    void Awake()
    {
        ship = GameObject.Find("Ship");
        cameraOb = GameObject.Find("Main Camera");

        enemySR = enemyOb.GetComponent<SpriteRenderer>();
        hitbox = enemyOb.GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        disToShip = Vector3.Distance(ship.transform.position, enemyOb.transform.position);

        enemyOb.transform.Rotate(0f, 0f, 12f);

        if (disToShip <= 6)
        {
            gVar.bossMoveSpeed = 0.3f;
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
            collision.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
            Explode();
        }
    }

    public void Explode()
    {
        StartCoroutine(cameraOb.GetComponent<CameraShake>().Shake(0.2f, 0.4f));

        enemySR.enabled = false;
        hitbox.enabled = false;

        particleSys.SetActive(true);

        if (gVar.calledByShip == false)
        {
            bossBabyOb = (GameObject)Instantiate(bossBabyOb, enemyOb.transform.position, enemyOb.transform.rotation * Quaternion.Euler (0f, 0f, 0f));
            bossBabyOb.GetComponent<BossBaby>().rotatingSpeed = 75f;

            bossBabyOb = (GameObject)Instantiate(bossBabyOb, enemyOb.transform.position, enemyOb.transform.rotation * Quaternion.Euler (0f, 0f, 0f));
            bossBabyOb.GetComponent<BossBaby>().rotatingSpeed = -75f;
        }

        StartCoroutine("WaitToDestroy");
    }

    IEnumerator WaitToDestroy()
    {
        if(gVar.calledByShip == false)
        {
            gVar.score += gVar.level * 25;
        }
        
        yield return new WaitForSeconds(1f);

        Destroy(enemyOb);
    }
}
