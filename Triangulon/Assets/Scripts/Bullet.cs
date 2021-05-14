using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // GameObjects to control the bullet and the target for the bullet
    public GameObject bullet;
    public GameObject target;

    // A Vector3 variable to get the position of the target
    public Vector3 targetVec;

    // Float variable to control the speed of the bullet
    public float speed;

    void Awake()
    {
        // Get's the position of the target
        targetVec = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z);

        // Sets the speed of the bullet
        speed = 25f;
    }

    // Update is called once per frame
    void Update()
    {
        // Sets how far the bullet should travel every frame based on it's speed
        float step = speed * Time.deltaTime;

        // Moves the bullet towards the target at the speed just calculated so that it appears to be shot forwards at a constant speed
        bullet.transform.position = Vector3.MoveTowards(bullet.transform.position, targetVec, step);
    }

    // When it is off the screen it will trigger the wait coroutine
    void OnBecameInvisible()
    {
        StartCoroutine("Wait");
    }

    // Waits for 1 second and then destroys the bullet object
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);

        Destroy(bullet);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // If the bullet collides with the player it will ignore the collision
        if (collision.gameObject.tag == "Player")
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }
}
