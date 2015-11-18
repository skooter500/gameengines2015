using UnityEngine;
using System.Collections;

public class CollisionTriggerTest : MonoBehaviour {

    int i = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision other)
    {
        Debug.Log((i++) + " OnCollisionEnter: " + other.gameObject);
    }

    void OnCollisionExit(Collision other)
    {
        Debug.Log((i++) + " OnCollisionEnter: " + other.gameObject);
    }

    void OnCollisionStay(Collision other)
    {
        Debug.Log((i++) + " OnCollisionEnter: " + other.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log((i++) + " OnTriggerEnter: " + other.gameObject);
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log((i++) + " OnTriggerExit:" + other.gameObject);
    }

    void OnTriggerStay(Collider other)
    {
        Debug.Log((i++) + " OnTriggerStay:" + other.gameObject);
    }
}
