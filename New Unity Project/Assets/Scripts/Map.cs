using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public int level = 1;
    public int health = 10;




    public void SaveMap()
    {
        SaveSystem.SaveMap(this);
    }
    public void LoadMap()
    {
        MapData data = SaveSystem.LoadMap();

        this.level = data.level;
        this.health = data.health;

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;

    }

    #region UI Methods

    public void ChangeLevel(int amount)
    {
        level += amount;
    }

    public void ChangeHealth(int amount)
    {
        health += amount;
    }

    #endregion
}
