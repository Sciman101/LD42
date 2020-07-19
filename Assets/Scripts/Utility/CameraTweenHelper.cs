using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTweenHelper : MonoBehaviour {

    public Vector3 start, end;
    public float speed, delay;

    bool animate = false;

	void Start () {
        transform.position = start;
        Invoke("Begin", delay);
	}
	
	void Update () {
        if (animate) transform.position = Vector3.Lerp(transform.position, end, Time.deltaTime * speed);
	}

    void Begin()
    {
        animate = true;
    }
}
