using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyList : MonoBehaviour
{

    //Example of creating enemies
    //To get attributes of an enemy type in any script, call functions like esimerkki1.GetStr(int enemyLevel);
    //Add a new line for every enemy type

    static EnemyAttributes exampleEnemy = new EnemyAttributes(1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f);

    static EnemyAttributes exampleGolem = new EnemyAttributes(1.3f, 0.8f, 1.2f, 1.1f, 1.2f, 0.7f, 1.3f, 0.4f, 1.2f, 1.1f);

    public static EnemyAttributes golemAttributes = new EnemyAttributes(1.3f, 10f, 0.7f, 3.5f, 1.2f, 1f, 1f, 1f, 1f, 1f);

    public static EnemyAttributes skullAttributes = new EnemyAttributes(0.8f, 3f, 1f, 2f, 1f, 1f, 1f, 1f, 1f, 1.2f);

    public static EnemyAttributes leshenAttributes = new EnemyAttributes(1.2f, 6f, 1f, 3f, 1f, 1f, 1f, 1.2f, 1f, 1f);

    public static EnemyAttributes trainingGuyAttributes = new EnemyAttributes(1f, 5f, 1f, 2.5f, 1f, 1f, 1f, 1f, 1f, 1f);
}
