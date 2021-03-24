using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public GameObject ship;
    public GameObject bullet;

    public Vector3 pos;

    public bool waiting = false;

    public void Start()
    {
        gVar.lives = 3;
    }

    public void Update()
    {
        pos = new Vector3(ship.transform.position.x, (ship.transform.position.y + 1f), ship.transform.position.z);

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            ship.transform.Rotate(0, 0, 4);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            ship.transform.Rotate(0, 0, -4);
        }

        if (Input.GetKeyDown("space") && waiting == false)
        {
            Instantiate(bullet, ship.transform.position + (transform.up * 1), ship.transform.rotation * Quaternion.Euler (0f, 0f, 0f));
            StartCoroutine("wait");
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            ship.transform.position += transform.up * 0.08f;

            gVar.moving = true;
        }
        else
        {
            gVar.moving = false;
        }
    }

    IEnumerator wait()
    {
        waiting = true;
        yield return new WaitForSeconds(0.3f);
        waiting = false;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Hit();
        }
    }

    public void Hit()
    {
        gVar.lives -= 1;
        if (gVar.lives == 0)
        {
            Time.timeScale = 0;
            waiting = true;
        }
    }
}
