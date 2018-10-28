using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteCameraController : MonoBehaviour {

    public GameObject player;       //Public variable to store a reference to the player game object
    private Vector3 offset;         //Private variable to store the offset distance between the player and camera

    // Use this for initialization
    void Start () {
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        // Uncomment below if player starts at 0,0. Otherwise leave it as 0 for cam to be centered on player
        offset = new Vector3(0f, 0f, -10f); //transform.position - player.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        transform.position = player.transform.position + offset;
    }
}
