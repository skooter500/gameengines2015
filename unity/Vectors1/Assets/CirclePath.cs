using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(PathFollower))]
public class CirclePath : MonoBehaviour {
    public float radius = 50;
    public float thetaOffset = 0.0f;
    public int numberOfWaypoints = 12;
    public float height;
    private PathFollower pathFollower;
    
    // Use this for initialization
    void Start () {
        pathFollower = GetComponent<PathFollower>();

        float thetaInc = Mathf.PI * 2.0f / (float)numberOfWaypoints;
        for (int i = 0; i < numberOfWaypoints; i++)
        {
            float theta = thetaOffset + (thetaInc * i);
            Vector3 pos = new Vector3();
            pos.x = transform.position.x + (Mathf.Sin(theta) * radius);
            pos.z = transform.position.z + (Mathf.Cos(theta) * radius);
            pathFollower.waypoints.Add(pos);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
