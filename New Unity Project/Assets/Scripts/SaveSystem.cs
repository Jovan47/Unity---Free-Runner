using System.IO; //korsitiomo fajlove
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem 
{
  public static void SavePlayer(Player player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path= Path.Combine(Application.persistentDataPath, "player.fun");
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        string path = Path.Combine(Application.persistentDataPath, "player.fun");
        if (File.Exists(path))
        {
           BinaryFormatter formatter = new BinaryFormatter();
           FileStream stream = new FileStream(path, FileMode.Open);

           PlayerData data= formatter.Deserialize(stream) as PlayerData;
           stream.Close();
           return data;
        }
        else 
        { 
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

    public static void SaveEnemy(Enemy enemy)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Path.Combine(Application.persistentDataPath, "enemy.fun");
        FileStream stream = new FileStream(path, FileMode.Create);

        EnemyData data = new EnemyData(enemy);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static EnemyData LoadEnemy()
    {
        string path = Path.Combine(Application.persistentDataPath, "enemy.fun");
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            EnemyData data = formatter.Deserialize(stream) as EnemyData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }


    public static void SaveMap(Map map)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Path.Combine(Application.persistentDataPath, "map.fun");
        FileStream stream = new FileStream(path, FileMode.Create);

        MapData data = new MapData(map);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static MapData LoadMap()
    {
        string path = Path.Combine(Application.persistentDataPath, "map.fun");
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            MapData data = formatter.Deserialize(stream) as MapData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

}
