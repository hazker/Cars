using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Car player;
    public Transform startPos;
    public MyProgress myProgress;


    float currentTime=0;
    bool paussed=true;

    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                DontDestroyOnLoad(instance);
            }
            return instance;
        }
    }

    private void Update()
    {
        if (!paussed)
        {
            currentTime += Time.deltaTime;
            UIManager.Instance.ChangeCurrentTime(currentTime);
        }

    }

    public void RecordTheTime()
    {
        if(currentTime< myProgress.bestTime || myProgress.bestTime==0)
        {
            myProgress.bestTime = currentTime;
            UIManager.Instance.ChangeBestTime();
        }
        
    }

    private void Awake()
    {
        myProgress.bestTime = PlayerPrefs.GetFloat("bestTime");
        myProgress.attemtpsAmount = PlayerPrefs.GetInt("attemptsAmount");
    }

    public void GameStart()
    {
        myProgress.attemtpsAmount++;
        PlayerPrefs.SetInt("attemptsAmount", myProgress.attemtpsAmount);
        Time.timeScale = 1;
        paussed = false;
        player.enabled = true;
        player.GetComponentInParent<Rigidbody>().velocity = Vector3.zero;
        player.GetComponentInParent<Rigidbody>().transform.position= startPos.position;
        currentTime = 0;
        UIManager.Instance.ChangeCurrentTime(currentTime);
    }

    public void GameStop()
    {
        Time.timeScale = 0;
        player.enabled = false;
        paussed = true;
        foreach (var item in player.GetComponentsInChildren<LineRenderer>())
        {
            item.positionCount = 0;
        } 
    }

    public void GameResume()
    {
        Time.timeScale = 1;
        paussed = false;
        player.enabled = true;
    }
}
