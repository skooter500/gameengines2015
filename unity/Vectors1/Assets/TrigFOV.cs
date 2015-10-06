using UnityEngine;
using System.Collections;

public class TrigFOV : MonoBehaviour {
    GameObject target;
    GameObject player;

    // Use this for initialization
    void Start() {
        target = GameObject.FindGameObjectWithTag("target");
        player = GameObject.FindGameObjectWithTag("player");


    }

    // Update is called once per frame
    void Update() {
        /*float opposite = target.transform.position.x - transform.position.x;
        float adj = target.transform.position.z - transform.position.z;
        float theta = Mathf.Atan(opposite / adj);
        //transform.Rotate(Vector3.up, theta * Mathf.Rad2Deg, Space.World);

        transform.rotation = Quaternion.AngleAxis(theta * Mathf.Rad2Deg, Vector3.up);
        */
        transform.LookAt(target.transform);

        float ot = target.transform.position.x - transform.position.x;
        float at = target.transform.position.z - transform.position.z;
        float thetaToTarget = Mathf.Atan2(ot, at);

        float op = player.transform.position.x - transform.position.x;
        float ap = player.transform.position.z - transform.position.z;
        float thetaToPlayer = Mathf.Atan2(op, ap);
        float fovRads = 45.0f * Mathf.Deg2Rad;
        if (thetaToPlayer + fovRads > thetaToPlayer)
        {
            GameManager.PrintMessage("Inside FOV!");
        }
        else
        {
            GameManager.PrintMessage("Outside FOV!");
        }

        /// Some code!!



    }
}
