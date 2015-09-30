using UnityEngine;
using System.Collections;

public class SimpleMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {

        float speed = 10.0f;
        float aSpeed = 90.0f;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(0, 0, speed * Time.deltaTime);
            
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(0, 0, - speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.up, - aSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.up, aSpeed * Time.deltaTime);
        }


    }
}
