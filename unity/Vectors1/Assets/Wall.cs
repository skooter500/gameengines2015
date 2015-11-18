using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour {

	// Use this for initialization
	void Start () {
        CreateWall(10, 10);
	}

    Color RandomColor()
    {
        return new Color(
            Random.Range(0.0f, 1.0f)
            , Random.Range(0.0f, 1.0f)
            , Random.Range(0.0f, 1.0f)
            );
    }

    void CreateWall(float width, float height)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.Translate(x, 2 + (y * 2), 0);
                cube.GetComponent<Renderer>().material.color = RandomColor();
                Rigidbody r = cube.AddComponent<Rigidbody>();
                r.mass = -1;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
