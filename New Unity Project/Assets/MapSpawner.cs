using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSpawner : MonoBehaviour
{

    [SerializeField] private Vector3 startPosition = new Vector3(0, 0, 0);
    public GameObject TerrainPrefab;


    private GameObject[,] mapa =new GameObject[10,10];
    private List<GameObject> listMapa;

    void Start()
    {
        Vector3 position = new Vector3(0, 0, 0);
        GameObject tile = null;
        
        for(int i=0; i<10; i++)
        {
            position.x=4*i;
            for (int j=0; j<10; j++)
            {
                position.z = 4 * j;

                mapa[i,j]= Instantiate(TerrainPrefab, position, Quaternion.identity);
                mapa[i, j].gameObject.tag = "tile";
            }
        }
    }

    void Update()
    {
        
    }
}
