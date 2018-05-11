using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{

    Rigidbody rigidbody;
    public float speed;

    public void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = rigidbody.transform.forward * speed;
    }
}
