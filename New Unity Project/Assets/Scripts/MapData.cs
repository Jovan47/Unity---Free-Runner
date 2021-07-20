using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //moze da se sacuva u fajl
public class MapData
{
    public int level;
    public int health;
    public int coins;
    public float[] position;

    public MapData(Map map)
    {
        level = map.level;
        health = map.health;

        position = new float[3];
        position[0] = map.transform.position.x;
        position[1] = map.transform.position.y;
        position[2] = map.transform.position.z;
    }



}
