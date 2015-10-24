using UnityEngine;
using System.Collections;

class GeneratedMesh
{
    public Vector3[] initialVertices;
    public Vector3[] initialNormals;
    public Vector2[] meshUv;
    public Color[] colours;
    public int[] meshTriangles;
}


public class ProcGen : MonoBehaviour {

    Mesh mesh;
    MeshRenderer renderer;
    MeshCollider collider;

    GeneratedMesh gm = new GeneratedMesh();

    public Vector2 samples = new Vector2(20, 20);
    public float amplitude = 10.0f;

    private Texture2D texture;

    System.Collections.IEnumerator DropShape()
    {
        while (true)
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.Translate(0, 20, 0);
            cube.AddComponent<Rigidbody>();
            cube.GetComponent<Renderer>().material.color = RandomColor();
            cube.GetComponent<Collider>().isTrigger = false;
            cube.transform.localScale = new Vector3(Random.Range(1.0f, 5.0f)
                , Random.Range(1.0f, 5.0f)
                , Random.Range(1.0f, 5.0f)
                );
            yield return new WaitForSeconds(2.0f);
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3 size = new Vector3(samples.x * transform.localScale.x
            , amplitude * transform.localScale.y
            , samples.y * transform.localScale.z);
        Gizmos.DrawWireCube(transform.position, size);
    }

	// Use this for initialization
	void Start () {
        renderer = gameObject.AddComponent<MeshRenderer>();
        renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        renderer.receiveShadows = true;
        mesh = gameObject.AddComponent<MeshFilter>().mesh;
        mesh.Clear();
        collider = gameObject.AddComponent<MeshCollider>();


        texture = new Texture2D((int) samples.x, (int) samples.y, TextureFormat.RGBAFloat, false);
        texture.filterMode = FilterMode.Point;
        
        GenerateMesh();
        GenerateTexture();

        renderer.material.SetTexture(0, texture);

        StartCoroutine("DropShape");


    }



    public static float Map(float value, float r1, float r2, float m1, float m2)
    {
        float dist = value - r1;
        float range1 = r2 - r1;
        float range2 = m2 - m1;
        return m1 + ((dist / range1) * range2);
    }

    float SampleCell(float x, float y)
    {
        float ret;
        /*
        ret = -Mathf.Sin(Map(x, 0, samples.x, 0, Mathf.PI))
            * Mathf.Sin(Map(y, 0, samples.y, 0, Mathf.PI));
        */
        ret = Mathf.PerlinNoise(Map(x, 0, samples.x, 0, 5.0f)
                , Map(y, 0, samples.y, 0, 5.0f));
        return ret;
    }

    Color RandomColor()
    {
        return new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
    }
    
    void GenerateTexture()
    {
        for (int y = 0; y < samples.y; y++)
        {
            for (int x = 0; x < samples.x; x++)
            {
                //texture.SetPixel(x, y, Color.blue);
                // texture.SetPixel(x, y, Color.blue * SampleCell(x, y));

                // texture.SetPixel(x, y, Color.white * (1.0f - SampleCell(x, y)));
                texture.SetPixel(x, y, RandomColor());
                
            }
        }
        texture.Apply();
    }

    void GenerateMesh()
    {

        int verticesPerSample = 6;
        int vertexCount = verticesPerSample * ((int)samples.x) * ((int)samples.y);
        
        gm.initialVertices = new Vector3[vertexCount];
        gm.initialNormals = new Vector3[vertexCount];
        gm.meshUv = new Vector2[vertexCount];
        gm.meshTriangles = new int[vertexCount];
        gm.colours = new Color[vertexCount];

        Vector3 bottomLeft = new Vector3(-samples.x / 2, 0, -samples.y / 2);

        int vertex = 0;
        for (int y = 0; y < samples.y; y++)
        {
            for (int x = 0; x < samples.x; x++)
            {                
                Vector3 sliceBottomLeft = bottomLeft + new Vector3(x, 0, y);
                Vector3 sliceTopLeft = bottomLeft + new Vector3(x, 0, y + 1);
                Vector3 sliceTopRight = bottomLeft + new Vector3(x + 1, 0, y + 1);
                Vector3 sliceBottomRight = bottomLeft + new Vector3(x + 1, 0, y);
                
                sliceBottomLeft.y += SampleCell(x, y) * amplitude;
                sliceTopLeft.y += SampleCell(x, y + 1) * amplitude;
                sliceTopRight.y += SampleCell(x + 1, y + 1) * amplitude;
                sliceBottomRight.y += SampleCell(x + 1, y) * amplitude;

                int startVertex = vertex;
                gm.initialVertices[vertex++] = sliceBottomLeft;
                gm.initialVertices[vertex++] = sliceTopLeft;
                gm.initialVertices[vertex++] = sliceTopRight;
                gm.initialVertices[vertex++] = sliceTopRight;
                gm.initialVertices[vertex++] = sliceBottomRight;
                gm.initialVertices[vertex++] = sliceBottomLeft;

                for (int i = 0; i < 6; i++)
                {
                    gm.meshUv[startVertex + i] = new Vector2(x / samples.x, y / samples.y);
                    gm.meshTriangles[startVertex + i] = startVertex + i;
                }
            }
        }

        mesh.vertices = gm.initialVertices;
        mesh.uv = gm.meshUv;
        mesh.triangles = gm.meshTriangles;
        mesh.RecalculateNormals();

        collider.sharedMesh = null;
        collider.sharedMesh = mesh;

        Shader shader = Shader.Find("Diffuse");

        Material material = null;
        if (renderer.material == null)
        {
            material = new Material(shader);
            renderer.material = material;
        }
    }

    float theta = 0;
    // Update is called once per frame
    void Update () {
        Vector3[] vertices = mesh.vertices;
        for (int i = 0; i < vertices.Length; i++)
        {
            //vertices[i].y = gm.initialVertices[i].y * Mathf.Sin(theta);
            vertices[i].y = SampleCell(vertices[i].x, vertices[i].z + theta) * amplitude;
        }
        theta += Time.deltaTime;
        mesh.vertices = vertices;
        collider.sharedMesh = null;
        collider.sharedMesh = mesh;
    }
}
