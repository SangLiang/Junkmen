using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;


public class BoardManager : MonoBehaviour
{
    [Serializable]
    public class Count
    {
        public int minmum;
        public int maxmum;
        public Count(int min, int max)
        {
            minmum = min;
            maxmum = max;
        }

    }

    public int colums = 8;
    public int rows = 8;
    public Count wallCount = new Count(5, 9);
    public Count foodCount = new Count(1, 5);
    public GameObject exit;
    public GameObject[] floorTiles;
    public GameObject[] wallTiles;
    public GameObject[] footTiles;
    public GameObject[] enemyTiles;
    public GameObject[] outWallTiles;


    private Transform boardHolder;
    private List<Vector3> gridPosition = new List<Vector3>();

    void InitialiseList()
    {
        gridPosition.Clear();

        for (int x = 1; x < colums - 1; x++)
        {
            for (int y = 1; y < rows - 1; y++)
            {
                gridPosition.Add(new Vector3(x, y, 0f));
            }
        }
    }


    void BoardSetUp()
    {
        boardHolder = new GameObject("Board").transform;
        for (int x = -1; x < colums + 1; x++)
        {
            for (int y = -1; y < rows + 1; y++)
            {

            }
        }


    }




    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
