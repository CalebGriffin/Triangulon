using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Shooter : MonoBehaviour
{
    public GameObject ship;
    public GameObject enemyOb;
    public GameObject particleSys;
    public GameObject shooterBullet;
    public GameObject cameraOb;

    public ConstraintSource shipSource;

    public AimConstraint aimC;

    public SpriteRenderer enemySR;

    public PolygonCollider2D hitbox;

    public Vector3 targetLoc;

    public bool alive;

    public float disToShip;
    
    public Animator animator;

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

        targetLoc = new Vector3(-14, 6, 0);

        alive = true;

        InvokeRepeating("Shoot", 2f, gVar.shootTimer);
    }

    // Update is called once per frame
    void Update()
    {
        float step = gVar.shooterMoveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetLoc, step);

        disToShip = Vector3.Distance(transform.position, ship.transform.position);
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

        alive = false;

        enemySR.enabled = false;
        hitbox.enabled = false;

        particleSys.SetActive(true);

        if(gVar.calledByShip == false)
        {
            gVar.score += gVar.level * 50;
        }

        StartCoroutine("WaitToDestroy");
    }

    IEnumerator WaitToDestroy()
    {
        yield return new WaitForSeconds(1f);

        Destroy(enemyOb);
    }

    void OnBecameInvisible()
    {
        if (gVar.calledByShip == false)
        {
            gVar.score += gVar.level * 10;
        }

        alive = false;

        StartCoroutine("WaitToDestroy");
    }

    public void Shoot()
    {
        if (alive == true)
        {
            animator.SetTrigger("ShooterFire");

            Instantiate(shooterBullet, enemyOb.transform.position + (transform.up * 1), enemyOb.transform.rotation * Quaternion.Euler (0f, 0f, 0f));
        }
    }
}
