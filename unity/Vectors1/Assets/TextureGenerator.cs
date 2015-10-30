using UnityEngine;
using System.Collections;

public abstract class TextureGenerator : MonoBehaviour
{
    [HideInInspector]
    public Texture2D texture;

    public abstract Texture2D GenerateTexture();    
}
