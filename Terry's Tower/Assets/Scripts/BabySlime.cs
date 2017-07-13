using UnityEngine;
using System.Collections;

public class BabySlime : EnemyBase {

    BabySlime()
    {
        health = 10;
        currentHealth = health;
        def = 0;
        atk = 2;
        xp = 3;
        powerAtkChance = 5;
        powerAtkMult = 1.5f;
        defChance = 15;
        defMult = 1.5f;
        healChance = 10;
        healPerc = .10;
        enemyName = Enemy.BabySlime;
    }
}
