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
    public GameObject enemyFireOb;

    public ConstraintSource shipSource;

    public AimConstraint aimC;

    public SpriteRenderer enemySR;
    public SpriteRenderer enemyFireSR;

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
        enemyFireSR = enemyFireOb.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float step = gVar.enemyMoveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, ship.transform.position, step);

        disToShip = Vector3.Distance(ship.transform.position, enemyOb.transform.position);
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
        switch (disToShip)
        {
            case float n when (n <= 3):
                gVar.shakeMagnitude = 0.5f;
                break;
            
            case float n when (n > 3 && n <= 7):
                gVar.shakeMagnitude = 0.4f;
                break;
            
            case float n when (n > 7 && n <= 11):
                gVar.shakeMagnitude = 0.3f;
                break;
            
            case float n when (n > 11 && n <= 14):
                gVar.shakeMagnitude = 0.2f;
                break;
            
            case float n when (n > 14):
                gVar.shakeMagnitude = 0.1f;
                break;
        }
        StartCoroutine(cameraOb.GetComponent<CameraShake>().Shake(0.2f, gVar.shakeMagnitude));

        enemySR.enabled = false;
        hitbox.enabled = false;
        enemyFireSR.enabled = false;

        particleSys.SetActive(true);

        StartCoroutine("WaitToDestroy");
    }

    IEnumerator WaitToDestroy()
    {
        if(gVar.calledByShip == false)
        {
            gVar.score += gVar.level * 10;
        }
        
        yield return new WaitForSeconds(1f);

        Destroy(enemyOb);
    }
}
