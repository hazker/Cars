using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text bestTime;
    public Text currentTime;
    public Text tensionPowerText;
    public Text complitionText;
    public GameObject gameUI;
    public Text rating;
    public GameObject winPanel;
    public Text finalText;
    public Image trafficLight;
    public Sprite redLight;
    public Sprite greenLight;
    private static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIManager>();
                DontDestroyOnLoad(instance);
            }
            return instance;
        }
    }

    private void Start()
    {
        ChangeBestTime();
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Restart()
    {
        GameManager.Instance.GameStart();
    }

    public void ChangeBestTime()
    {
        bestTime.text = "Best time: " + GameManager.Instance.myProgress.bestTime.ToString();
        PlayerPrefs.SetFloat("bestTime", GameManager.Instance.myProgress.bestTime);
    }

    public void ChangeCurrentTime(float time)
    {
        currentTime.text = "Time: " + time.ToString();
    }

    public void tensionPower(float pow)
    {
        tensionPowerText.text = "Tension power: " + Mathf.FloorToInt(pow) + "%";
    }

    public void CompletedProc(float dist)
    {
        complitionText.text = "Completed: " + Mathf.FloorToInt(dist) + "%";
    }

    public void Rating()
    {
        rating.text = "Best time: " + GameManager.Instance.myProgress.bestTime + "\nTotal amount of attempts: " + GameManager.Instance.myProgress.attemtpsAmount;
    }

    public void Win()
    {
        gameUI.SetActive(false);
        winPanel.SetActive(true);
        finalText.text = "You completed the level!\n" + currentTime.text + "\nYour best time:" + GameManager.Instance.myProgress.bestTime + "\nYour total amount of attempts: " + GameManager.Instance.myProgress.attemtpsAmount;
    }

    public void ChangeTrafficLight(bool red)
    {
        if (!red)
            trafficLight.sprite = greenLight;
        else
            trafficLight.sprite = redLight;
    }
}
