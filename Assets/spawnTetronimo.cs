using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnTetronimo : MonoBehaviour
{
    public GameObject[] Tetronimoes;

    // Start is called before the first frame update
    void Start()
    {
        NewTetronimo();
    }

    public void NewTetronimo()
    {
        Instantiate(Tetronimoes[Random.Range(0, Tetronimoes.Length)], transform.position, Quaternion.identity);
    }
}
