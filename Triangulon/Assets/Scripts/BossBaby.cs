using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class BossBaby : MonoBehaviour
{
    // Gameobjects so that the enemy ship can interact with different elements in the game
    public GameObject ship;
    public GameObject enemyOb;
    public GameObject particleSys;
    public GameObject cameraOb;

    // This script needs to hide the enemy ship by disabling the hitbox and the sprite renderer
    public SpriteRenderer enemySR;
    public PolygonCollider2D hitbox;
    public Vector3 axis = Vector3.back;

    // floats for calculating the distance to the ship and to control how fast the ship rotates around the player
    public float disToShip;
    public float rotatingSpeed;

    // Finds the ship and the camera in the scene
    // Sets some of the variables declared earlier
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
        // Finds the distance to the ship
        disToShip = Vector3.Distance(ship.transform.position, enemyOb.transform.position);

        // Rotates the ship so that it appears to be spinning
        enemyOb.transform.Rotate(0f, 0f, 12f);

        // Slows the enemy down if it is near to the player and starts to rotate around the ship
        if (disToShip <= 5)
        {
            gVar.bossMoveSpeed = 0.4f;
            enemyOb.transform.RotateAround(ship.transform.position, axis, rotatingSpeed * Time.deltaTime);
        }
        else
        {
            gVar.bossMoveSpeed = 6f;
        }

        // Changes some of the variables that control how fast the ship moves
        float step = gVar.bossMoveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, ship.transform.position, step);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // Explodes the enemy ship if it collides with the bullet
        if (collision.gameObject.tag == "Bullet")
        {
            Explode();
        }
    }

    public void Explode()
    {
        // Shakes the camera by calling another script
        StartCoroutine(cameraOb.GetComponent<CameraShake>().Shake(0.2f, 0.4f));

        // Makes the ship invisible
        enemySR.enabled = false;
        hitbox.enabled = false;

        // Starts the particle system
        particleSys.SetActive(true);

        StartCoroutine("WaitToDestroy");
    }

    // If the player shot the ship then they will get 25 points for every level that they have achieved and will then destroy the ship object
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
