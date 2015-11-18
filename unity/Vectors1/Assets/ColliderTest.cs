using UnityEngine;
using System.Collections;

public class ColliderTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject);
        Rigidbody r = other.gameObject.GetComponent<Rigidbody>();
        r.AddForce(Vector3.up * 1000);

    }

    void OnTriggerStay(Collider other)
    {
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log(other.gameObject);
    }

}
