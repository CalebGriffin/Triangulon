using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// All of these variables are set to public and static so that they can be accessed at any time by any script and they control the entire game
public class gVar
{
    // Is the player moving?
    public static bool moving;

    // Player's lives
    public static int lives;

    // Player's score
    public static int score;

    // Player's highscore
    public static int highScore;

    // Player's level
    public static int level = 1;

    // Is the game paused?
    public static bool paused;

    // Should the flames be showing?
    public static bool flames;

    // Has the ship called to explode all of the enemies because it has been hit?
    public static bool calledByShip = false;

    // How fast should the enemies move towards the player
    public static float enemyMoveSpeed = 2f;

    // Controls how fast the shooter enemies move
    public static float shooterMoveSpeed = 2f;

    // Controls how fast the boss enemies move
    public static float bossMoveSpeed = 6f;

    // Controls how fast the enemy bullets are
    public static float enemyBulletSpeed = 6f;

    // Controls how often the shooter enemies shoot
    public static float shootTimer = 3f;

    // Used to randomly spawn the enemies between a certain time
    public static float spawnTimer;

    // Controls how much the camera shakes so that only the object being destroyed can edit this variable
    public static float shakeMagnitude;

    // Used as the maximum amount of time between enemies being spawned
    public static int gSpawnTimer = 4;

    // Controls which enemy should be spawned
    public static int enemyRandomiser;
}
