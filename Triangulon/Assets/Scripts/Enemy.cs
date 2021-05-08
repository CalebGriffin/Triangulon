using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Enemy : MonoBehaviour
{
    public GameObject ship;
    public GameObject enemyOb;
    public GameObject particleSys;
    public GameObject cameraOb;

    public ConstraintSource shipSource;

    public AimConstraint aimC;

    public SpriteRenderer enemySR;

    public PolygonCollider2D hitbox;

    public float disToShip;

    void Awake()
    {
        ship = GameObject.Find("Ship");
        cameraOb = GameObject.Find("Main Camera");

        aimC = GetComponent<AimConstraint>();

        shipSource.sourceTransform = ship.transform;
        shipSource.weight = 1;
        aimC.AddSource(shipSource);

        enemySR = enemyOb.GetComponent<SpriteRenderer>();
        hitbox = enemyOb.GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float step = gVar.enemyMoveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, ship.transform.position, step);

        disToShip = Vector3.Distance(ship.transform.position, enemyOb.transform.position);
        Debug.Log(disToShip.ToString());
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
        StartCoroutine(cameraOb.GetComponent<CameraShake>().Shake(0.2f, 0.1f));

        enemySR.enabled = false;
        hitbox.enabled = false;

        particleSys.SetActive(true);

        StartCoroutine("WaitToDestroy");
    }

    IEnumerator WaitToDestroy()
    {
        gVar.score += gVar.level * 100;
        
        yield return new WaitForSeconds(1f);

        Destroy(enemyOb);
    }
}
