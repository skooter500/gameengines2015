using UnityEngine;
using System.Collections.Generic;

public class Fountain : MonoBehaviour {

    public int particleCount = 100;
    public float startVelocityMin = 100;
    public float startVelocityMax = 100;

    List<GameObject> spheres = new List<GameObject>();

    private Color GetColor()
    {
        return new Color(Random.Range(0.0f, 1.0f), 0, Random.Range(0.0f, 1.0f));
    }

    // Use this for initialization
    void Start()
    {
        StartCoroutine("SpawnParticles");
    }

    System.Collections.IEnumerator SpawnParticles()
    {
        // Create the particles
        for (int i = 0; i < particleCount; i++)
        {
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            SphereParticle sphereParticle = sphere.AddComponent<SphereParticle>();
            sphereParticle.velocity = Random.insideUnitSphere * Random.Range(startVelocityMin, startVelocityMax);
            sphereParticle.velocity.y = Mathf.Abs(sphereParticle.velocity.y);
            sphere.GetComponent<Renderer>().material.color = GetColor();

            yield return new WaitForSeconds(0.1f);
        }
    }
    

// Update is called once per frame
void Update () {
	
	}
}
