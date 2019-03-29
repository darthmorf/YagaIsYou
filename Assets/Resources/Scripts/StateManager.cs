using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour {

    public Entity player = null;
    
    float squareSize = 0.8f;

    // Use this for initialization
    void Start ()
    {
        player = new Entity("Yaga", "Sprites/yaga");

        //Entity temp = new Entity("Yaga2", "Sprites/yaga");
    }
	
	// Update is called once per frame
	void Update ()
    {
        Movement();	
	}

    void Movement ()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
           player.destination.y += squareSize;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            player.destination.y -= squareSize;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            player.destination.x += squareSize;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            player.destination.x -= squareSize;
        }

        player.go.transform.position = Vector2.Lerp(player.go.transform.position, player.destination, player.speed * Time.deltaTime);
        //player.go.transform.position = position;
    }
}
