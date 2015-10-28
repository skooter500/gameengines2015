using UnityEngine;
using System.Collections;

public abstract class Sampler : MonoBehaviour
{    
    public Sampler()
    {
    }

    public abstract float Sample(float x, float y);
}
