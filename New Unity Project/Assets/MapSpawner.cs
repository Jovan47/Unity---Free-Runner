using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSpawner : MonoBehaviour
{

    [SerializeField] private Vector3 startPosition = new Vector3(0, 0, 0);
    public GameObject TerrainPrefab;
  

    private GameObject[,] mapa =new GameObject[10,10];

    public Color druga;
    bool prekidac = true;
    List<GameObject> listaTile = new List<GameObject>();
    int indexListe = 0;
    void Start()
    {
        Vector3 position = new Vector3(0, 0, 0);
        
        for(int i=0; i<10; i++)
        {
            position.x=4*i;
            for (int j = 0; j < 10; j++)
            {
                position.z = 4 * j;

                mapa[i, j] = Instantiate(TerrainPrefab, position, Quaternion.identity);
                mapa[i, j].gameObject.tag = "tile";
                GameObject tempObj = mapa[i, j];
                tempObj.transform.parent = gameObject.transform;
                listaTile.Add(tempObj);
                if (i == j || (i+j)==10-1)
                {
                    Renderer rend = tempObj.GetComponent<Renderer>();
                    rend.material.color = druga;
                    
                }
                tempObj.SetActive(false);



                // LeanTween.scale(tempObj, new Vector3(3f, 0.1f, 3f), 2f).setEase(LeanTweenType.easeInSine);
            }
        }

        for(int i=0; i<100; i++)
        {
            StartCoroutine(tweenING());

        }
    }

    IEnumerator tweenING()
    {
        yield return new WaitForSeconds(3);
        if (indexListe == listaTile.Count) { prekidac = false; yield break; }
        GameObject temp = listaTile[indexListe];
        indexListe += 1;
        temp.SetActive(true);
        LeanTween.scale(temp, new Vector3(3f, 0.1f, 3f), 2f).setEase(LeanTweenType.easeOutBounce).setDelay(1f);
    }


    void Update()
    {


    }
}
