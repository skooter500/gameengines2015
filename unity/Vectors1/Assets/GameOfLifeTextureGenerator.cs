using UnityEngine;
using System.Collections;

public class GameOfLifeTextureGenerator : TextureGenerator
{
    [HideInInspector]
    public Texture2D texture;

    public Color backGround;
    public Color foreGround;

    int boardWidth;
    int boardHeight;

    NoiseForm noiseForm;
    // Use this for initialization

    public float delay;

    bool[,] current;
    bool[,] next;

    public GameOfLifeTextureGenerator()
    {
        backGround = Color.blue;
        foreGround = Color.gray;
        delay = 0.1f;
    }

    public void Clear()
    {
        ClearBoard(current);
    }

    private void ClearBoard(bool[,] board)
    {
        for (int x = 0; x < boardWidth; x++)
        {
            for (int y = 0; y < boardHeight; y++)
            {
                board[y, x] = false;
            }
        }
    }

    void UpdateTexture()
    {
        for (int y = 0; y < boardHeight; y++)
        {
            for (int x = 0; x < boardWidth; x++)
            {
                texture.SetPixel(x, y, current[y, x] ? foreGround : backGround);
            }
        }

        texture.Apply();
    }

    float t = 0.0f;

    void LerpTexture()    {
        if (texture == null)
        {
            return;
        }
        for (int y = 0; y < boardHeight; y++)
        {
            for (int x = 0; x < boardWidth; x++)
            {
                Color from = next[y, x] ? foreGround : backGround;
                Color to = current[y, x] ? foreGround : backGround;
                texture.SetPixel(x, y, Color.Lerp(from, to, t));
            }
        }
        t += ((Time.deltaTime * 2.0f) / delay);
        texture.Apply();
    }

    public override Texture2D GenerateTexture()
    {
        texture = new Texture2D(boardWidth, boardHeight, TextureFormat.RGBAFloat, false);
        texture.filterMode = FilterMode.Point;
        StartCoroutine("UpdateBoard");
        //StartCoroutine("ResetBoard");
        return texture;
    }


    void Start()
    {
        noiseForm = GetComponent<NoiseForm>();
        if (noiseForm == null)
        {
            Debug.LogError("GameOfLifeTextureGenerator with no NoiseForm");
        }

        boardWidth = (int)noiseForm.cellsPerTile.x;
        boardHeight = (int)noiseForm.cellsPerTile.y;
        
        current = new bool[boardHeight, boardWidth];
        next = new bool[boardHeight, boardWidth];
        // MakeGosperGun(boardWidth / 2, boardHeight / 2);
        MakeTumbler(boardWidth / 2, boardHeight / 2);
        
        Randomise();
    }

    bool paused = false;

    int CountNeighbours(int row, int col)
    {
        int count = 0;

        // Top left
        if ((row > 0) && (col > 0) && (current[row - 1,col - 1]))
        {
            count++;
        }
        // Top
        if ((row > 0) && current[row - 1, col])
        {
            count++;
        }
        // Top right
        if ((row > 0) && (col < (boardWidth - 1)) && (current[row - 1, col + 1]))
        {
            count++;
        }
        // Left
        if ((col > 0) && (current[row, col - 1]))
        {
            count++;
        }
        // Right
        if ((col < (boardWidth - 1)) && current[row, col + 1])
        {
            count++;
        }
        // Bottom left
        if ((col > 0) && (row < (boardHeight - 1))
          && current[row + 1, col - 1])
        {
            count++;
        }
        // Bottom
        if ((row < (boardHeight - 1)) && (current[row + 1, col]))
        {
            count++;
        }
        // Bottom right
        if ((col < (boardWidth - 1)) && (row < (boardHeight - 1))
          && current[row + 1, col + 1])
        {
            count++;
        }
        return count;
    }

    public void Randomise()  
    {
        for (int row = 0; row < boardHeight; row++)
        {
            for (int col = 0; col < boardWidth; col++)
            {
                float f = UnityEngine.Random.Range(0.0f, 1.0f);
                if (f > 0.5f)
                {
                    current[row, col] = true;
                }
            }
        }
    }

    System.Collections.IEnumerator ResetBoard()
    {
        while (true)
        {
            yield return new WaitForSeconds(delay * 50);
            Randomise();
        }
    }

    System.Collections.IEnumerator UpdateBoard()
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            if (!paused)
            {
                ClearBoard(next);
                for (int row = 0; row < boardHeight; row++)
                {
                    for (int col = 0; col < boardWidth; col++)
                    {
                        int count = CountNeighbours(row, col);
                        if (current[row, col])
                        {
                            if (count < 2)
                            {
                                next[row, col] = false;
                            }
                            else if ((count == 2) || (count == 3))
                            {
                                next[row, col] = true;
                            }
                            else if (count > 3)
                            {
                                next[row, col] = false;
                            }
                        }
                        else
                        {
                            if (count == 3)
                            {
                                next[row, col] = true;
                            }
                        }
                    }
                }
            }
            bool[,] temp;
            temp = current;
            current = next;
            next = temp; 
            t = 0.0f;
            //UpdateTexture();
        }
    }

    public void On(int x, int y)
    {
        if ((x >= 0) && (x < boardWidth) && (y >= 0) && (y < boardHeight))
        {
            current[y, x] = true;
        }
    }

    public void Off(int x, int y)
    {
        if ((x >= 0) && (x < boardWidth) && (y >= 0) && (y < boardHeight))
        {
            current[y, x] = false;
        }
    }

    public void MakeGosperGun(int x, int y)
    {
        On(x + 23, y);
        On(x + 24, y);
        On(x + 34, y);
        On(x + 35, y);

        On(x + 22, y + 1);
        On(x + 24, y + 1);
        On(x + 34, y + 1);
        On(x + 35, y + 1);

        On(x + 0, y + 2);
        On(x + 1, y + 2);
        On(x + 9, y + 2);
        On(x + 10, y + 2);
        On(x + 22, y + 2);
        On(x + 23, y + 2);

        On(x + 0, y + 3);
        On(x + 1, y + 3);
        On(x + 8, y + 3);
        On(x + 10, y + 3);

        On(x + 8, y + 4);
        On(x + 9, y + 4);
        On(x + 16, y + 4);
        On(x + 17, y + 4);

        On(x + 16, y + 5);
        On(x + 18, y + 5);

        On(x + 16, y + 6);

        On(x + 35, y + 7);
        On(x + 36, y + 7);

        On(x + 35, y + 8);
        On(x + 37, y + 8);

        On(x + 35, y + 9);

        On(x + 24, y + 12);
        On(x + 25, y + 12);
        On(x + 26, y + 12);

        On(x + 24, y + 13);

        On(x + 25, y + 14);
    }

    public void MakeLightWeightSpaceShip(int x, int y)
    {
        On(x + 1, y);
        On(x + 2, y);
        On(x + 3, y);
        On(x + 4, y);

        On(x, y + 1);
        On(x + 4, y + 1);

        On(x + 4, y + 2);

        On(x, y + 3);
        On(x + 3, y + 3);
    }


    public void MakeTumbler(int x, int y)
    {
        On(x + 1, y);
        On(x + 2, y);
        On(x + 4, y);
        On(x + 5, y);

        On(x + 1, y + 1);
        On(x + 2, y + 1);
        On(x + 4, y + 1);
        On(x + 5, y + 1);

        On(x + 2, y + 2);
        On(x + 4, y + 2);

        On(x, y + 3);
        On(x + 2, y + 3);
        On(x + 4, y + 3);
        On(x + 6, y + 3);

        On(x, y + 4);
        On(x + 2, y + 4);
        On(x + 4, y + 4);
        On(x + 6, y + 4);

        On(x, y + 5);
        On(x + 1, y + 5);
        On(x + 5, y + 5);
        On(x + 6, y + 5);

    }

    

    public void MakeGlider(int x, int y)
    {
        current[y, x + 1] = true;
        current[y + 1, x + 2] = true;
        current[y + 2, x] = true;
        current[y + 2, x + 1] = true;
        current[y + 2, x + 2] = true;
    }

    void Update()
    {
        LerpTexture();
    }


}
