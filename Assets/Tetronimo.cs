using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetronimo : MonoBehaviour
{
    public Vector3 rotationPoint;

    private float prevTime;
    public float fallTime = 0.8f;
    public static int height = 20;
    public static int width = 10;

    private static Transform[,] grid = new Transform[width, height];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);
            if(!ValidMove())
                transform.position += new Vector3(1, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);
            if(!ValidMove())
                transform.position += new Vector3(-1, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0,0,1), 90);
            if(!ValidMove())
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0,0,1), -90);
        }

        if (Time.time - prevTime > (Input.GetKey(KeyCode.DownArrow) ? fallTime / 10 : fallTime))
        {
            transform.position += new Vector3(0, -1, 0);
            if(!ValidMove())
            {
                transform.position += new Vector3(0, 1, 0);
                AddToGrid();
                CheckForLines();
                this.enabled = false;
                FindObjectOfType<spawnTetronimo>().NewTetronimo();
            }
            prevTime = Time.time;
        }
    }

    void CheckForLines()
    {
        for (int a = height - 1; a >= 0; a--)
        {
            if (HasLine(a))
            {
                DeleteLine(a);
                RowDown(a);
            }
        }
    }

    bool HasLine(int a)
    {
        for (int b = 0; b < width; b++)
        {
            if (grid[b, a] == null)
                return false;
        }
        return true;
    }

    void DeleteLine(int a)
    {
        for (int b = 0; b < width; b++)
        {
            Destroy(grid[b,a].gameObject);
            grid[b,a] = null;
        }
    }

    void RowDown(int a)
    {
        for (int c = a; c < height; c++)
        {
            for (int d = 0; d < width; d++)
            {
                if (grid[d,c] != null)
                {
                    grid[d, c -1] = grid[d, c];
                    grid[d, c] = null;
                    grid[d, c - 1].transform.position -= new Vector3(0, 1, 0);
                }
            }
        }
    }

    void AddToGrid()
    {
        foreach (Transform children in transform)
        {
            int roundX = Mathf.RoundToInt(children.transform.position.x);
            int roundY = Mathf.RoundToInt(children.transform.position.y);

            grid[roundX, roundY] = children;
        }
    }

    bool ValidMove()
    {
        foreach (Transform children in transform)
        {
            int roundX = Mathf.RoundToInt(children.transform.position.x);
            int roundY = Mathf.RoundToInt(children.transform.position.y);

            if(roundX < 0 || roundX >= width || roundY < 0 || roundY >= height)
            {
                return false;
            }

            if(grid[roundX,roundY] != null)
                return false;
        }
        return true;
    }
}
