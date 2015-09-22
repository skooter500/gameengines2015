using UnityEngine;
using System.Collections;

public class CubeMove : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        // Move the cube on the x axis
        Vector3 pos = transform.position;
        pos.x += 0.1f;
        transform.position = pos;
	}
}
