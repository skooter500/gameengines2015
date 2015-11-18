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
    public static float Map(float value, float r1, float r2, float m1, float m2)
    {
        float dist = value - r1;
        float range1 = r2 - r1;
        float range2 = m2 - m1;
        return m1 + ((dist / range1) * range2);
    }
}
}

