using UnityEngine;
using System.Collections;

public class PhysicsOutput : MonoBehaviour {
    Rigidbody body;
	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        GameManager.PrintVector("Inertial Tensor: ", body.inertiaTensor);
        GameManager.PrintVector("Position: ", transform.position);
        GameManager.PrintVector("Velocity: ", body.velocity);
        GameManager.PrintVector("Angular Velocity: ", body.angularVelocity);
    }
}
