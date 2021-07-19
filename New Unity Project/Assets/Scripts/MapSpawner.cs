using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> terrains = new List<GameObject>();
    [SerializeField] private int distanceBetweenSquares=4;
    private float maximumPositionX = 36;
    private float startPositionX = 0;
    private float maximumPositionZ = 36;
    private float startPositionZ = 0;
    private Vector3 nextPosition = new Vector3(0, 0, 0);
    public GameObject TerrainPrefab;
    public Color secondColor;
    public GameObject obstaclePrefab; //fsdf
    private int numberOfObstacles = 10;
    private List<GameObject> listTile;
    private int indexListe;

    
    private enum Direction{ left,right,down,up,stop};
    Direction direction;

    void Start()
    {
        listTile = new List<GameObject>();
        Vector3 position = new Vector3(0, 0, 0);
        indexListe = 0;

        for (int i = 0; i < 10; i++)
        {
            position.x = 4 * i;
            for (int j = 0; j < 10; j++)
            {
                position.z = 4 * j;
                GameObject tempObj = Instantiate(TerrainPrefab, position, Quaternion.identity);
                tempObj.gameObject.tag = "Tile";
                tempObj.transform.parent = gameObject.transform;
                listTile.Add(tempObj);
                terrains.Add(tempObj);
        
                tempObj.SetActive(false);
                // LeanTween.scale(tempObj, new Vector3(3f, 0.1f, 3f), 2f).setEase(LeanTweenType.easeInSine);
            }
        }

        for (int i = 0; i < 100; i++)
        {
            StartCoroutine(TweenIng());
        }
        StartCoroutine(MakeObstacles());
    }

    IEnumerator MakeObstacles()
    {
        yield return new WaitForSeconds(8);

        for (int i = 1; i < listTile.Count; i++)
        {
            if ((i * i) % 8 == 0 && numberOfObstacles > 0)
            {
                GameObject t = Instantiate(obstaclePrefab, listTile[i].transform.position + new Vector3(0, 1, 0), Quaternion.identity);
                t.transform.parent = listTile[i].transform;
                numberOfObstacles--;
            }
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
    {   //Logic, moves only once 
        if (direction!=Direction.stop)
        {
            switch (direction)
            {
                case Direction.down:  MoveTilesDown();  break;
                case Direction.up:    MoveTilesUp();    break;
                case Direction.left:  MoveTilesLeft();  break;
                case Direction.right: MoveTilesRight(); break;
                default: break;
            }
            direction = Direction.stop;
        }
    }

    public void MoveTiles(int dirr)
    {
        switch(dirr)
        {
            case (int)Direction.down:  direction = Direction.down;  break;
            case (int)Direction.up:    direction = Direction.up;    break;
            case (int)Direction.left:  direction = Direction.left;  break;
            case (int)Direction.right: direction = Direction.right; break;
            default: break;
        }
    }

    public void MoveTilesRight()
    {
        foreach (var el in listTile)
        {
            if (el.transform.position.z == this.maximumPositionZ)
            {
                nextPosition = new Vector3(el.transform.position.x, 0, startPositionZ - distanceBetweenSquares);
                LeanTween.move(el, nextPosition, 0.18f).setEase(LeanTweenType.easeInOutCirc);
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
                LeanTween.move(el, nextPosition, 0.18f).setEase(LeanTweenType.easeInQuint);
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
                LeanTween.move(el, nextPosition, 0.18f).setEase(LeanTweenType.easeInOutCirc);
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
                LeanTween.move(el, nextPosition, 0.18f).setEase(LeanTweenType.easeInOutCirc);
            }
        }
        startPositionX -= distanceBetweenSquares;
        maximumPositionX -= distanceBetweenSquares;
    }
}

