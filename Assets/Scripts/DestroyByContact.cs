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

    public GameObject megaExplosion;

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

     
    /// <summary>
    /// функция обрабатывает столкновения
    /// если любая частица Particle System столкнулась с объектом, к которому привязан данный скрипт, то обрабатывается код внутри данного метода
    /// (сработает если на Particle System объекте включено Send Collision Messages)
    /// </summary>
    /// <param name="other"></param>
    private void OnParticleCollision(GameObject other)
    {
        //клонируем префаб взрыва
        cloneExplotion = (GameObject)Instantiate(megaExplosion, GetComponent<Rigidbody>().position, GetComponent<Rigidbody>().rotation);
        //уничтожаем объект, на котором данный скрипт
        Destroy(gameObject);
        //удаляем клон взыва, после того как он произошел и отработала анимация
        Destroy(cloneExplotion, 0.7f);
        //начисление очков за уничтожение
        gameController.AddToScore(scoreValue);
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
