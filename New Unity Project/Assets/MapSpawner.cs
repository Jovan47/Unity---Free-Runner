using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> terrains = new List<GameObject>();
    // [SerializeField] private int count;
    [SerializeField] private float playerJumpDistance = 4;
    [SerializeField] private int numberOfSquares = 10;
    [SerializeField] private int distanceBetweenSquares;
    private float maximumPositionX = 36;
    private float startPositionX = 0;
    private float maximumPositionZ = 36;
    private float startPositionZ = 0;
    private Vector3 nextPosition = new Vector3(0, 0, 0);
    public GameObject TerrainPrefab;
    public Color secondColor;




    private List<GameObject> listTile;
    private GameObject[,] mapa;
    private int indexListe;

    private bool left = false;
    private bool right = false;
    private bool down = false;
    private bool up = false;
    private bool moved = false;
    private int numberOfTiles = 10;

    void Start()
    {


        listTile = new List<GameObject>();
        Vector3 position = new Vector3(0, 0, 0);
        mapa = new GameObject[10, 10];
        indexListe = 0;


        for (int i = 0; i < 10; i++)
        {
            position.x = 4 * i;
            for (int j = 0; j < 10; j++)
            {
                position.z = 4 * j;
                mapa[i, j] = Instantiate(TerrainPrefab, position, Quaternion.identity);
                mapa[i, j].gameObject.tag = "Tile";
                GameObject tempObj = mapa[i, j];
                tempObj.transform.parent = gameObject.transform;
                listTile.Add(tempObj);

                terrains.Add(tempObj);

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

        for (int i = 0; i < 100; i++)
        {
            StartCoroutine(TweenIng());
        }
    }

    IEnumerator TweenIng()
    {
        yield return new WaitForSeconds(3);
        if (indexListe == listTile.Count)
        {
            yield break;
        }
        GameObject temp = listTile[indexListe];
        indexListe += 1;
        temp.SetActive(true);
        LeanTween.scale(temp, new Vector3(3f, 0.1f, 3f), 2f).setEase(LeanTweenType.easeOutBounce).setDelay(1f);
    }




    void Update()
    {
        moveTiles();

    }

    public void moveTiles()
    {   //kill switch, moves only once
        if (moved)
        {
            if (right)
            {
                MoveTilesRight();
                right = false;
            }
            if (left)
            {
                MoveTilesLeft();
                left = false;
            }
            if (up)
            {
                MoveTilesUp();
                up = false;
            }
            if (down)
            {
                MoveTilesDown();
                down = false;
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
            down = true;
        }
        else if (s == "up")
        {
            up = true;
        }
        moved = true;
    }

    public void MoveTilesRight()
    {
        foreach (var el in listTile)
        {
            if (el.transform.position.z == this.maximumPositionZ)
            {
                nextPosition = new Vector3(el.transform.position.x, 0, startPositionZ - distanceBetweenSquares);
                LeanTween.move(el, nextPosition, 0.35f).setEase(LeanTweenType.easeInOutCirc);
            }
        }
        startPositionZ -= distanceBetweenSquares;
        maximumPositionZ -= distanceBetweenSquares;
    }
    public void MoveTilesLeft()
    {
        foreach (var el in listTile)
        {
            if (el.transform.position.z == this.startPositionZ)
            {
                nextPosition = new Vector3(el.transform.position.x, 0, maximumPositionZ + distanceBetweenSquares);
                LeanTween.move(el, nextPosition, 0.35f).setEase(LeanTweenType.easeInQuint);
            }
        }
        startPositionZ += distanceBetweenSquares;
        maximumPositionZ += distanceBetweenSquares;
    }
    public void MoveTilesUp()
    {
        foreach (var el in listTile)
        {
            if (el.transform.position.x == this.startPositionX)
            {
                nextPosition = new Vector3(maximumPositionX + distanceBetweenSquares, 0, el.transform.position.z);
                LeanTween.move(el, nextPosition, 0.35f).setEase(LeanTweenType.easeInOutCirc);
            }
        }
        startPositionX += distanceBetweenSquares;
        maximumPositionX += distanceBetweenSquares;

    }
    public void MoveTilesDown()
    {
        foreach (var el in listTile)
        {
            if (el.transform.position.x == this.maximumPositionX)
            {
                nextPosition = new Vector3(startPositionX - distanceBetweenSquares, 0, el.transform.position.z);
                LeanTween.move(el, nextPosition, 0.35f).setEase(LeanTweenType.easeInOutExpo);
            }
        }
        startPositionX -= distanceBetweenSquares;
        maximumPositionX -= distanceBetweenSquares;
    }
}

