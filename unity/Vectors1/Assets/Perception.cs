using UnityEngine;
using System.Collections;

public class Perception : MonoBehaviour {

    public GameObject ferdelance;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 toFerdelance = ferdelance.transform.position - transform.position;
        toFerdelance.Normalize();
        float dot = Vector3.Dot(transform.forward, toFerdelance);

        if (dot < 0)
        {
            GameManager.PrintMessage("Behind");
        }
        else
        {
            GameManager.PrintMessage("In front");

        }
    }
}
