using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TImer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Runs the LevelUp function every 15 seconds after the game has started
        InvokeRepeating("LevelUp", 0f, 15f);
    }

    public void LevelUp()
    {
        // Increases the level which affects how many points the player gets for killing enemies
        gVar.level += 1;

        switch (gVar.lives)
        {
            // If the level is equal to 2,5,8 or 11 then it will make the ships spawn faster
            case 2: case 5: case 8: case 11: 
                gVar.gSpawnTimer += -1;
                break;
            // If the level is 3,6,9 or 12 then it will make the enemies move faster
            case 3: case 6: case 9: case 12: 
                gVar.enemyMoveSpeed += 0.5f;
                break;

            // If the level is 4,7, 10 or 13 then it will make the shooter enemies shoot more often
            case 4: case 7: case 10: case 13: 
                gVar.shootTimer += -0.5f;
                break;

            // Does nothing as level 13 is the highest level
            default:
                break;
        }
    }
}
