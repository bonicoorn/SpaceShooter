using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotator : MonoBehaviour
{

    public float tumble;
    Rigidbody rigidbody;

    // Use this for initialization
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        //rigidbody.angularVelocity = new Vector3(1, 1, 1) * tumble;
        rigidbody.angularVelocity = Random.insideUnitSphere * tumble;
    }

}
