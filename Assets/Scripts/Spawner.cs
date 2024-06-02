using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Spawner : Singleton<Spawner>
{

    [SerializeField] private float timeToSpawn, speed;
    public LayerMask wallLayer;
    private List<GameObject> prefabs = new List<GameObject>();
    private List<GameObject> gameObjects = new List<GameObject>();
    private string[] nameWalls = { "Wall", "WallDamage", "WallDead", "WallHeal" };
    private string[] tages = { "wall", "heal", "damage", "dead" };
    private float timer;
    private enum Position { Left = -1, Mid = 0, Right = 1 }

    void Awake()
    {
        timer = timeToSpawn;

        for (int i = 0; i < nameWalls.Length; i++)
            prefabs.Add(Resources.Load<GameObject>(nameWalls[i]));
    }


    void Update()
    {
        if (timer <= 0)
        {
            timer = timeToSpawn;
            SpawnWallLR();
            SpawnRandomObstacle();
        }
        else
            timer -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        MovementItems(gameObjects);
    }


    private void MovementItems(List<GameObject> GOs)
    {
        for (int i = 0; i < GOs.Count; i++)
            GOs[i].transform.position = new Vector2(GOs[i].transform.position.x, GOs[i].transform.position.y - speed * Time.fixedDeltaTime);
        CheckPositionItem(GOs);
    }

    private void CheckPositionItem(List<GameObject> GOs)
    {
        for (int i = 0; i < GOs.Count; i++)
        {
            if (GOs[i].transform.position.y <= -7)
                DestroyItemWithIndex(i);

        }
    }

    public void StartSpawnWalls()
    {
        for (int i = 1; i < 4; i++)
            SpawnWallLR(height: i*5 - 3, length: 4.5f, random: false);
    }

    private void SpawnItem(Position pos, float length, GameObject go = null, float height = 10)
    {
        if (go is not null)
        {
            GameObject Wall = Instantiate(go, new Vector2((int)pos * 2, height), Quaternion.identity);
            Wall.transform.localScale = new Vector2(Wall.transform.localScale.x, length);
            gameObjects.Add(Wall);
            CheckPositionWall(Wall.GetComponent<BoxCollider2D>(), gameObjects.Count-1);
        }
    }
    public void SpawnWallLR(float height = 10, bool random = true, float length = 1)
    {
        if (random) length = Random.Range(2, 4);
        SpawnItem(Position.Left, length, prefabs.Find(p => p.name.Equals("Wall")), height);
        SpawnItem(Position.Right, length, prefabs.Find(p => p.name.Equals("Wall")), height);
    }
    public void SpawnRandomObstacle()
    {
        float randomLength = Random.Range(1, 5);
        int randomObstacle = Random.Range(1, prefabs.Count);
        if (prefabs[randomObstacle].tag == "dead" || prefabs[randomObstacle].tag == "damage")
            randomLength = Random.Range(1, 2);
        SpawnItem(Position.Mid, randomLength, prefabs[randomObstacle]);
    }
    public void AllDestroyItem()
    {
        for (int i = 0; i < tages.Length; i++)
            DestroyItemWithTag(tages[i]);
    }
    public void DestroyItemWithTag(string tag)
    {
        for (int i = gameObjects.Count - 1; i >= 0; i--)
            if (gameObjects[i].tag == tag)
            {
                Destroy(gameObjects[i]);
                gameObjects.RemoveAt(i);
            }
    }
    public void DestroyItemWithIndex(int index)
    {
        for (int i = gameObjects.Count - 1; i >= 0; i--)
            if (i == index)
            {
                Destroy(gameObjects[i]);
                gameObjects.RemoveAt(i);
            }
    }
    private void CheckPositionWall(BoxCollider2D wallCollider, int index = -1)  
    {
        Vector2 size = wallCollider.GetComponent<BoxCollider2D>().size;
        Vector2 center = wallCollider.GetComponent<BoxCollider2D>().bounds.center;
        Collider2D[] results = Physics2D.OverlapBoxAll(center, size, 0, wallLayer);

        foreach (var result in results)
            if (result != wallCollider)
                DestroyItemWithIndex(index);
    }


}
