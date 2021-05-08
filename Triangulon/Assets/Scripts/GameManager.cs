﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject ship;
    public GameObject bullet;
    public GameObject particleSys;
    public GameObject cannon;
    public GameObject[] enemies;
    public GameObject[] powerups;
    public GameObject[] shooters;
    public GameObject[] bosses;
    public GameObject[] bossBabies;
    public GameObject[] enemyBullets;

    public Vector3 pos;

    public SpriteRenderer shipSR;
    public Animator animator;

    public bool waiting = false;
    public bool touchingBorder = false;

    public void Start()
    {
        gVar.lives = 3;

        Application.targetFrameRate = 60;

        animator = cannon.GetComponent<Animator>();
    }

    public void Update()
    {

        pos = new Vector3(ship.transform.position.x, (ship.transform.position.y + 100f), ship.transform.position.z);

        if (Input.GetKey(KeyCode.LeftArrow) && gVar.paused == false)
        {
            ship.transform.Rotate(0, 0, 4);
        }

        if (Input.GetKey(KeyCode.RightArrow) && gVar.paused == false)
        {
            ship.transform.Rotate(0, 0, -4);
        }

        if (Input.GetKeyDown("space") && waiting == false)
        {
            animator.SetTrigger("Fire");
            Instantiate(bullet, ship.transform.position + (transform.up * 1), ship.transform.rotation * Quaternion.Euler (0f, 0f, 0f));
            StartCoroutine("Wait");
        }

        if (Input.GetKey(KeyCode.UpArrow) && gVar.paused == false)
        {
            ship.transform.position += transform.up * 0.08f;

            gVar.moving = true;
        }

        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            gVar.moving = false;
        }
    }

    IEnumerator Wait()
    {
        waiting = true;
        yield return new WaitForSeconds(0.3f);
        waiting = false;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }

        if (collision.gameObject.tag == "Enemy")
        {
            Hit();
            collision.gameObject.GetComponent<Enemy>().Explode();
        }
        else if (collision.gameObject.tag == "Boss")
        {
            Hit();
            collision.gameObject.GetComponent<Boss>().Explode();
        }
        else if (collision.gameObject.tag == "BossBaby")
        {
            Hit();
            collision.gameObject.GetComponent<BossBaby>().Explode();
        }
        else if (collision.gameObject.tag == "Shooter")
        {
            Hit();
            collision.gameObject.GetComponent<Shooter>().Explode();
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyBullet")
        {
            Hit();
            collision.gameObject.GetComponent<EnemyBullet>().Explode();
        }
        if (collision.gameObject.tag == "Top" && touchingBorder == false)
        {
            touchingBorder = true;
            ship.transform.position = new Vector3(ship.transform.position.x, -10f, ship.transform.position.z);
        }
        if (collision.gameObject.tag == "Bottom" && touchingBorder == false)
        {
            touchingBorder = true;
            ship.transform.position = new Vector3(ship.transform.position.x, 10f, ship.transform.position.z);
        }
        if (collision.gameObject.tag == "Right" && touchingBorder == false)
        {
            touchingBorder = true;
            ship.transform.position = new Vector3(-13.5f, ship.transform.position.y, ship.transform.position.z);
        }
        if (collision.gameObject.tag == "Left" && touchingBorder == false)
        {
            touchingBorder = true;
            ship.transform.position = new Vector3(13.5f, ship.transform.position.y, ship.transform.position.z);
        }
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        touchingBorder = false;
    }

    public void Hit()
    {
        gVar.lives -= 1;

        StartCoroutine("Explode");
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(0.6f);
        Time.timeScale = 0;
    }

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(0f);

        shipSR.enabled = false;

        particleSys.SetActive(true);

        gVar.paused = true;
        waiting = true;
        gVar.moving = false;

        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        powerups = GameObject.FindGameObjectsWithTag("PowerUp");
        shooters = GameObject.FindGameObjectsWithTag("Shooter");
        bosses = GameObject.FindGameObjectsWithTag("Boss");
        bossBabies = GameObject.FindGameObjectsWithTag("BossBaby");
        enemyBullets = GameObject.FindGameObjectsWithTag("EnemyBullet");
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<Enemy>().Explode();
        }
        foreach (GameObject enemyBullet in enemyBullets)
        {
            enemyBullet.GetComponent<EnemyBullet>().Explode();
        }
        foreach (GameObject powerup in powerups)
        {
            Destroy(powerup);
        }
        foreach (GameObject shooter in shooters)
        {
            shooter.GetComponent<Shooter>().Explode();
        }
        foreach (GameObject boss in bosses)
        {
            boss.GetComponent<Boss>().calledByShip = true;
            boss.GetComponent<Boss>().Explode();
        }
        foreach (GameObject bossBaby in bossBabies)
        {
            bossBaby.GetComponent<BossBaby>().Explode();
        }

        if (gVar.lives == 0)
        {
            StartCoroutine("Death");
        }
        else
        {
            StartCoroutine("Restart");
        }
    }

    IEnumerator Restart()
    {
        yield return new WaitForSeconds(0.6f);

        ship.transform.position = new Vector3(0, 0, 0);
        ship.transform.rotation = Quaternion.Euler(new Vector3(0,0,0));

        shipSR.enabled = true;

        particleSys.SetActive(false);

        gVar.paused = false;
        waiting = false;
    }
}
