using UnityEngine;
using System.Collections;

public class Pusher : MonoBehaviour {
    public KeyCode forwardKey = KeyCode.N;
    public KeyCode leftKey = KeyCode.B;
    public KeyCode rightKey = KeyCode.M;
    public float damping = 0.1f;
    public float thrust = 100.0f; // Newtons
    public float mass = 1.0f;
    public Vector3 velocity;
    public Vector3 force;
    
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(forwardKey))
        {
            force += transform.forward * thrust;
        }

        if (Input.GetKey(leftKey))
        {
            force -= transform.right * thrust;
        }

        if (Input.GetKey(rightKey))
        {
            force += transform.right * thrust;
        }

        Vector3 acceleration = force / mass;
        velocity += acceleration * Time.deltaTime;
        transform.Translate(velocity * Time.deltaTime, Space.World);
        if (velocity.sqrMagnitude > float.Epsilon)
        {
            transform.forward = velocity;
        }
        float dampingThisFrame = 1.0f - (damping * Time.deltaTime);
        velocity *= dampingThisFrame;
        force = Vector3.zero;
    }
}
