using UnityEngine;
using System.Collections;

public class Harmonic : MonoBehaviour {
    public float range = 20;
	// Use this for initialization
	void Start () {
	}
    float theta = 0.0f;
	// Update is called once per frame
	void Update () {
        float angle = range * Mathf.Sin(theta);
        theta += Time.deltaTime;
        Debug.Log(theta);
        Debug.Log(Mathf.Sin(theta));
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = q;
	}
}
