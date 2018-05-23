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
    public GameObject megaShot;

    public Transform[] shotSpawns;

    public float fireRate = 0.5f;
    public float fireRateMegaShot = 0.5f;

    public float nextFire = 0.0f;

    public Quaternion calibrationQuaternion;

    public SimpleTouchPad touchPad;

    private void Start()
    {
        CalibrateAccelerometr();

        rigidbody = GetComponent<Rigidbody>();
    }

    private void CalibrateAccelerometr()
    {
        //получаем данные с датчика акселерометра и записываем в переменную
        Vector3 accelerationSnapshot = Input.acceleration;

        //поворачиваем из положения лицом вверх в положение, которое было получено из акселерометра
        //функция возвращает координаты положения телефона в пространстве типа кватернион
        //т.е. текущее положение телефона
        Quaternion rotateQuaternion = Quaternion.FromToRotation(new Vector3(0.0f, 0.0f, -1.0f), accelerationSnapshot);


        //инвертируем значение осей
        calibrationQuaternion = Quaternion.Inverse(rotateQuaternion);
        //calibrationQuaternion = rotateQuaternion;
    }

    public Vector3 FixAcceleration(Vector3 acceleration)
    {
        //умножаем стартовое положение телефона на текущее и получаем текущее положение телефона с учетом калибровки
        return calibrationQuaternion * acceleration;
    }


    private void Update()
    {
        if (Input.GetButton("MyFire") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            //GameObject clone
            foreach(var shotSpawn in shotSpawns)
            {
                Instantiate(shot, shotSpawn.position, shotSpawn.rotation); //as GameObject
            }
            
            GetComponent<AudioSource>().Play();
        }

        //если нажата кнопка из набора Fire2(правая мыши или левый Alt) и по времени возможен выстрел
        if(Input.GetButton("Fire2") && Time.time > nextFire)
        {
            //указываем время для следующего выстрела
            nextFire = Time.time + fireRateMegaShot;

            //создается клон пребафа супер-выстрела
            Instantiate(megaShot, shotSpawns[0].position, shotSpawns[0].rotation);
            
            //звук запуска снаряда
            GetComponent<AudioSource>().Play();
        }
    }

    private void FixedUpdate()
    {
        //каждый кадр получаем положение телефона в пространстве
        Vector3 accelerationRaw = Input.acceleration;

        //получаем координаты текущего положения телефона относительно стартового положения
        Vector3 acceleration = FixAcceleration(accelerationRaw);

        //float moveHorizontal = Input.GetAxis("Horizontal");
        //float moveVertical = Input.GetAxis("Vertical");
        rigidbody.rotation = Quaternion.Euler(0f, 0f, rigidbody.velocity.x * -tilt);
        //rigidbody.velocity = new Vector3(moveHorizontal, 0, moveVertical) * speed;
        rigidbody.velocity = new Vector3(acceleration.x, 0, acceleration.y) * speed;
        rigidbody.position = new Vector3
            (
            Mathf.Clamp(rigidbody.position.x, boundary.xMin, boundary.xMax),
            0f,
            Mathf.Clamp(rigidbody.position.z, boundary.zMin, boundary.zMax)
            );
    }
}
