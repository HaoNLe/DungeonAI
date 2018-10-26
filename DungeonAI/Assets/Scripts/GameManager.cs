using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public DungeonBasicRoom dungeonBasicRoom;

	// Use this for initialization
	void Start () {
        dungeonBasicRoom = GetComponent<DungeonBasicRoom>();
        InitGame();
    }

    void InitGame()
    {
        dungeonBasicRoom.SetupScene();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
