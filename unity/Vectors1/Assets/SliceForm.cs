using UnityEngine;
using System.Collections;

public class SliceForm : MonoBehaviour {

    public Vector3 size; 
    public Vector2 sliceCount; // The number of slices on each axis
    public Vector2 noiseStart;
    public Vector2 noiseDelta;
    public Color horizontalColour;
    public Color verticalColour;
    public bool closed;
    [Range(0, 1)]
    public float noiseToBase;

    Vector2 sliceSize; // The size of each slice

    Vector3[] initialVertices;
    Vector3[] initialNormals;
    Vector2[] meshUv;
    Color[] colours;
    int[] meshTriangles;
    Vector2[] uvSeqHoriz = new Vector2[] { new Vector2(0, 0), new Vector2(0, 1), new Vector2(0.1f, 1)
                                      , new Vector2(0.1f, 1), new Vector2(0.1f, 0), new Vector2(0, 0)
                                };
    Vector2[] uvSeqVert = new Vector2[] { new Vector2(0.9f, 0), new Vector2(1, 1), new Vector2(1, 1)
                                       , new Vector2(1, 1), new Vector2(1, 0), new Vector2(0.9f, 0)
                                };
    
    float baseHeight, noiseHeight;

    [HideInInspector]
    public float maxY;

    private bool generated = false;

    public static Color HexToColor(string hex)
    {
        hex = hex.Replace("0x", "");//in case the string is formatted 0xFFFFFF
        hex = hex.Replace("#", "");//in case the string is formatted #FFFFFF
        byte a = 255;//assume fully visible unless specified in hex
        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        //Only use alpha if the string has enough characters
        if (hex.Length == 8)
        {
            a = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        }
        return new Color32(r, g, b, a);
    }
    
    public SliceForm()
    {
        size = new Vector3(100, 100, 100);
        sliceCount = new Vector3(10, 10);
        noiseStart = new Vector2(0, 0);
        noiseDelta = new Vector2(0.1f, 0.1f);

        horizontalColour = HexToColor("0x2A59AD");
        verticalColour = HexToColor("0xFFB429");

        closed = true;
        noiseToBase = 0.2f;
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, size);
    }

    public Texture2D CreateTexture()
    {
        /*
        Texture2D texture = new Texture2D(2, 1, TextureFormat.RGBAFloat, false);
        texture.filterMode = FilterMode.Point;

        texture.SetPixel(0, 0, Color.red);
        texture.SetPixel(1, 0, Color.green);
        */
        
        int width = 1;
        int height = 1;

        Texture2D texture = new Texture2D(width, height, TextureFormat.RGBAFloat, false);
        texture.filterMode = FilterMode.Point;

        Color c = horizontalColour;
        horizontalColour.a = 0.5f;

        for (int y = 0 ; y < height ; y ++)
        {
            for (int x = 0 ; x < width ; x ++)
            {
                texture.SetPixel(x, y, c);
                //texture.SetPixel(x, y, (x < width / 2) ? horizontalColour : verticalColour);
            }
        }
        
        texture.Apply();
        return texture;
    }

    void MaxY(float y)
    {
        if (y > maxY)
        {
            maxY = y;
        }
    }

    public void Generate()
    {
        if (generated)
        {
            return;
        }
        sliceSize = new Vector2(size.x / sliceCount.x, size.z / sliceCount.y);

        noiseHeight = size.y * noiseToBase;
        baseHeight = size.y - noiseHeight;

        MeshRenderer renderer = gameObject.AddComponent<MeshRenderer>();
        renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        renderer.receiveShadows = true;
        if (renderer == null)
        {
            Debug.Log("Renderer is null 1");
        }

        Mesh mesh = gameObject.AddComponent<MeshFilter>().mesh;
        mesh.Clear();


        int verticesPerSegment = 24;

        int verticesPerHorizontalSlice = verticesPerSegment * (int)sliceCount.x;
        int vertexCount = 0;
        if (closed)
        {
            // Go one extra slice horizontally and vertically
            sliceCount.x++;
            sliceCount.y++;
        }
        vertexCount = verticesPerSegment * ((int)sliceCount.x) * ((int)sliceCount.y);
        // Reduce by one vertical slice and one horizontal slice
        vertexCount -= (verticesPerSegment / 2) * (int)sliceCount.x;
        vertexCount -= (verticesPerSegment / 2) * (int)sliceCount.y;

        initialVertices = new Vector3[vertexCount];
        initialNormals = new Vector3[vertexCount];
        meshUv = new Vector2[vertexCount];
        meshTriangles = new int[vertexCount];
        colours = new Color[vertexCount];

        Vector3 bottomLeft = -(size / 2);

        Vector2 noiseXY = noiseStart;
        int vertex = 0;
        float seam = 0.1f;

        for (int y = 0; y < sliceCount.y; y++)
        {
            noiseXY.x = noiseStart.x;
            for (int x = 0; x < sliceCount.x; x++)
            {

                int startVertex = vertex;
                // Make the horizontal slice
                if ((!closed && y == 0) || (closed && x == sliceCount.x - 1))
                {
                    // Skip a slice
                }
                else
                {
                    // Calculate some stuff
                    Vector3 sliceBottomLeft = bottomLeft + new Vector3(x * sliceSize.x, 0, y * sliceSize.y);
                    Vector3 sliceTopLeft = sliceBottomLeft + new Vector3(0, baseHeight + (Mathf.PerlinNoise(noiseXY.x, noiseXY.y) * noiseHeight));
                    Vector3 sliceTopRight = sliceBottomLeft + new Vector3(sliceSize.x, baseHeight + (Mathf.PerlinNoise(noiseXY.x + noiseDelta.x, noiseXY.y) * noiseHeight));
                    Vector3 sliceBottomRight = sliceBottomLeft + new Vector3(sliceSize.x, 0, 0);
                    MaxY(sliceTopLeft.y); MaxY(sliceTopRight.y);
                    if (x == 0)
                    {
                        sliceBottomLeft.x += seam;
                        sliceTopLeft.x += seam;
                    }
                    if (x == sliceCount.x - 2)
                    {
                        sliceBottomRight.x -= seam;
                        sliceTopRight.x -= seam;
                    }
                    // Make the vertices
                    initialVertices[vertex++] = sliceBottomLeft;
                    initialVertices[vertex++] = sliceTopLeft;
                    initialVertices[vertex++] = sliceTopRight;
                    initialVertices[vertex++] = sliceTopRight;
                    initialVertices[vertex++] = sliceBottomRight;
                    initialVertices[vertex++] = sliceBottomLeft;

                    // Back face
                    initialVertices[vertex++] = sliceTopRight;
                    initialVertices[vertex++] = sliceTopLeft;
                    initialVertices[vertex++] = sliceBottomLeft;
                    initialVertices[vertex++] = sliceBottomLeft;
                    initialVertices[vertex++] = sliceBottomRight;
                    initialVertices[vertex++] = sliceTopRight;

                    // Make the normals, UV's and triangles                
                    for (int i = 0; i < 12; i++)
                    {
                        initialNormals[startVertex + i] = (i < 6) ? Vector3.forward : -Vector3.forward;
                        meshUv[startVertex + i] = uvSeqHoriz[i % 6];
                        meshTriangles[startVertex + i] = startVertex + i;
                        colours[startVertex + i] = Color.green;
                    }
                }

                if ((!closed && x == 0) || (closed && y == sliceCount.y - 1))
                {
                    // Dont do a vertical slice
                }
                else
                {
                    startVertex = vertex;
                    // Make the vertical slice
                    Vector3 sliceBottomLeft = bottomLeft + new Vector3(x * sliceSize.x, 0, y * sliceSize.y);
                    Vector3 sliceTopLeft = sliceBottomLeft + new Vector3(0, baseHeight + (Mathf.PerlinNoise(noiseXY.x, noiseXY.y) * noiseHeight));
                    Vector3 sliceBottomForward = sliceBottomLeft + new Vector3(0, 0, sliceSize.y);
                    Vector3 sliceTopForward = sliceBottomLeft + new Vector3(0, baseHeight + (Mathf.PerlinNoise(noiseXY.x, noiseXY.y + noiseDelta.y) * noiseHeight), sliceSize.y);
                    MaxY(sliceTopLeft.y); MaxY(sliceTopForward.y);
                    if (y == 0)
                    {
                        sliceBottomLeft.z += seam;
                        sliceTopLeft.z += seam;
                    }
                    if (y == sliceCount.y - 2)
                    {
                        sliceBottomForward.z -= seam;
                        sliceTopForward.z -= seam;
                    }

                    initialVertices[vertex++] = sliceBottomLeft;
                    initialVertices[vertex++] = sliceTopLeft;
                    initialVertices[vertex++] = sliceTopForward;

                    initialVertices[vertex++] = sliceTopForward;
                    initialVertices[vertex++] = sliceBottomForward;
                    initialVertices[vertex++] = sliceBottomLeft;

                    // Back face
                    initialVertices[vertex++] = sliceTopForward;
                    initialVertices[vertex++] = sliceTopLeft;
                    initialVertices[vertex++] = sliceBottomLeft;

                    initialVertices[vertex++] = sliceBottomLeft;
                    initialVertices[vertex++] = sliceBottomForward;
                    initialVertices[vertex++] = sliceTopForward;

                    // Make the normals, UV's and triangles                
                    for (int i = 0; i < 12; i++)
                    {
                        initialNormals[startVertex + i] = (i < 6) ? Vector3.right : -Vector3.right;
                        meshUv[startVertex + i] = uvSeqVert[i % 6];
                        meshTriangles[startVertex + i] = startVertex + i;
                        colours[startVertex + i] = Color.red;
                    }
                }
                noiseXY.x += noiseDelta.x;
            }
            noiseXY.y += noiseDelta.y;
        }


        mesh.vertices = initialVertices;
        mesh.uv = meshUv;
        mesh.normals = initialNormals;
        mesh.triangles = meshTriangles;
        mesh.colors = colours;

        //mesh.RecalculateNormals();

        renderer.material.color = horizontalColour;

        //Shader shader = Shader.Find("Diffuse");
        //Material material = new Material(shader);
        //material.color = horizontalColour;
        ////material.mainTexture = CreateTexture(); 
        //if (renderer == null)
        //{
        //    Debug.Log("Renderer is null 2");
        //}
        //else
        //{
        //    renderer.material = material;
        //}

        generated = true;
    }
	
	void Start () {

        Generate();
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
