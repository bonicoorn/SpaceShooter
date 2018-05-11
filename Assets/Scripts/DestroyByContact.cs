using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    public GameObject explotion;

    public GameObject explotionPlayer;

    public int scoreValue;

    GameObject cloneExplotion;

    GameController gameController;

    private void Start()
    {
        GameObject gameControllerObject = GameObject.FindGameObjectWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        else
        {
            Debug.Log("GameController не найден");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            cloneExplotion = Instantiate(explotionPlayer, GetComponent<Rigidbody>().position, GetComponent<Rigidbody>().rotation);
            gameController.GameOver();
            Destroy(other.gameObject);
            Destroy(gameObject);

            Destroy(cloneExplotion, 1f);
        }

        if (other.tag == "Bolt")
        {
            cloneExplotion = Instantiate(explotion, GetComponent<Rigidbody>().position, GetComponent<Rigidbody>().rotation);
            Destroy(other.gameObject);
            Destroy(gameObject);

            Destroy(cloneExplotion, 1f);

            gameController.AddToScore(scoreValue);
        }

    }
}
