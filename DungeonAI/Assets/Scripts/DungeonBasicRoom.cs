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
    private string FOREGROUND_LAYER_NAME = "Foreground";

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
                GameObject instantiated;

                // Generate floor tiles
                if (0 <= x && x < columns && 0 <= y && y < rows)
                {

                    // Generate bottom walltop tiles and set layer to foreground
                    if (y == 0 && 0 <= x && x < columns)
                    {
                        toInstantiate = wallTopMid;
                        instantiated = placeTile(toInstantiate, x, y);
                        setForeground(instantiated, FOREGROUND_LAYER_NAME);
                        addBoxCollider(instantiated, 0.0f, -1.25f);
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
                    placeTile(toInstantiate, x, y);
                }

                // Generate top walltop tiles
                else if (y == rows + 1)
                {
                    if (x == -1)
                    {
                        toInstantiate = sideTopLeft;
                    }
                    else if (x == columns)
                    {
                        toInstantiate = sideTopRight;
                    }
                    else if (x == 0)
                    {
                        toInstantiate = wallTopLeft;
                    }
                    else if (x == columns - 1)
                    {
                        toInstantiate = wallTopRight;
                    }
                    else
                    {
                        toInstantiate = wallTopMid;
                    }
                    instantiated = placeTile(toInstantiate, x, y);
                    addBoxCollider(instantiated, 0.0f, -0.25f);
                }

                // Generate side wall tiles
                else if (x == -1 || x == columns)
                {
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
                    placeTile(toInstantiate, x, y);
                }


                // Generate wall tiles above and below the room
                else
                {
                    // Above room left and right wall
                    if (x == 0 && y == rows)
                    {
                        toInstantiate = leftWall;
                        placeTile(toInstantiate, x, y);
                    }
                    else if (x == columns - 1 && y == rows)
                    {
                        toInstantiate = rightWall;
                        placeTile(toInstantiate, x, y);
                    }
                    // Middle walls above room 
                    else if (0 < x && x < columns - 1 && y == rows)
                    {
                        if (Random.Range(0.0f, 1.0f) > 0.75)
                        {
                            toInstantiate = midWallTiles[Random.Range(0, midWallTiles.Length)];
                        }
                        else
                        {
                            toInstantiate = midWallTiles[0];
                        }
                        placeTile(toInstantiate, x, y);
                    }
                    // Middle walls below room
                    else if (0 <= x && x < columns && y == -1)
                    {
                        if (Random.Range(0.0f, 1.0f) > 0.75)
                        {
                            toInstantiate = midWallTiles[Random.Range(0, midWallTiles.Length)];
                        }
                        else
                        {
                            toInstantiate = midWallTiles[0];
                        }
                        instantiated = placeTile(toInstantiate, x, y);
                        setForeground(instantiated, FOREGROUND_LAYER_NAME);
                    }
                }

            }
        }
    }

    // Moves a sprite to foreground
    void setForeground(GameObject toBringForward, string layerName)
    {
        SpriteRenderer sprite = toBringForward.GetComponent<SpriteRenderer>();
        sprite.sortingLayerName = layerName;
    }

    // Adds a box collider
    void addBoxCollider(GameObject toCollide, float xOffset=0.0f, float yOffset=0.0f)
    {
        BoxCollider bc = toCollide.AddComponent(typeof(BoxCollider)) as BoxCollider;
        bc.size = new Vector3(1.0f, 1.0f, 1.0f);
        bc.center = new Vector3(xOffset, yOffset, 0.0f);
    }

    // Instantiates the tile and places it in the scene
    GameObject placeTile(GameObject toPlace, int x, int y)
    {
        GameObject placed = Instantiate(toPlace, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
        placed.transform.SetParent(boardHolder);
        return placed;
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
