using UnityEngine;
using System.Collections.Generic;

public class Guard : MonoBehaviour {

    public List<Vector3> waypoints = new List<Vector3>();
    public bool randomPath = true;
    public float radius = 100;
    public float speed = 0.5f;
    int currentWaypoint = 0;

    public GameObject player;
    public float fov = 45.0f;
    public float range = 20.0f;

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
            if (dist < 0.1f)
            {
                currentWaypoint = (currentWaypoint + 1) % waypoints.Count;
            }
            transform.LookAt(waypoints[currentWaypoint]);
            transform.Translate(0, 0, speed * Time.deltaTime);
        }
        else
        {
            float dist = Vector3.Distance(transform.position, player.transform.position);
            if (dist > 5.0f)
            {
                transform.LookAt(player.transform.position);
                transform.Translate(0, 0, speed * Time.deltaTime);
            }
        }
    }
}
