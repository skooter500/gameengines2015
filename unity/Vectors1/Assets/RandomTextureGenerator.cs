using UnityEngine;
using System.Collections;

public class RandomTextureGenerator:TextureGenerator
{
    [HideInInspector]
    public Texture2D texture;

    NoiseForm noiseForm;
    // Use this for initialization

    public override Texture2D GenerateTexture()
    {
        int width = (int)noiseForm.cellsPerTile.x;
        int height = (int)noiseForm.cellsPerTile.y;

        Texture2D texture = new Texture2D(width, height, TextureFormat.RGBAFloat, false);
        texture.filterMode = FilterMode.Point;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                texture.SetPixel(x, y, RandomColor());
            }
        }

        texture.Apply();
        return texture;
    }

    public static Color RandomColor()
    {
        return new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
    }

    
    void Start()
    {
        noiseForm = GetComponent<NoiseForm>();
        if (noiseForm == null)
        {
            Debug.LogError("RandomTextureGenerator with no NoiseForm");
        }
    }
}
