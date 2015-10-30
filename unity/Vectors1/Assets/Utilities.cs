using UnityEngine;
using System.Collections;


public class Utilities
{
    public static Color RandomColor()
    {
        return new Color(Random.Range(0.0f, 1.0f)
            , Random.Range(0.0f, 1.0f)
            , Random.Range(0.0f, 1.0f)
            );
    }
}

