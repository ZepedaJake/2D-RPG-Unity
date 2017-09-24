using UnityEngine;
using System.Collections;

public class EnemyBase : MonoBehaviour {

    public int health;
    public int currentHealth;
    public int atk;
    public int def;
    public int xp;

    public int powerAtkChance;
    public int powerAtkCooldown = 3;
    public int currentPowerAtkCooldown;
    public float powerAtkMult;

    public int defChance;
    public float defMult;

    public int healChance;
    public int healCooldown = 3;
    public int currentHealCooldown;
    public double healPerc;

    //public Enemy enemyName;
    public string enemyName;
    public bool alive = true;
    public string heldItem;

    

}
/*public enum Enemy
{
    BabySlime,
    Bat,
    Slime,
    Knight,
    BigKnight,
    Golem,
    Male,
    Female,
    CorruptedMale,
    CorruptedFemale,
    Statue,
    Boss1
}*/
