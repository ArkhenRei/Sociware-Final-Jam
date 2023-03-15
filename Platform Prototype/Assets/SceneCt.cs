using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class SceneCt : MonoBehaviour
{
    public static SceneCt Instance { set; get; }
    public int value;
    public int roundTime;
    public float startTime;
    public GameObject timer;
    public GameObject timerText;
    public GameObject canvas;
    public int score;
    public GameObject scoreUI;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            canvas = transform.GetChild(0).gameObject;
            timer = canvas.transform.GetChild(2).gameObject;
            timerText = canvas.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject;
            scoreUI = canvas.transform.GetChild(4).gameObject;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            timer.SetActive(false);
            scoreUI.SetActive(false);
            transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        roundTime = (Time.time - startTime).ConvertTo<int>();
        timerText.GetComponent<TextMeshProUGUI>().text = "Time: " + roundTime.ToString();
    }

    public void StartGame()
    {
        transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(false);
        SceneManager.LoadScene(1);
        timer.SetActive(true);
        scoreUI.SetActive(true);
        startTime = Time.time;
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void NextLevel()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        startTime = Time.time;
        timer.SetActive(true);
        scoreUI.SetActive(true);
        Time.timeScale = 1;
    }
    public void Continue()
    {
        transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.SetActive(false);
        timer.SetActive(true);
        scoreUI.SetActive(true);
        Time.timeScale = 1;
    }
    public void FinishLevel()
    {
        canvas.transform.GetChild(3).gameObject.SetActive(true);
        timer.SetActive(false);
        scoreUI.SetActive(false);
        Time.timeScale = 0;
        score = score + (60000 * (1 / roundTime));
    }

}

