using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// This script controls everything to do with the ship object including movement and animations and many other things, it also controls some extra elements that I added last minute and didn't want to make a new script to control
public class GameManager : MonoBehaviour
{
    // GameObjects so that the script can access lots of information and interact with all of the objects in the game
    public GameObject ship;
    public GameObject bullet;
    public GameObject particleSys;
    public GameObject cannon;
    public GameObject cameraOb;
    public GameObject fuelSpawner;
    public GameObject currentFuel;
    public GameObject deathCanvas;
    public GameObject[] enemies;
    public GameObject[] powerups;
    public GameObject[] shooters;
    public GameObject[] bosses;
    public GameObject[] bossBabies;
    public GameObject[] enemyBullets;

    // Vector3 to control where the bullets go
    public Vector3 pos;

    // Controls the different components of the ship to make it invisible when it is destroyed
    public SpriteRenderer shipSR;
    public SpriteRenderer cannonSR;

    // Controls the cannon animations
    public Animator cannonAnimator;

    // Controls the fuel system
    public Slider fuelSlider;

    // Boolean variables to control different elements of the game like checking if the game is paused
    public bool waiting = false;
    public bool touchingBorder = false;
    public bool fuelEmpty = false;

    // Controls the different text elements of the UI so that it can be updated with the score and lives
    public Text scoreText;
    public Text livesText;
    public Text menuScoreText;
    public Text menuHighScoreText;

    public void Start()
    {
        // Sets the global variables ready to start the game
        gVar.lives = 3;
        gVar.score = 0;
        gVar.paused = false;

        // So that the game runs at the correct speed
        Time.timeScale = 1;

        // Makes sure that Update() runs the right amount of times per second
        Application.targetFrameRate = 60;

        // So that the script can control the cannon
        cannonAnimator = cannon.GetComponent<Animator>();
        cannonSR = cannon.GetComponent<SpriteRenderer>();

        // Gets the camera object
        cameraOb = GameObject.Find("Main Camera");

        // This stops the player from getting points when they die
        gVar.calledByShip = false;

        // Gives the player full fuel when the game starts
        fuelSlider.value = 100f;

        // Updates the highscore if it exists
        if (PlayerPrefs.HasKey("highScore"))
        {
            gVar.highScore = PlayerPrefs.GetInt("highScore");
            menuHighScoreText.text = "High Score: " + gVar.highScore.ToString();
        }

        // Controls the screen that appears at the end of the game
        deathCanvas.SetActive(false);
    }

    public void Update()
    {
        // Sets the target position of the bullet so that it shoots straight
        pos = new Vector3(ship.transform.position.x, (ship.transform.position.y + 100f), ship.transform.position.z);

        // Decreases the fuel by a certain amount every second
        fuelSlider.value -= 7.5f * Time.deltaTime;

        // Updates the UI to reflect the score and the lives
        scoreText.text = "Score: " + gVar.score.ToString();
        livesText.text = "Lives: " + gVar.lives.ToString();
        menuScoreText.text = "Score: " + gVar.score.ToString();

        // Controls the player's input and movement
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
            cannonAnimator.SetTrigger("Fire");
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

        // If the player runs out of fuel then they lose a life
        if (fuelSlider.value == 0 && gVar.paused == false)
        {
            Hit();
        }
        
        // Sends the player back to the menu if they press escape
        if (Input.GetKey("escape"))
        {
            SceneManager.LoadScene("MenuScene");
        }
    }

    // Forces the player to wait so that they cannot spam the shoot button
    IEnumerator Wait()
    {
        waiting = true;
        yield return new WaitForSeconds(0.3f);
        waiting = false;
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        // Ignore collisions with bullets
        if (collision.gameObject.tag == "Bullet")
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }

        // Lose a life if the ship collides with an enemy
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
        // Lose a life if the ship is hit by a bullet
        if (collision.gameObject.tag == "EnemyBullet")
        {
            Hit();
            collision.gameObject.GetComponent<EnemyBullet>().Explode();
        }

        // Fills up the fuel bar when colliding with a fuel object
        if (collision.gameObject.tag == "Fuel")
        {
            fuelSlider.value = 100f;
            collision.gameObject.GetComponent<Animator>().SetTrigger("Collected");
            Destroy(collision.gameObject);
            //respawn fuel
            fuelSpawner.GetComponent<FuelSpawner>().Respawn();
        }

        // Teleports the ship if they touch the edge of the screen
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
        // Continues to ignore the bullet collisions
        if (collision.gameObject.tag == "Bullet")
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        // This stops the ship from constantly teleporting between the edges
        touchingBorder = false;
    }

    public void Hit()
    {
        // Pauses the game to show the death animation and decreases the player's lives
        gVar.paused = true;
        gVar.lives -= 1;
        gVar.calledByShip = true;

        StartCoroutine("Explode");
    }

    IEnumerator Death()
    {
        // Waits for the death animation and pauses the game and displays the game over screen
        yield return new WaitForSeconds(0.6f);
        Time.timeScale = 0;

        if (gVar.score > gVar.highScore)
        {
            gVar.highScore = gVar.score;
            PlayerPrefs.SetInt("highScore", gVar.highScore);

            menuHighScoreText.text = "High Score: " + gVar.highScore;
        }

        deathCanvas.SetActive(true);
    }

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(0f);

        // Shakes the camera the maximum amount
        StartCoroutine(cameraOb.GetComponent<CameraShake>().Shake(0.2f, 0.5f));

        // Makes the ship invisible and starts the particle system
        shipSR.enabled = false;
        cannonSR.enabled = false;
        particleSys.SetActive(true);

        // Pauses the game and sets the boolean which control the input so that the player cannot move when they are dead
        gVar.paused = true;
        waiting = true;
        gVar.moving = false;
        gVar.calledByShip = true;

        // Finds all of the objects in the game and explodes them
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        powerups = GameObject.FindGameObjectsWithTag("PowerUp");
        shooters = GameObject.FindGameObjectsWithTag("Shooter");
        bosses = GameObject.FindGameObjectsWithTag("Boss");
        bossBabies = GameObject.FindGameObjectsWithTag("BossBaby");
        enemyBullets = GameObject.FindGameObjectsWithTag("EnemyBullet");
        currentFuel = GameObject.FindGameObjectWithTag("Fuel");
        Destroy(currentFuel);
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
            boss.GetComponent<Boss>().Explode();
        }
        foreach (GameObject bossBaby in bossBabies)
        {
            bossBaby.GetComponent<BossBaby>().Explode();
        }

        // If the player has run out of lives then it will show the game over screen but if not then the game will restart
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
        // Left in from testing
        Debug.Log("Restarted");

        // Waits to show the explosion animation
        yield return new WaitForSeconds(0.6f);

        // Resets the ship's position and rotation
        ship.transform.position = new Vector3(0, 0, 0);
        ship.transform.rotation = Quaternion.Euler(new Vector3(0,0,0));

        // Makes the ship visible again
        shipSR.enabled = true;
        cannonSR.enabled = true;

        // Resets the fuel slider and spawns a new fuel object
        fuelSlider.value = 100f;
        fuelSpawner.GetComponent<FuelSpawner>().Respawn();

        // Disables the particle system
        particleSys.SetActive(false);

        // Unpauses the game so that the player can move again
        gVar.paused = false;
        waiting = false;
        gVar.calledByShip = false;
    }

    // This is called by the button on the game over screen to send the player back to the main menu
    public void BackToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    // Called when the player goes back to the menu and it will reset all of the variables ready for the next game
    public void globalReset()
    {
        gVar.score = 0;
        gVar.lives = 3;
        gVar.level = 1;
    }
}