using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;

    public float spawnWait;
    public float startWait;
    public float WaveWait;

    public Text scoreText;
    int score;

    //public Text restartText;
    public GameObject restartButton;
    public Text gameOverText;

    bool gameOver;
    bool restartGame;

    void Start()
    {
        restartButton.SetActive(false);
        //restartText.text = "";
        gameOverText.text = "";
        StartCoroutine(SpawnWaves());
        UpdateScore();
        //scoreObject = GameObject.FindGameObjectWithTag("Score");
        
    }
    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;

                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Instantiate(hazard, spawnPosition, spawnRotation);
 
                if (gameOver)
                {
                    restartButton.SetActive(true);
                    //restartText.text = "Press R to restart";
                    restartGame = true;
                    break;
                }
                yield return new WaitForSeconds(Random.Range(0.5f,spawnWait));
               
            }
            yield return new WaitForSeconds(WaveWait);
            
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("main", LoadSceneMode.Single);
    }

    //private void Update()
    //{
    //    if (restartGame)
    //    {
    //        if (Input.GetKeyDown(KeyCode.R))
    //        {
    //            SceneManager.LoadScene("main", LoadSceneMode.Single);
    //        }
    //    }
    //}

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    public void AddToScore(int value) {
        score += value;
        UpdateScore();
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over";
        gameOver = true;
    }
    

	
}
