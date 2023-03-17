using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleCT : MonoBehaviour
{
    public bool turn;
    public float circleSpeed;
    public bool secondChance;
    public GameObject sceneCT;
    public GameObject player;
    public GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        sceneCT = GameObject.FindGameObjectWithTag("SceneCT");
        player = GameObject.FindWithTag("Player");
        enemy = GameObject.FindWithTag("Enemy");
        secondChance = false;
        turn = true;
        circleSpeed = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (transform.localScale.x > 0.9f && transform.localScale.x < 1.1f)
            {
                if (secondChance)
                {
                    GameObject.FindGameObjectWithTag("Combat").SetActive(false);
                    player.GetComponent<Animator>().SetTrigger("Attack");
                    enemy.GetComponent<Animator>().SetTrigger("Death");
                    sceneCT.GetComponent<SceneCt>().score += 1000;
                    Move.fight = false;
                    sceneCT.GetComponent<SceneCt>().FinishCombat();


                }
                else if(!secondChance)
                {
                    GameObject.FindGameObjectWithTag("Combat").SetActive(false);

                    player.GetComponent<Animator>().SetTrigger("Attack");
                    enemy.GetComponent<Animator>().SetTrigger("Death");
                    sceneCT.GetComponent<SceneCt>().score += 2000;
                    Move.fight = false;
                    sceneCT.GetComponent<SceneCt>().FinishCombat();
                }




            }
            else
            {
                if (!secondChance)
                {
                    circleSpeed = 0.4f;
                    transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    secondChance = true;


                }
                else if ( secondChance)
                {
                    GameObject.FindGameObjectWithTag("Combat").SetActive(false);
                    enemy.GetComponent<Animator>().SetTrigger("Attack");
                    player.GetComponent<Animator>().SetTrigger("Death");
                    

                }
            }
           

        }

    }
    private void FixedUpdate()
    {

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
    }
}
