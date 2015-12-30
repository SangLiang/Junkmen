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
    private List<Vector3> gridPosition = new List<Vector3>();//网格坐标列表

    void InitialiseList()
    {
        //初始化列表
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
        //生成外墙和墙内的地面
        boardHolder = new GameObject("Board").transform;
        for (int x = -1; x < colums + 1; x++)
        {
            for (int y = -1; y < rows + 1; y++)
            {
                //生成地板图片
                GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];

                //生成外墙
                if (x == -1 || y == -1 || x == colums || y == rows)
                {
                    toInstantiate = outWallTiles[Random.Range(0, outWallTiles.Length)];
                }

                //实例化外墙和内墙
                GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
                instance.transform.SetParent(boardHolder);
            }
        }


    }



    //随机位置方法
    Vector3 RandomPosition()
    {
        int randomIndex = Random.Range(0, gridPosition.Count);
        Vector3 randomPosition = gridPosition[randomIndex];
        gridPosition.RemoveAt(randomIndex);
        return randomPosition;
    }


    void LayOutObjectAtRandom(GameObject[] tileArray, int minmum, int maxmum)
    {
        int objectCount = Random.Range(minmum, maxmum + 1);
        for (int i = 0; i < objectCount; i++)
        {
            Vector3 randomPosition = RandomPosition();
            GameObject tileChoise = tileArray[Random.Range(0, tileArray.Length)];
            Instantiate(tileChoise, randomPosition, Quaternion.identity);
        }


    }

    public void SetupScene(int level)
    {
        BoardSetUp();
        InitialiseList();
        LayOutObjectAtRandom(wallTiles,wallCount.minmum,wallCount.maxmum);
        LayOutObjectAtRandom(footTiles, foodCount.minmum, foodCount.maxmum);

        int enemyCount = (int)Mathf.Log(level,2f);
        LayOutObjectAtRandom(enemyTiles,enemyCount,enemyCount);
        Instantiate(exit, new Vector3(colums - 1, rows - 1, 0f), Quaternion.identity);

    }


}
