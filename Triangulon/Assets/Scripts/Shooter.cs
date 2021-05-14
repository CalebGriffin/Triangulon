using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class Shooter : MonoBehaviour
{
    // GameObjects to get the information from different objects in the game like the camera and the particle system
    public GameObject ship;
    public GameObject enemyOb;
    public GameObject particleSys;
    public GameObject shooterBullet;
    public GameObject cameraOb;

    // Different components which are used to control the aim constraint that makes the shooter look at the ship
    public ConstraintSource shipSource;
    public AimConstraint aimC;

    // SpriteRenderer and hitbox so that the script can make the shooter invisible
    public SpriteRenderer enemySR;
    public PolygonCollider2D hitbox;

    // Vector3 to set the movement target of the shooter
    public Vector3 targetLoc;

    // A boolean to check if the shooter is still alive
    public bool alive;

    // A float to check the distance to the ship
    public float disToShip;

    // Controls the animations of the ship
    public Animator animator;

    void Awake()
    {
        // Finds the ship object and the camera object
        ship = GameObject.Find("Ship");
        cameraOb = GameObject.Find("Main Camera");

        // Sets up the aim constraint so that the shooter is always facing the ship
        aimC = GetComponent<AimConstraint>();
        shipSource.sourceTransform = ship.transform;
        shipSource.weight = 1;
        aimC.AddSource(shipSource);

        // Sets up some of the shooter components so that the script can make the shooter invisible
        enemySR = enemyOb.GetComponent<SpriteRenderer>();
        hitbox = enemyOb.GetComponent<PolygonCollider2D>();

        // Finds the location for the shooter to moves towards
        targetLoc = new Vector3(-14, 6, 0);

        alive = true;

        // Repeatedly calls a function based on the global variable so that it shoots more often the longer the player survives
        InvokeRepeating("Shoot", 2f, gVar.shootTimer);
    }

    // Update is called once per frame
    void Update()
    {
        // Calculates the shooter's move speed and moves it towards the target position
        float step = gVar.shooterMoveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetLoc, step);

        // Gets the distance to the ship
        disToShip = Vector3.Distance(transform.position, ship.transform.position);

        // If the shooter is close to it's destination, destroy the shooter
        if ((transform.position - targetLoc).magnitude < 1)
        {
            if (gVar.calledByShip == false)
            {
                gVar.score += gVar.level * 10;
            }

            alive = false;

            StartCoroutine("WaitToDestroy");
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // If the shooter collides with the bullet then explode the shooter
        if (collision.gameObject.tag == "Bullet")
        {
            Explode();
        }
    }

    public void Explode()
    {
        // Sets the magnitude of the camera shake based on the shooter's distance from the player
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

        // Makes the ship invisible and starts the particle system
        enemySR.enabled = false;
        hitbox.enabled = false;

        particleSys.SetActive(true);

        // If the shooter has been shot then give the player points
        if(gVar.calledByShip == false)
        {
            gVar.score += gVar.level * 50;
        }

        // Start the coroutine which destroys the shooter
        StartCoroutine("WaitToDestroy");
    }

    // Wait and destroy the shooter
    IEnumerator WaitToDestroy()
    {
        yield return new WaitForSeconds(1f);

        Destroy(enemyOb);
    }

    // This code was being used to destroy the ship once it became invisible but it caused problems so I replaced it with another method
    /*void OnBecameInvisible()
    {
        if (gVar.calledByShip == false)
        {
            gVar.score += gVar.level * 10;
        }

        alive = false;

        StartCoroutine("WaitToDestroy");
    }*/

    // Spawns a bullet if the shooter object is alive and makes a shooting animation
    public void Shoot()
    {
        if (alive == true)
        {
            animator.SetTrigger("ShooterFire");

            // Spawn the bullet in the direction the shooter is facing, which will be towards the player
            Instantiate(shooterBullet, enemyOb.transform.position + (transform.up * 1), enemyOb.transform.rotation * Quaternion.Euler (0f, 0f, 0f));
        }
    }
}
