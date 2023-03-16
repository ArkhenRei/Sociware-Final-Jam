using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleCT : MonoBehaviour
{
    public bool turn;
    public float circleSpeed;
    // Start is called before the first frame update
    void Start()
    {
        turn = true;
        circleSpeed = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (transform.localScale.x > 0.9f && transform.localScale.x < 1.1f)
        {
            GameObject.FindWithTag("SceneCT").GetComponent<SceneCt>().FinishCombat();
        }*/
        if (turn == true && transform.localScale.x <= 1.5f)
        {
            transform.localScale = new Vector3(transform.localScale.x + circleSpeed * Time.deltaTime, transform.localScale.y + circleSpeed * Time.deltaTime, 1);
        }
        else if (turn == true && transform.localScale.x >= 1.5f)
        {
            turn = false;
        }
        if (turn == false && transform.localScale.x >= 0.5f)
        {
            transform.localScale = new Vector3(transform.localScale.x - circleSpeed * Time.deltaTime, transform.localScale.y - circleSpeed * Time.deltaTime, 1);
        }
        else if (turn == false && transform.localScale.x <= 0.5f)
        {
            turn = true;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (transform.localScale.x > 0.9f && transform.localScale.x < 1.1f)
            {
                GameObject.FindGameObjectWithTag("SceneCT").GetComponent<SceneCt>().FinishCombat();
                Destroy(GameObject.FindWithTag("Enemy"));
            }
            else
            {
                Time.timeScale = 0;
            }
        }
    }
}
