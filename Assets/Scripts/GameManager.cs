using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    //TODO переделать обработку закрытия/открытия окна
    public GameObject MenuWindow;

    void Start()
    {
        StartLevel();
    }


    public void StartLevel()
    {
        Spawner.Instance.AllDestroyItem();
        ScoreManager.Instance.ResetScore();
        HeartManager.Instance.ResetHeart();

        Player.Instance.transform.position = Vector2.zero;


        Spawner.Instance.StartSpawnWalls();
    }



    public void CloseMenu()
    {
        Time.timeScale = 1.0f;

        MenuWindow.SetActive(false);
    }

    public void OpenMenu()
    {
        Time.timeScale = 0.0f;
        MenuWindow.SetActive(true);
    }

}
