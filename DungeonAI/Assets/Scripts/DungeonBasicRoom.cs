using System.Collections;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;
using UnityEngine;

public class DungeonBasicRoom : MonoBehaviour {

    public GameObject[] floorTiles;
    public GameObject[] midWallTiles;
    public GameObject leftWall;
    public GameObject rightWall;
    public GameObject wallTopMid;
    public GameObject wallTopLeft;
    public GameObject wallTopRight;
    public GameObject sideTopLeft;
    public GameObject sideTopRight;
    public GameObject sideMidLeft;
    public GameObject sideMidRight;
    public GameObject sideFrontLeft;
    public GameObject sideFrontRight;

    private Transform boardHolder;
    public List<Vector3> gridPositions = new List<Vector3>();
    
    // Initializes list for inner area of room 
    /*
    void InitializeList()
    {
        gridPositions.Clear();

        for (int x = 1; x < columns - 1; x++)
        {
            for (int y = 1; y < rows - 1; y++)
            {
                gridPositions.Add(new Vector3(x, y, 0f));
            }
        }
    }
    */

    void GenerateRoom(int width, int height)
    {
        int columns = width;
        int rows = height;
        boardHolder = new GameObject("Board").transform;

        for (int x = -1; x < columns + 1; x++)
        {
            for (int y = -1; y < rows + 2; y++)
            {
                // init GameObject as placeholder for our logic
                GameObject toInstantiate = midWallTiles[5];

                // Generate floor tiles
                if (0 <= x && x <= columns && 0 <= y && y <= rows)
                {

                    // Generate bottom walltop tiles
                    if (y == 0 && 0 <= x && x < columns)
                    {
                        toInstantiate = wallTopMid;
                        placeTile(toInstantiate, x, y);
                    }

                    // choose and generate floor tile
                    float abnormalFloor = Random.Range(0.0f, 1.0f);
                    if (abnormalFloor > 0.8)
                    {
                        toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];
                    }
                    else
                    {
                        toInstantiate = floorTiles[0];
                    }
                }

                // Generate top wall tiles
                if (y == rows + 1 && x == -1)
                {
                    toInstantiate = sideTopLeft;
                }
                else if (y == rows + 1 && x == columns)
                {
                    toInstantiate = sideTopRight;
                }
                else if (y == rows + 1 && x == 0)
                {
                    toInstantiate = wallTopLeft;
                }
                else if (y == rows + 1 && x == columns - 1)
                {
                    toInstantiate = wallTopRight;
                } else if (y == rows + 1)
                {
                    toInstantiate = wallTopMid;
                }
                // Generate side wall tiles
                if (x == -1 && 0 <= y && y <= rows)
                {
                    toInstantiate = sideMidLeft;
                }
                else if (x == -1 && y == -1)
                {
                    toInstantiate = sideFrontLeft;
                }
                else if (x == columns && 0 <= y && y <= rows)
                {
                    toInstantiate = sideMidRight;
                }
                else if (x == columns && y == -1)
                {
                    toInstantiate = sideFrontRight;
                }

                // Generate wall tiles above and below the room
                if (x == 0 && y == rows)
                {
                    toInstantiate = leftWall;
                }
                else if (x == columns - 1 && y == rows)
                {
                    toInstantiate = rightWall;
                }
                else if (0 < x && x < columns - 1 && y == rows)
                {
                    if (Random.Range(0.0f, 1.0f) > 0.65)
                    {
                        toInstantiate = midWallTiles[Random.Range(0, midWallTiles.Length)];
                    }
                    else
                    {
                        toInstantiate = midWallTiles[0];
                    }
                }
                else if (0 <= x && x < columns && y == - 1)
                {
                    if (Random.Range(0.0f, 1.0f) > 0.65)
                    {
                        toInstantiate = midWallTiles[Random.Range(0, midWallTiles.Length)];
                    }
                    else
                    {
                        toInstantiate = midWallTiles[0];
                    }
                }

                placeTile(toInstantiate, x, y);
            }
        }
    }

    void placeTile(GameObject toPlace, int x, int y)
    {
        GameObject placed = Instantiate(toPlace, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
        placed.transform.SetParent(boardHolder);
    }

    public void SetupScene()
    {
        GenerateRoom(Random.Range(18,28), Random.Range(15,25));
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update () {
		
	}
}
