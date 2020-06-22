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
                this.enabled = false;
                FindObjectOfType<spawnTetronimo>().NewTetronimo();
            }
            prevTime = Time.time;
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
        }
        return true;
    }
}
