using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    public GameObject explotion;

    public GameObject explotionPlayer;

    GameObject cloneExplotion;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            cloneExplotion = Instantiate(explotionPlayer, GetComponent<Rigidbody>().position, GetComponent<Rigidbody>().rotation);
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
        }

    }
}
