using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public GameObject hazard;
    public Vector3 spawnValues;
    public int hazardCount;

    public float spawnWait;
    public float startWait;
    public float WaveWait;

    public Text scoreText;
    int score;

    public Text restartText;
    public Text gameOverText;

    bool gameOver;
    bool restartGame;

    void Start()
    {
        restartText.text = "";
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
                Instantiate(hazard, spawnPosition, spawnRotation);
 
                if (gameOver)
                {
                    restartText.text = "Press R to restart";
                    restartGame = true;
                    break;
                }
                yield return new WaitForSeconds(Random.Range(0.5f,spawnWait));
               
            }
            yield return new WaitForSeconds(WaveWait);
            
        }
    }

    private void Update()
    {
        if (restartGame)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("main", LoadSceneMode.Single);
            }
        }
    }

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
