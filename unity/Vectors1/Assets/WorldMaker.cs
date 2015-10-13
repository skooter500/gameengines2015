using UnityEngine;
using System.Collections;

public class WorldMaker : MonoBehaviour {

    public GameObject cube;
    // Use this for initialization
    void Start () {

        Vector3 a = new Vector3(8, 10, 0);
        Vector3 b = new Vector3(12, 5, 0);

        float f = Vector3.Dot(a, b) / (a.magnitude * b.magnitude);
        float theta = Mathf.Rad2Deg * Mathf.Acos(f);
        Debug.Log("Theta: " + theta);

        float theta1 = Mathf.Atan(a.x / a.y);
        float theta2 = Mathf.Atan(b.y / b.x);

        float theta3 = Mathf.Rad2Deg *  ((Mathf.PI * 0.5f) - (theta1 + theta2));
        Debug.Log("Theta3: " + theta3);



        /*
        for (int x = -10; x < 10; x++)
        {
            GameObject newCube = GameObject.Instantiate(cube);
            Vector3 p = new Vector3(x, 0, 0);
            newCube.transform.position = p;

            Color c = new Color(
                Random.Range(0.0f, 1.0f),
                Random.Range(0.0f, 1.0f),
                Random.Range(0.0f, 1.0f)
                );

            newCube.GetComponent<Renderer>().material.color = c;
        }
        */
    }

    // Update is called once per frame
    void Update () {
	
	}
}
