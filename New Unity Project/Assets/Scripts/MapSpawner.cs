using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class MapSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> terrains = new List<GameObject>();
    [SerializeField] private int distanceBetweenSquares = 4;
    private float maximumPositionX = 36;
    private float minPositionX = 0;
    private float maximumPositionZ = 36;
    private float minPositionZ = 0;
    private Vector3 nextPosition = new Vector3(0, 0, 0);
    public GameObject TerrainPrefab;
    public Color secondColor;
    public GameObject obstaclePrefab; //fsdf
    private List<GameObject> listTile;
    private List<GameObject> listObstacles = new List<GameObject>();
    private List<Renderer> listOfRend = new List<Renderer>();
    private List<GameObject> listOfObstacleBody = new List<GameObject>();
    private Color orignialTileColor;
    private int indexListe;
    public float obstaclesRotateSpeed = 2f;
    private float timer = 0f;
    float startObstaclePosY;
    bool flag = false;
    private float timerSecond = 0f;
    private enum Direction { left, right, down, up, stop };
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
                listOfRend.Add(tempObj.GetComponent<Renderer>());
                tempObj.SetActive(false);
                // LeanTween.scale(tempObj, new Vector3(3f, 0.1f, 3f), 2f).setEase(LeanTweenType.easeInSine);
            }
        }

        for (int i = 0; i < 100; i++)
        {
            StartCoroutine(TweenIng());
        }

        StartCoroutine(SpawnObstacles());

        orignialTileColor = listOfRend[0].material.color;

    }
    void Update()
    {
        timer += Time.deltaTime;
        timerSecond += Time.deltaTime;


        moveTiles();

        if (timerSecond >= 3f && flag)
        {
            ObstaclesLiftDown();
            timer = 0;
            timerSecond = 0;
        }

        if (timer >= 6f)
        {
            flag = true;
        }

    }

    IEnumerator SpawnObstacles()
    {
        yield return new WaitForSeconds(5f);
        foreach (var x in listTile)
        {
            GameObject t = Instantiate(obstaclePrefab, x.transform.position + new Vector3(0, 1, 0), Quaternion.identity);
            t.transform.parent = x.transform;
            t.transform.GetChild(0).gameObject.SetActive(false);
            listObstacles.Add(t);
            listOfObstacleBody.Add(t.transform.GetChild(0).gameObject);
        }
       
    }

    IEnumerator TweenIng()
    {
        yield return new WaitForSeconds(2);
        if (indexListe == listTile.Count)
        {
            yield break;
        }
        GameObject temp = listTile[indexListe];
        indexListe += 1;
        temp.SetActive(true);
        LeanTween.scale(temp, new Vector3(3f, 0.1f, 3f), 2f).setEase(LeanTweenType.easeOutBounce).setDelay(1f);
    }


    #region MoveLogic
    public void moveTiles()
    {   //Logic, moves only once 
        if (direction != Direction.stop)
        {
            switch (direction)
            {
                case Direction.down: MoveTilesDown(); break;
                case Direction.up: MoveTilesUp(); break;
                case Direction.left: MoveTilesLeft(); break;
                case Direction.right: MoveTilesRight(); break;
                default: break;
            }
            direction = Direction.stop;
        }
    }

    public void MoveTiles(int dirr)
    {
        switch (dirr)
        {
            case (int)Direction.down: direction = Direction.down; break;
            case (int)Direction.up: direction = Direction.up; break;
            case (int)Direction.left: direction = Direction.left; break;
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
                nextPosition = new Vector3(el.transform.position.x, 0, minPositionZ - distanceBetweenSquares);
                LeanTween.move(el, nextPosition, 0.1f).setEase(LeanTweenType.easeInOutCirc);
            }
        }
        minPositionZ -= distanceBetweenSquares;
        maximumPositionZ -= distanceBetweenSquares;
    }

    public void MoveTilesLeft()
    {
        foreach (var el in listTile)
        {
            if (el.transform.position.z == this.minPositionZ)
            {
                nextPosition = new Vector3(el.transform.position.x, 0, maximumPositionZ + distanceBetweenSquares);
                LeanTween.move(el, nextPosition, 0.1f).setEase(LeanTweenType.easeInQuint);
            }
        }
        minPositionZ += distanceBetweenSquares;
        maximumPositionZ += distanceBetweenSquares;
    }

    public void MoveTilesUp()
    {
        foreach (var el in listTile)
        {
            if (el.transform.position.x == this.minPositionX)
            {
                nextPosition = new Vector3(maximumPositionX + distanceBetweenSquares, 0, el.transform.position.z);
                LeanTween.move(el, nextPosition, 0.1f).setEase(LeanTweenType.easeInOutCirc);
            }
        }
        minPositionX += distanceBetweenSquares;
        maximumPositionX += distanceBetweenSquares;
    }

    public void MoveTilesDown()
    {
        List<GameObject> movedTiles = new List<GameObject>();

        foreach (var el in listTile)
        {
            if (el.transform.position.x == this.maximumPositionX)
            {
                nextPosition = new Vector3(minPositionX - distanceBetweenSquares, 0, el.transform.position.z);
                LeanTween.move(el, nextPosition, 0.1f).setEase(LeanTweenType.easeInOutCirc);

                movedTiles.Add(el);
            }
        }
        minPositionX -= distanceBetweenSquares;
        maximumPositionX -= distanceBetweenSquares;
    }
    #endregion

    public void ObstaclesLiftDown()
    {
        int numberOfRandomObstacles = Random.Range(10, 30);

        for(int i=0; i< numberOfRandomObstacles; i++)
        {
            int index = Random.Range(0, listObstacles.Count);
            //Vector3 pos = liftOfObstacleBody[index].transform.position;

            if (listOfObstacleBody[index].gameObject.activeInHierarchy ==false)
            {

                 StartCoroutine(ChangeColorOfRendForSec(index,Color.red));
                 StartCoroutine(ObstacleActiveSwitcher(index, true));
                // LeanTween.moveY(liftOfObstacleBody[index],-3f, 0.14f);

            }
            else if(listOfObstacleBody[index].gameObject.activeInHierarchy ==true)
            {
                StartCoroutine(ChangeColorOfRendForSec(index,Color.green));
                StartCoroutine(ObstacleActiveSwitcher(index, false));

                // LeanTween.moveY(liftOfObstacleBody[index], 3f, 0.14f);
            }
        }

    }
  
    public IEnumerator ChangeColorOfRendForSec(int index,Color color)
    {   
        
        float spawnDelay = 1f;
        float tileFlashSpeed = 4f;
        Material tileMat = listOfRend[index].material;
        Color initialCOlor = tileMat.color;
        Color flashColor=color;
        float spawnTimer = 0;

        while (spawnTimer < spawnDelay)
        {
            tileMat.color = Color.Lerp(initialCOlor, flashColor, Mathf.PingPong(spawnTimer * tileFlashSpeed, 1f));
            spawnTimer += Time.deltaTime;

            yield return null;
        }
        tileMat.color = orignialTileColor;
    }


   IEnumerator ObstacleActiveSwitcher(int index,bool status)
    {
        yield return new WaitForSeconds(1.1f);
        listOfObstacleBody[index].SetActive(status);

    }

}

