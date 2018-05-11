using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}



public class PlayerController : MonoBehaviour
{

    public float speed = 10;
    public Boundary boundary;
    public float tilt;
    Rigidbody rigidbody;

    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate = 0.5f;
    public float nextFire = 0.0f;


    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetButton("MyFire") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            //GameObject clone
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation); //as GameObject
            GetComponent<AudioSource>().Play();
        }
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        rigidbody.rotation = Quaternion.Euler(0f, 0f, rigidbody.velocity.x * -tilt);
        rigidbody.velocity = new Vector3(moveHorizontal, 0, moveVertical) * speed;
        rigidbody.position = new Vector3
            (
            Mathf.Clamp(rigidbody.position.x, boundary.xMin, boundary.xMax),
            0f,
            Mathf.Clamp(rigidbody.position.z, boundary.zMin, boundary.zMax)
            );
    }
}
