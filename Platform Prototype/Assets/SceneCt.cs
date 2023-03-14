using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCt : MonoBehaviour
{
    public static SceneCt Instance { set; get; }
    public int value;
    public int roundTime;
    public float startTime;
    public GameObject timer;
    public GameObject canvas;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            canvas = transform.GetChild(0).gameObject;
            timer = canvas.transform.GetChild(2).gameObject;
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
            transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        roundTime = (Time.time - startTime).ConvertTo<int>();
        timer.GetComponent<TextMeshProUGUI>().text = "Time: " + roundTime.ToString();
    }

    public void StartGame()
    {
        transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(false);
        SceneManager.LoadScene(1);
        timer.SetActive(true);
        startTime = Time.time;
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    public void Continue()
    {
        transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.SetActive(false);
        Time.timeScale = 1;
    }
    public void NextLevelMenu()
    {
        startTime = Time.time;
    }
}

