using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Spawner : Singleton<Spawner>
{

    [SerializeField] private float timeToSpawn, speed;
    private List<GameObject> prefabs = new List<GameObject>();
    private List<GameObject> gameObjects = new List<GameObject>();
    private string[] tages = { "Wall", "WallDamage", "WallDead", "WallHeal" };
    private float timer;
    private enum Position { Left = -1, Mid = 0, Right = 1 }

    void Awake()
    {
        timer = timeToSpawn;


        for (int i = 0; i < tages.Length; i++)
            prefabs.Add(Resources.Load<GameObject>(tages[i]));
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
        MovementItems(gameObjects);
    }


    private void MovementItems(List<GameObject> GOs)
    {
        for (int i = 0; i < GOs.Count; i++)
            GOs[i].transform.position = new Vector2(GOs[i].transform.position.x, GOs[i].transform.position.y - speed * Time.deltaTime);
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



    private void SpawnItem(Position pos, float length, GameObject go = null, float height = 10)
    {
        if (go is not null)
        {
            GameObject Wall = Instantiate(go, new Vector2((int)pos * 2, height), Quaternion.identity);
            Wall.transform.localScale = new Vector2(Wall.transform.localScale.x, length);
            gameObjects.Add(Wall);
        }
    }
    public void SpawnWallLR(float height = 10)
    {
        float randomLength = Random.Range(3, 5);
        SpawnItem(Position.Left, randomLength, prefabs.Find(p => p.name.Equals("Wall")), height);
        SpawnItem(Position.Right, randomLength, prefabs.Find(p => p.name.Equals("Wall")), height);
    }
    public void SpawnRandomObstacle()
    {
        float randomLength = Random.Range(1, 5);
        int randomObstacle = Random.Range(1, prefabs.Count);
        if (prefabs[randomObstacle].tag == "dead" || prefabs[randomObstacle].tag == "damage")
            randomLength = 1;
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

}
