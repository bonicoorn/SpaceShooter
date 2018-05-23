using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvasiveManeuver : MonoBehaviour {

    public Vector2 startWait;//время до появления корабля
    public Vector2 maneuverTime;//время на маневр
    public Vector2 maneuverWait;//время между маневрами

    public float dodge;//макс расстояние маневра
    public float maneuverSpeed;//скорость маневра
    public float tilt;//градус поворота корабля

    float targetManeuver;
    float currentSpeed;

    public Boundary boundary;//границы
    

    private void Start()
    {
        currentSpeed = GetComponent<Rigidbody>().velocity.z;
        StartCoroutine(Evade());
    }

    private IEnumerator Evade()
    {
        yield return new WaitForSeconds(Random.Range(startWait.x,startWait.y));

        while (true)
        {
            targetManeuver = Random.Range(1, dodge) * -Mathf.Sign(transform.position.x);

            yield return new WaitForSeconds(Random.Range(maneuverTime.x,maneuverTime.y));

            targetManeuver = 0;

            yield return new WaitForSeconds(Random.Range(maneuverWait.x, maneuverWait.y));
        }
    }

    private void FixedUpdate()
    {
        float newManeuver = Mathf.MoveTowards
            (
                GetComponent<Rigidbody>().velocity.x,
                targetManeuver,
                maneuverSpeed*Time.deltaTime
            );

        GetComponent<Rigidbody>().velocity = new Vector3(newManeuver, 0.0f, currentSpeed);

        GetComponent<Rigidbody>().position = new Vector3
            (
                Mathf.Clamp(GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax),
                0.0f,
                Mathf.Clamp(GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
            );

        GetComponent<Rigidbody>().rotation = Quaternion.Euler(0, 0, GetComponent<Rigidbody>().velocity.x * -tilt);
    }
}
