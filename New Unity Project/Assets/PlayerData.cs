using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //moze da se sacuva u fajl
public class PlayerData 
{
     public int level;
     public int health;
     public int coins;
    public float[] position;

    public PlayerData(Player player)
    {
        level = player.level;
        health = player.health;

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
    }



}
