using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

    public KeyCode forwardKey;
    public KeyCode leftKey;
    public KeyCode rightKey;

    public float speed = 10;
    public float rotSpeedDegrees = 90;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(forwardKey))
        {
            transform.Translate(0, 0, speed * Time.deltaTime);
        }

        if (Input.GetKey(leftKey))
        {
            transform.Rotate(Vector3.up, -rotSpeedDegrees * Time.deltaTime);
        }

        if (Input.GetKey(rightKey))
        {
            transform.Rotate(Vector3.up, rotSpeedDegrees * Time.deltaTime);
        }
    }
}

