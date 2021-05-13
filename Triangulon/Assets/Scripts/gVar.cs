using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gVar
{
    public static bool moving;

    public static int lives;

    public static int score;

    public static int highScore;

    public static int level = 1;

    public static bool paused;

    public static bool flames;

    public static bool calledByShip = false;

    public static float enemyMoveSpeed = 2f;

    public static float shooterMoveSpeed = 2f;

    public static float bossMoveSpeed = 6f;

    public static float enemyBulletSpeed = 6f;

    public static float shootTimer = 3f;

    public static float spawnTimer;

    public static float shakeMagnitude;

    // Temporarily adjusted for testing
    public static int gSpawnTimer = 4;

    public static int enemyRandomiser;
}
