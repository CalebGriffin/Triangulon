using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Enemy : MonoBehaviour
{
    // GameObjects so that it can control and interact with different objects in the game like the ship and the particle system
    public GameObject ship;
    public GameObject enemyOb;
    public GameObject particleSys;
    public GameObject cameraOb;
    public GameObject enemyFireOb;

    // Variables to control the aim constraint so that the enemy is constantly looking at the ship
    public ConstraintSource shipSource;
    public AimConstraint aimC;

    // Sprite renderers so that this script can make the enemy invisible when it is destroyed
    public SpriteRenderer enemySR;
    public SpriteRenderer enemyFireSR;

    // The enemy ship's hitbox
    public PolygonCollider2D hitbox;

    // Float to store the distance to the ship
    public float disToShip;

    void Awake()
    {
        // Finds the ship and the camera
        ship = GameObject.Find("Ship");
        cameraOb = GameObject.Find("Main Camera");

        // Sets up the aim constraint on the ship
        aimC = GetComponent<AimConstraint>();

        shipSource.sourceTransform = ship.transform;
        shipSource.weight = 1;
        aimC.AddSource(shipSource);

        // Sets the different components of the enemy ship
        enemySR = enemyOb.GetComponent<SpriteRenderer>();
        hitbox = enemyOb.GetComponent<PolygonCollider2D>();
        enemyFireSR = enemyFireOb.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Sets the enemy's speed and moves towards the enemy at a constant speed
        float step = gVar.enemyMoveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, ship.transform.position, step);

        // Sets the distance to the ship
        disToShip = Vector3.Distance(ship.transform.position, enemyOb.transform.position);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // If it is hit by a bullet then it will explode
        if (collision.gameObject.tag == "Bullet")
        {
            Explode();
        }
    }

    public void Explode()
    {
        // Check the distance to the ship and then change the camera shake magnitude based on how close it is
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

        // Makes the enemy invisible and starts the particle system
        enemySR.enabled = false;
        hitbox.enabled = false;
        enemyFireSR.enabled = false;

        particleSys.SetActive(true);

        // Calls the coroutine to destroy the enemy
        StartCoroutine("WaitToDestroy");
    }

    // If the player shot the enemy then give the points based on their level
    // Then destroy the enemy ship
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
