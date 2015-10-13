using UnityEngine;
using System.Collections;

public class ColorLerper : MonoBehaviour {
    Color startColor;
    Color endColor;
    float t = 0;
    Color RandomColor()
    {
        return new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
    }

	// Use this for initialization
	void Start () {
        startColor = RandomColor();
        endColor = RandomColor();
	}
	
	// Update is called once per frame
	void Update () {
        
        GetComponent<Renderer>().material.color = Color.Lerp(startColor, endColor, t);
        t += Time.deltaTime;
        if (t > 1.0f)
        {
            t = 0;
            startColor = endColor;
            endColor = RandomColor();
        }
    }
}
