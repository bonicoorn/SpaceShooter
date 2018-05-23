using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackground : MonoBehaviour {

    public float scrollSpeed;
    public float tileSize;
    Transform currentObject;
	// Use this for initialization
	void Start () {
        currentObject = GetComponent<Transform>();

		
	}
	
	// Update is called once per frame
	void Update () {
        currentObject.position = new Vector3(
            currentObject.position.x,
            currentObject.position.y,
            Mathf.Repeat(Time.time*scrollSpeed,tileSize)
            );
	}
}
