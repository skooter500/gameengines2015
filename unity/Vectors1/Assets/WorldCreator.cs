using UnityEngine;
using System.Collections;

public class WorldCreator : MonoBehaviour {

    public GameObject cube;
    public int width = 10;
    public int depth = 10;
    public int height = 10;
	// Use this for initialization
	void Start () {

        int halfWidth = width / 2;
        int halfDepth = depth / 2;
        float theta = Mathf.PI / width;
        // Make a mountain
        for (int x = 0; x <= width; x++)
        {
            for (int z = 0; z <= depth; z++)
            {
                float y = Mathf.Round(1 + Mathf.Sin(x * theta) * Mathf.Sin(z * theta) * height);
                
                for (int tower = 0; tower < y; tower++)
                {
                    GameObject newCube = GameObject.Instantiate<GameObject>(cube);
                    Vector3 p = newCube.transform.position;
                    p.x = x - halfWidth;
                    p.y = tower;
                    p.z = z - halfDepth;
                    newCube.transform.position = p;
                    newCube.transform.parent = transform;
                }                
            }
        }


        /*
        // Make a stairs
        for (int x = -halfWidth; x < halfWidth; x++)
        {
            for (int z = -halfDepth; z < halfDepth; z ++)
            {
                if (cube == null)
                {
                    Debug.Log("Not suppposed");
                }
                GameObject newCube = GameObject.Instantiate<GameObject>(cube);
                Vector3 p = newCube.transform.position;
                p.x = x;
                p.y = x + z;
                p.z = z;
                newCube.transform.position = p;
            }
        } 
        */
    }

        // Update is called once per frame
        void Update () {
        transform.Rotate(Vector3.up, Time.deltaTime);
	}
}
