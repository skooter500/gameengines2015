using UnityEngine;
using System.Collections;


public class PerlinNoiseSampler:Sampler
{
    public Vector2 origin;
    public Vector2 scale;
    public float height;

    public PerlinNoiseSampler()
    {
        origin = new Vector2(0, 0);
        scale = new Vector2(0.2f, 0.2f);
    }

    public override float Sample(float x, float y)
    {
        float sample = Mathf.PerlinNoise(origin.x + (x * scale.x), origin.y + (y * scale.y)) * height;
        return sample;
    }
}
