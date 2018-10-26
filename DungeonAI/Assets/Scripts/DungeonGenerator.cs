using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour {

    static List<double> CHILD_START_1X1= new List<double>() { 0.0, 0.3, 0.35, 0.45 } ;
    static List<double> CHILD_START_2X1 = new List<double>() { 0.0, 0.1, 0.25, 0.25, 0.25, 0.15 };
    static List<double> CHILD_START_2X2 = new List<double>() { 0.0, 0.0, 0.20, 0.25, 0.25, 0.15, 0.1, 0.05 };

    static List<double> CHILD_END_1X1 = new List<double>() { 0.9, 0.1, 0.0, 0.0 };
    static List<double> CHILD_END_2X1 = new List<double>() { 0.85, 0.1, 0.05, 0.0, 0.0, 0.0 };
    static List<double> CHILD_END_2X2 = new List<double>() { 0.80, 0.1, 0.05, 0.05, 0.0, 0.0, 0.0, 0.0 };

    static List<List<double>> CHILD_START_DIST = new List<List<double>>() { CHILD_START_1X1, CHILD_START_2X1, CHILD_START_2X2 };
    static List<List<double>> CHILD_END_DIST = new List<List<double>>() { };

    static List<double> SIZE_START_DIST = new List<double>() { 0.65, 0.25, 0.1 };
    static List<double> SIZE_MID_DIST = new List<double>() { 0.2, 0.35, 0.45 };
    static List<double> SIZE_END_DIST = new List<double>() { 1.0, 0, 0 };

    // Use this for initialization
    void Start () {
		
	}
	
    // Update is called once per frame
	void Update () {
		
	}
    /*
        // Creates new DungeonNode for every tile in the matrix
        for (int row = 0; row < dungeonMatrix.GetLength(0); row ++)
        {
            for (int col = 0; col < dungeonMatrix.GetLength(1); col++)
            {
                dungeonMatrix[row, col] = new DungeonNode();
            }
              
        } */
}


