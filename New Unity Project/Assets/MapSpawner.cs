using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> terrains = new List<GameObject> ();
    [SerializeField] private float playerJumpDistance = 4;
    public GameObject TerrainPrefab;
    public Color      secondColor;

    private List<GameObject> listaTile;
    private GameObject       [,]mapa;
    private int              indexListe;

    private bool left   = false;
    private bool right  = false;
    private bool down   = false;
    private bool up     = false;
    private bool moved = false;
    private int numberOfTiles = 10;

    void Start()
    {

        listaTile = new List<GameObject>();
        Vector3 position = new Vector3(0, 0, 0);
        mapa = new GameObject[10, 10];
        indexListe = 0;


        for (int i=0; i<10; i++)
        {
            position.x=4*i;
            for (int j = 0; j < 10; j++)
            {
                position.z = 4 * j;
                mapa[i, j] = Instantiate(TerrainPrefab, position, Quaternion.identity);
                mapa[i, j].gameObject.tag = "Tile";
                GameObject tempObj = mapa[i, j];
                tempObj.transform.parent = gameObject.transform;
                listaTile.Add(tempObj);
                /* 
                 if (i == j || (i+j)==10-1)
                 {
                     Renderer rend = tempObj.GetComponent<Renderer>();
                     rend.material.color = secondColor;
                 }
                */
                Renderer rend = tempObj.GetComponent<Renderer>();

                if (i == 0)
                {
                    rend.material.color = Color.cyan;
                }
                if (i == 1)
                {
                    rend.material.color = Color.white;
                }
                if (i == 2)
                {
                    rend.material.color = Color.black;
                }
                if (i == 3)
                {
                    rend.material.color = Color.gray;
                }
                if (i == 4)
                {
                    rend.material.color = Color.green;
                }
                if (i == 5)
                {
                    rend.material.color = Color.blue;
                }
                if (i == 6)
                {
                    rend.material.color = Color.red;
                }
                if (i == 7)
                {
                    rend.material.color = Color.yellow;
                }
                if (i == 8)
                {
                    rend.material.color = new Color32(173, 173, 21, 200);
                }
                if (i == 9)
                {
                    rend.material.color = new Color32(100, 2, 21, 12);
                }

                tempObj.SetActive(false);
                // LeanTween.scale(tempObj, new Vector3(3f, 0.1f, 3f), 2f).setEase(LeanTweenType.easeInSine);
            }
        }

        for(int i=0; i<100; i++)
        {
            StartCoroutine(TweenIng());
        }
    }

    IEnumerator TweenIng()
    {
        yield return new WaitForSeconds(3);
        if (indexListe == listaTile.Count) 
        { 
            yield break; 
        }
        GameObject temp = listaTile[indexListe];
        indexListe += 1;
        temp.SetActive(true);
        LeanTween.scale(temp, new Vector3(3f, 0.1f, 3f), 2f).setEase(LeanTweenType.easeOutBounce).setDelay(1f);
    }


    

    void Update()
    {
        if (moved)
        {   
            if(right)
            {
                MoveTilesRight();
            }
             if (left)
            {
                MoveTilesLeft();
            }
             if (up)
            {
                MoveTilesUp();
            }
            if (down)
            {
                MoveTilesDown();
                down = false;
                Debug.Log("POZAVNA DA IDE DOLE!!");
            }
            moved = false;

        }

    }



    public void MoveTiles(string s)
    {
        if (s == "left")
        {
            this.left = true;
        }
        else if (s == "right")
        {
            this.right = true;
        }
        else if (s == "down")
        {
           // Debug.Log("POMERI MAPU DOLE");

            down = true;
        }
        else if (s == "up")
        {
            up = true;
        }
        moved = true;
    }
    

    public void MoveTilesDown()
    {   
        for(int i=0; i< numberOfTiles; i++)
        {
            for(int j=0; j< numberOfTiles; j++)
            {
                if (i ==numberOfTiles-1)
                {
                    mapa[i, j].transform.position = mapa[0, j].transform.position + new Vector3(-4, 0,0);

                }
            }
        }
        SwapCoulumnsUP();


    }
    public void MoveTilesUp()
    {

    }
    public void MoveTilesLeft()
    {

    }
    public void MoveTilesRight()
    {

    }

    public void SwapCoulumnsUP()
    {
        GameObject[] arrayTmp = new GameObject[numberOfTiles];
       
        for(int i=0; i<10; i++)
        {
            arrayTmp[i] = mapa[numberOfTiles - 1, i];
            Renderer ob= arrayTmp[i].GetComponent<Renderer>();
            ob.material.color = Color.red;

        }

        for(int i=numberOfTiles-1; i>0; i--)
        {
            for (int j = numberOfTiles - 1; j >= 0; j--)
            {
                mapa[i, j] = mapa[i - 1, j];
               
            }
        }
        
        
        
        for(int i=0; i<10; i++)
        {
            mapa[0, i] = arrayTmp[i];
        }

    }

}
