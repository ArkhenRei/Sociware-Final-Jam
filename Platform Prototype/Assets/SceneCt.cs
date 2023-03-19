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


    [SerializeField] int LevelTime =15;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            canvas = transform.GetChild(0).gameObject;
            timer = canvas.transform.GetChild(2).gameObject;
            timerText = canvas.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject;
        

            //score = 0;
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
          
            transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.SetActive(true);
            //Time.unscaledTime = 0;
        }
        roundTime = (Time.time - startTime).ConvertTo<int>();
        timerText.GetComponent<TextMeshProUGUI>().text = "Time: " + (LevelTime-roundTime).ToString();
        //scoreText.GetComponent<TextMeshProUGUI>().text = "Score: " + score.ToString();
    }
    private void FixedUpdate()
    {
        if (roundTime > LevelTime && SceneManager.GetActiveScene().buildIndex !=1)
        {
            LoadScene();
        }
    }

    public void StartGame()
    {
        
        SceneManager.LoadScene(1);
        transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(false);
        timer.SetActive(true);
        startTime = Time.time;
       
       
        
        
       
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void NextLevel()
    {
     
        startTime = Time.time;
        timer.SetActive(true);
        
        canvas.transform.GetChild(3).gameObject.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        
    }
    public void Continue()
    {
        transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.SetActive(false);
        timer.SetActive(true);
       
        Time.timeScale = 1;
    }
    public void FinishLevel()
    {
       
        canvas.transform.GetChild(3).gameObject.SetActive(true);
        timer.SetActive(false);
        
        Time.timeScale = 0;
        

    }

    public void LoadScene()
    {
       
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        startTime = Time.time;
    }

   
    public void FinishCombat()
    {
        GameObject.FindGameObjectWithTag("Parent").gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        GameObject.FindGameObjectWithTag("Parent").gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}

