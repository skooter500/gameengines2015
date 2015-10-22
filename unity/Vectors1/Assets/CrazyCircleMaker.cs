using UnityEngine;
using System.Collections;

public class CrazyCircleMaker : MonoBehaviour {

    public GameObject prefab;
    public int count = 12;
	// Use this for initialization
	void Start () {
        float thetaInc = (Mathf.PI * 2.0f) / count;
        for (int i = 0; i < count; i++)
        {
            GameObject newObject = GameObject.Instantiate<GameObject>(prefab);
            newObject.GetComponent<CirclePath>().thetaOffset = (i * thetaInc);
            newObject.transform.Translate(0, i, 0);
        } 
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
