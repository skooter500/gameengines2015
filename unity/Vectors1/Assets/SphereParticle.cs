using UnityEngine;
using System.Collections.Generic;

public class SphereParticle:MonoBehaviour
{
    public Vector3 velocity = Vector3.zero;
    public Vector3 gravity = new Vector3(0, -9, 0);
    public float alive = 0.0f;

    public void Update()
    {
        velocity += gravity * Time.deltaTime;
        transform.Translate(velocity * Time.deltaTime, Space.World);
        if (transform.position.y < 0.5f)
        {
            velocity.y = -velocity.y;
            Vector3 pos = transform.position;
            pos.y = 0.5f;
            transform.position = pos;
            alive += Time.deltaTime;
        }
    }
}