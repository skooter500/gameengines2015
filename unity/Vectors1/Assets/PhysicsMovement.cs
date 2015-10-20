using UnityEngine;
using System.Collections;

public class PhysicsMovement : MonoBehaviour {
    public KeyCode left = KeyCode.LeftArrow;
    public KeyCode right = KeyCode.RightArrow;
    public KeyCode forward = KeyCode.UpArrow;
    public KeyCode backward = KeyCode.DownArrow;

    [HideInInspector]
    public Vector3 velocity;

    [HideInInspector]
    public Vector3 force;
    public float mass = 1.0f;

    public float thrust = 10.0f;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(forward))
        {
            force += transform.forward * thrust;
        }
        if (Input.GetKey(backward))
        {
            force -= transform.forward * thrust;
        }
        if (Input.GetKey(left))
        {
            force -= transform.right * thrust;
        }

        if (Input.GetKey(right))
        {
            force += transform.right * thrust;
        }

        Vector3 acceleration = force / mass;
        velocity += acceleration * Time.deltaTime;

        Vector3 pos = transform.position;
        pos += velocity * Time.deltaTime;
        transform.position = pos;

        if (velocity.sqrMagnitude > float.Epsilon)
        {
            transform.forward = velocity;
        }

        velocity *= 0.99f;

        force = Vector3.zero;
    }
}
