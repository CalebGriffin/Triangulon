using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    // GameObjects to control the bullet and the target
    public GameObject bullet;
    public GameObject target;

    // Vector3 to store the position of the target
    public Vector3 targetVec;

    // Variables to control different elements of the bullet
    public SpriteRenderer bulletSR;
    public PolygonCollider2D hitbox;

    void Awake()
    {
        // Sets the position of the target
        targetVec = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        // Calculates the speed and then moves the bullet towards the target so that it appears like the bullet is being fired forwards
        float step = gVar.enemyBulletSpeed * Time.deltaTime;
        bullet.transform.position = Vector3.MoveTowards(bullet.transform.position, targetVec, step);
    }

    void OnBecameInvisible()
    {
        // When the bullet disappears, start the coroutine to destroy it
        StartCoroutine("WaitToDestroy");
    }

    public void Explode()
    {
        // Make the bullet invisible and start the coroutine to destroy it
        bulletSR.enabled = false;
        hitbox.enabled = false;

        StartCoroutine("WaitToDestroy");
    }

    // Wait for 1 second and then destroy the bullet
    IEnumerator WaitToDestroy()
    {
        yield return new WaitForSeconds(1f);

        Destroy(bullet);
    }
}
