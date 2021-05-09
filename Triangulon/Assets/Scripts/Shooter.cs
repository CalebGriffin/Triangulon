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

    public ConstraintSource shipSource;

    public AimConstraint aimC;

    public SpriteRenderer enemySR;

    public PolygonCollider2D hitbox;

    public Vector3 targetLoc;

    public bool alive;
    
    public Animator animator;

    void Awake()
    {
        ship = GameObject.Find("Ship");

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
        alive = false;

        enemySR.enabled = false;
        hitbox.enabled = false;

        particleSys.SetActive(true);

        if(gVar.calledByShip == false)
        {
            gVar.score += gVar.level * 500;
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
            gVar.score += gVar.level * 100;
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
