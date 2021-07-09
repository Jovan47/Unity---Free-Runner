using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //moze da se sacuva u fajl
public class EnemyData
{
    public int level;
    public int health;
    public int coins;
    public float[] position;

    public EnemyData(Enemy enemy)
    {
        level = enemy.level;
        health = enemy.health;

        position = new float[3];
        position[0] = enemy.transform.position.x;
        position[1] = enemy.transform.position.y;
        position[2] = enemy.transform.position.z;
    }



}
