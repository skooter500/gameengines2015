using UnityEngine;
using System.Collections.Generic;


public class PhysicsGuard : MonoBehaviour {

    public List<Vector3> waypoints = new List<Vector3>();
    public bool randomPath = true;
    public float radius = 100;
    public float speed = 0.5f;
    int currentWaypoint = 0;

    public GameObject player;
    public float fov = 45.0f;
    public float range = 20.0f;

    public Vector3 velocity;
    public Vector3 force;

    public float mass = 1.0f;
    public float maxSpeed = 1.0f;

    // Use this for initialization
    void Start () {
        if (randomPath)
        {
            waypoints.Add(transform.position);
            for (int i = 0; i < 9; i++)
            {
                Vector3 waypoint = Random.insideUnitSphere * radius;
                waypoint.y = 0;

                waypoints.Add(waypoint);
            }
        }
	}

    void OnDrawGizmos()
    {
        // Draw the path
        for (int i = 1; i <= waypoints.Count; i++)
        {
            Vector3 prev = waypoints[i - 1];
            Vector3 next = waypoints[i % waypoints.Count];
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(prev, next);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(next, 0.1f);
        }
    }
	
	// Update is called once per frame
	void Update () {
        bool canSee = false;
        // Perceive the player
        float pDist = Vector3.Distance(transform.position, player.transform.position);
        if (pDist < range)
        {
            GameManager.PrintMessage("In Range!");

            Vector3 toPlayer = player.transform.position - transform.position;
            toPlayer.Normalize();
            float dot = Mathf.Clamp(Vector3.Dot(toPlayer, transform.forward), -1.0f, 1.0f);
            float angleToPlayer = Mathf.Acos(dot) * Mathf.Rad2Deg;
            GameManager.PrintMessage("Angle to player: " + (int)angleToPlayer);
            if (angleToPlayer < fov)
            {
                GameManager.PrintMessage("I can see you!");
                canSee = true;
            }
        }

        // Follow the path
        if (!canSee)
        {
            GameManager.PrintMessage("Where are you!");
            float dist = Vector3.Distance(transform.position, waypoints[currentWaypoint]);
            if (dist < 1.1f)
            {
                currentWaypoint = (currentWaypoint + 1) % waypoints.Count;
            }

            Vector3 desired = waypoints[currentWaypoint] - transform.position;
            desired.Normalize();
            desired *= maxSpeed;
            force = desired;
        }
        else
        {
            float dist = Vector3.Distance(transform.position, player.transform.position);
            if (dist > 5.0f)
            {
                Vector3 desired = player.transform.position - transform.position;
                desired.Normalize();
                desired *= maxSpeed;
                force = (desired - velocity);
            }
        }
        Vector3 acceleration = force / mass;
        velocity += acceleration * Time.deltaTime;
        transform.Translate(velocity * Time.deltaTime, Space.World);
        if (velocity.sqrMagnitude > float.Epsilon)
        {
            transform.forward = velocity;
        }
        velocity *= 0.99f;
        force = Vector3.zero;
    }
}
