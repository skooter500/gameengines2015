using UnityEngine;
using System.Collections;

public class QuaternionTest : MonoBehaviour {

    public GameObject other;
    public float rotSpeed = 90.0f;

    Quaternion from;
    Quaternion to;
    float t;

    // Use this for initialization
    void Start () {

        Vector3 toOther = other.transform.position - transform.position;
        toOther.Normalize();
        Vector3 axis = Vector3.Cross(Vector3.forward, toOther);

        float angle = Mathf.Acos(Vector3.Dot(Vector3.forward, toOther));
        to = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, axis);
        
        from = transform.rotation;
        t = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (t < 1.0f)
        {
            Quaternion q = Quaternion.Slerp(from, to, t);
            t += Time.deltaTime;
            transform.rotation = q;
        }
	}
}
