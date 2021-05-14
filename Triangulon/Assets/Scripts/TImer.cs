using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TImer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("LevelUp", 0f, 15f);
    }

    public void LevelUp()
    {
        gVar.level += 1;

        switch (gVar.lives)
        {
            case 2: case 5: case 8: case 11: 
                gVar.gSpawnTimer += -1;
                break;
            
            case 3: case 6: case 9: case 12: 
                gVar.enemyMoveSpeed += 0.5f;
                break;

            case 4: case 7: case 10: case 13: 
                gVar.shootTimer += -0.5f;
                break;

            default:
                break;
        }
    }
}
