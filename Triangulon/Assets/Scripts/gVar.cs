using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gVar
{
    public static bool moving;

    public static int lives;

    public static int score;

    public static int level = 0;

    public static bool paused;

    public static bool flames;

    public static float enemyMoveSpeed = 2f;

    public static float shooterMoveSpeed = 2f;

    public static float bossMoveSpeed = 6f;

    public static float enemyBulletSpeed = 6f;

    public static float shootTimer = 3f;

    public static float spawnTimer;

    // Temporarily adjusted for testing
    public static int gSpawnTimer = 4;

    public static int enemyRandomiser;
}
