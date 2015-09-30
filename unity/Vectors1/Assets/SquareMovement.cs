using UnityEngine;
using System.Collections;

public class SquareMovement : MonoBehaviour {

    public float distance = 20;
    float travelled;
	// Use this for initialization
	void Start ()
    {
        travelled = 0;
    }
	
	// Update is called once per frame
	void Update () {
        float speed = 20.0f;
        float off = speed * Time.deltaTime;
        travelled += off;    
        if (travelled > distance)
        {
            travelled = 0.0f;
            transform.Rotate(Vector3.up, 90.0f);
        }
        transform.Translate(0, 0, off);
    }
}
