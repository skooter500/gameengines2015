using UnityEngine;
using System.Collections;

public class QuaternionSlerp : MonoBehaviour {

    public GameObject initialTarget;
    public GameObject endTarget;

    Quaternion fromQuaternion = Quaternion.identity;
    Quaternion toQuaternion = Quaternion.identity;
    bool slerping = false;
    float t = 0.0f;
    float toRotate;
    [Range(0.0f, 360.0f)]
    public float turnRateDegrees = 45.0f;

    // Use this for initialization
    void Start () {
        // Look at initialTarget
        Vector3 basis = Vector3.forward;
        Vector3 toTarget = initialTarget.transform.position - transform.position;
        toTarget.Normalize();

        // Calculate the axis
        Vector3 axis = Vector3.Cross(basis, toTarget);

        // Calculate the angle
        float angle = Mathf.Acos(Vector3.Dot(basis, toTarget));

        // Make the quaternion
        Quaternion q = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, axis);
        
        transform.rotation = q;

        // Alternatively, just call :-)
        // transform.LookAt(toTarget);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Space))
        {
            slerping = true;
            fromQuaternion = transform.rotation;

            // Make the to quaternion
            Vector3 basis = Vector3.forward;
            Vector3 toTarget = endTarget.transform.position - transform.position;
            toTarget.Normalize();
            // Calculate the axis
            Vector3 axis = Vector3.Cross(basis, toTarget);
            // Calculate the angle
            float angle = Mathf.Acos(Vector3.Dot(basis, toTarget));
            // Make the quaternion
            toQuaternion = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, axis);

            // The total angle we need to move
            toRotate = Mathf.Acos(Vector3.Dot(transform.forward, toTarget));
            
            t = 0.0f;
        }

        if (slerping)
        {
            Quaternion newRot = Quaternion.Slerp(
                fromQuaternion
                , toQuaternion
                , t
                );

            transform.rotation = newRot;

            float angleThisFrameRadians = turnRateDegrees * Mathf.Deg2Rad * Time.deltaTime;
            float tDelta = 1.0f / (toRotate / angleThisFrameRadians);
            t += tDelta;
            if (t >= 1.0f)
            {
                slerping = false;
            }
        }
	}
}
