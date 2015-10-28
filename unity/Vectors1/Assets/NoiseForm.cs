using UnityEngine;
using System.Collections.Generic;

using System.Threading;
using com.youvisio;

public class NoiseForm : MonoBehaviour {

    public Vector2 cellsPerTile = new Vector2(50, 50);
    public Vector2 cellSize = new Vector2(1, 1);
    private Vector2 tileSize = new Vector2(50, 50);

    [HideInInspector]
    public float maxY;

    TextureGenerator textureGenerator;

    GameObject[] tiles = new GameObject[9];
    GameObject player;
    Texture2D texture;

    Sampler[] samplers;

    public float GetHeight(Vector3 pos)
    {
        pos.y = float.MaxValue;
        RaycastHit hitInfo;
        bool collided = Physics.Raycast(pos, Vector3.down, out hitInfo);
        if (collided)
        {
            return hitInfo.point.y;
        }
        else
        {
            return hitInfo.point.y; //  SampleCell(pos);
        }
    }
    
    private void CreateTiles()
    {
        // The position of the bottom left tile
        Vector3 bottomLeft = new Vector3();
        bottomLeft.x = transform.position.x - (tileSize.x);
        bottomLeft.z = transform.position.z - (tileSize.y);

        int tileIndex = 0;

        for (int z = 0; z < 3; z ++)
        {
            for (int x = 0; x < 3; x ++)
            {
                GameObject tile = new GameObject();
                tile.transform.parent = this.transform;
                MeshRenderer renderer = tile.AddComponent<MeshRenderer>();
                renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                renderer.receiveShadows = true;
                Mesh mesh = tile.AddComponent<MeshFilter>().mesh;
                mesh.Clear();
                tile.AddComponent<MeshCollider>();
                Vector3 tilePos = new Vector3();
                tilePos.x = bottomLeft.x + (x * tileSize.x);
                tilePos.z = bottomLeft.z + (z * tileSize.y);
                tilePos.y = transform.position.y;
                tile.transform.position = tilePos;                
                GenerateTile(tile);
                tiles[tileIndex ++] = tile;
            }
        }
    }

    private int FindTile(Vector3 pos)
    {       
        for (int i = 0; i < tiles.Length; i ++)
        {
            GameObject tile = tiles[i];
            Vector3 tileBottomLeft = new Vector3(
                tile.transform.position.x - (tileSize.x / 2)
                , 0
                , tile.transform.position.z - (tileSize.y / 2)
                );
            Vector3 tileTopRight = new Vector3(
                tile.transform.position.x + (tileSize.x / 2)
                , 0
                , tile.transform.position.z + (tileSize.y / 2)
                );
            if (pos.x > tileBottomLeft.x && pos.x <= tileTopRight.x && pos.z > tileBottomLeft.z && pos.z <= tileTopRight.z)
            {
                return i;
            }
        }
        return -1;
    }

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

    public void OnDrawGizmos()
    {
        //Gizmos.DrawWireCube(transform.position, size);
    }

    void MaxY(float y)
    {
        if (y > maxY)
        {
            maxY = y;
        }
    }

    class Arg
    {
        public GameObject t;
        public Vector3 position;
    }

    class GeneratedMesh
    {
        public Vector3[] vertices;
        public Vector3[] normals;
        public Vector2[] uvs;
        public Color[] colours;
        public int[] triangles;
    }
    
    List<BackgroundWorker> workers = new List<BackgroundWorker>();

    void GenerateTile(GameObject tileGameObject)
    {
        BackgroundWorker backgroundWorker = new BackgroundWorker();

        MeshRenderer renderer = tileGameObject.GetComponent<MeshRenderer>();
        Mesh mesh = tileGameObject.GetComponent<MeshFilter>().mesh;
        mesh.Clear();
        backgroundWorker.DoWork += (o, a) =>
        {
            Arg aa = (Arg)a.Argument;            
            a.Result = GenerateTileAsync(aa.t, aa.position);
        };

        backgroundWorker.RunWorkerCompleted += (o, a) =>
        {
            GeneratedMesh gm = (GeneratedMesh)a.Result;
            
            mesh.vertices = gm.vertices;
            mesh.uv = gm.uvs;
            mesh.triangles = gm.triangles;
            mesh.RecalculateNormals();

            //renderer.material.color = RandomTextureGenerator.RandomColor();

            //tileGameObject.GetComponent<MeshCollider>().sharedMesh = null;
            //tileGameObject.GetComponent<MeshCollider>().sharedMesh = mesh;
        };

        Arg args = new Arg();
        args.t = tileGameObject;
        args.position = tileGameObject.transform.position;
        workers.Add(backgroundWorker);
        backgroundWorker.RunWorkerAsync(args);
    }

    GeneratedMesh GenerateTileAsync(GameObject tileGameObject, Vector3 position)
    {        
        int verticesPerSegment = 6;

        int vertexCount = verticesPerSegment * ((int)cellsPerTile.x) * ((int)cellsPerTile.y);

        GeneratedMesh gm = new GeneratedMesh();

        gm.vertices = new Vector3[vertexCount];
        gm.normals = new Vector3[vertexCount];
        gm.uvs = new Vector2[vertexCount];
        gm.triangles = new int[vertexCount];

        gm.colours = new Color[vertexCount];

        int vertex = 0;

        // What cell is x and z for the bottom left of this tile in world space
        Vector3 tileBottomLeft = new Vector3();
        tileBottomLeft.x = - (tileSize.x) / 2;
        tileBottomLeft.z = - (tileSize.y) / 2;

        for (int z = 0; z < cellsPerTile.y; z++)
        {
            for (int x = 0; x < cellsPerTile.x; x++)
            {
                int startVertex = vertex;
                // Calculate some stuff
                Vector3 cellBottomLeft = tileBottomLeft + new Vector3(x * cellSize.x, 0, z * cellSize.y);
                Vector3 cellTopLeft = tileBottomLeft + new Vector3(x * cellSize.x, 0, (z + 1) * cellSize.y);
                Vector3 cellTopRight = tileBottomLeft + new Vector3((x + 1) * cellSize.x, 0 , (z + 1) * cellSize.y);
                Vector3 celBottomRight = tileBottomLeft + new Vector3((x + 1) * cellSize.x, 0, z * cellSize.y);

                // Add all the samplers together to make the height
                Vector3 cellWorldCoords = position + tileBottomLeft  + new Vector3(x * cellSize.x, 0, z * cellSize.y);
                foreach(Sampler sampler in samplers)
                {
                    cellBottomLeft.y += sampler.Sample(cellWorldCoords.x, cellWorldCoords.z);
                    cellTopLeft.y += sampler.Sample(cellWorldCoords.x, cellWorldCoords.z + cellSize.y);
                    cellTopRight.y += sampler.Sample(cellWorldCoords.x + cellSize.x, cellWorldCoords.z + cellSize.y);
                    celBottomRight.y += sampler.Sample(cellWorldCoords.x + cellSize.x, cellWorldCoords.z);
                }
                                   
                // Make the vertices
                gm.vertices[vertex++] = cellBottomLeft;
                gm.vertices[vertex++] = cellTopLeft;
                gm.vertices[vertex++] = cellTopRight;
                gm.vertices[vertex++] = cellTopRight;
                gm.vertices[vertex++] = celBottomRight;
                gm.vertices[vertex++] = cellBottomLeft;

                // Make the normals, UV's and triangles                
                for (int i = 0; i < 6; i++)
                {
                    int vertexIndex = startVertex + i;
                    gm.triangles[vertexIndex] = vertexIndex;
                    gm.uvs[vertexIndex] = new Vector2(x / cellsPerTile.x, z / cellsPerTile.y);
                }
            }            
        }
        return gm;
    }


    void Start()
    {
        tileSize = new Vector2(cellSize.x * cellsPerTile.x, cellSize.y * cellsPerTile.y);

        samplers = GetComponents<Sampler>();
        if (samplers == null)
        {
            Debug.Log("Sampler is null! Add a sampler to the NoiseForm");
        }

        CreateTiles();
        Random.seed = 42;
        player = GameObject.FindGameObjectWithTag("Player");

        textureGenerator = GetComponent<TextureGenerator>();
        if (textureGenerator != null)
        {
            texture = textureGenerator.GenerateTexture();
        }
       foreach(GameObject tile in tiles)
       {
           tile.GetComponent<Renderer>().material.SetTexture(0, texture);
       }
    }
   
    void Update () {
        int tileIndex = FindTile(player.transform.position);
        GameManager.PrintFloat("Tile: ", tileIndex);
        //TODO: Diagonals
        switch (tileIndex)
        {
            case 7:
            {
                // Player has moved forward one tile
                GameObject[] newTiles = new GameObject[9];                
                newTiles[0] = tiles[3]; newTiles[1] = tiles[4]; newTiles[2] = tiles[5];
                newTiles[3] = tiles[6]; newTiles[4] = tiles[7]; newTiles[5] = tiles[8];
                newTiles[6] = tiles[0]; newTiles[7] = tiles[1]; newTiles[8] = tiles[2];
                tiles = newTiles;
                tiles[6].transform.Translate(new Vector3(0, 0, tileSize.y * 3.0f));
                tiles[7].transform.Translate(new Vector3(0, 0, tileSize.y * 3.0f));
                tiles[8].transform.Translate(new Vector3(0, 0, tileSize.y * 3.0f));
                GenerateTile(tiles[6]);
                GenerateTile(tiles[7]);
                GenerateTile(tiles[8]);                
                break;
            }
            case 1:
            {
                // Player has moved backward one tile, so regenerate the 2nd row
                GameObject[] newTiles = new GameObject[9];
                newTiles[0] = tiles[6]; newTiles[1] = tiles[7]; newTiles[2] = tiles[8];
                newTiles[3] = tiles[0]; newTiles[4] = tiles[1]; newTiles[5] = tiles[2];
                newTiles[6] = tiles[3]; newTiles[7] = tiles[4]; newTiles[8] = tiles[5];
                tiles = newTiles;
                tiles[0].transform.Translate(0, 0, -tileSize.y * 3.0f);
                tiles[1].transform.Translate(0, 0, -tileSize.y * 3.0f);
                tiles[2].transform.Translate(0, 0, -tileSize.y * 3.0f);
                GenerateTile(tiles[0]);
                GenerateTile(tiles[1]);
                GenerateTile(tiles[2]);
                break;
            }
            case 3:
            {
                // Player has moved left one tile, so regenerate the 0th col
                GameObject[] newTiles = new GameObject[9];
                newTiles[0] = tiles[2]; newTiles[1] = tiles[0]; newTiles[2] = tiles[1];
                newTiles[3] = tiles[5]; newTiles[4] = tiles[3]; newTiles[5] = tiles[4];
                newTiles[6] = tiles[8]; newTiles[7] = tiles[6]; newTiles[8] = tiles[7];
                tiles = newTiles;
                tiles[0].transform.Translate(-tileSize.x * 3.0f, 0, 0);
                tiles[3].transform.Translate(-tileSize.x * 3.0f, 0, 0);
                tiles[6].transform.Translate(-tileSize.x * 3.0f, 0, 0);
                GenerateTile(tiles[0]);
                GenerateTile(tiles[3]);
                GenerateTile(tiles[6]);
                break;
            }
            case 5:
            {
                // Player has moved left one tile, so regenerate the 0th col
                GameObject[] newTiles = new GameObject[9];
                newTiles[0] = tiles[1]; newTiles[1] = tiles[2]; newTiles[2] = tiles[0];
                newTiles[3] = tiles[4]; newTiles[4] = tiles[5]; newTiles[5] = tiles[3];
                newTiles[6] = tiles[7]; newTiles[7] = tiles[8]; newTiles[8] = tiles[6];
                tiles = newTiles;
                tiles[2].transform.Translate(tileSize.x * 3.0f, 0, 0);
                tiles[5].transform.Translate(tileSize.x * 3.0f, 0, 0);
                tiles[8].transform.Translate(tileSize.x * 3.0f, 0, 0);
                GenerateTile(tiles[2]);
                GenerateTile(tiles[5]);
                GenerateTile(tiles[8]);
                break;
            }
        }
        for(int i = workers.Count - 1 ; i >=  0 ; i --)
        {
            if (workers[i].IsBusy) 
            {
                workers[i].Update();
            }
            else
            {
                workers.Remove(workers[i]);
            }
        }
    }
}
