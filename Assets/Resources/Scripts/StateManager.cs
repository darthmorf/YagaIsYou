using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour {

    public Entity player = null;
    
    float squareSize = 0.8f;

    // Use this for initialization
    void Start ()
    {
        player = new Entity(Entity.type.Yaga);

        Entity wall1 = new Entity(Entity.type.WallH);
        wall1.pos = new Vector2(0, 0.8f);
        Entity wall2 = new Entity(Entity.type.WallH);
        wall2.pos = new Vector2(0.8f, 0.8f);
        Entity wall3 = new Entity(Entity.type.WallH);
        wall3.pos = new Vector2(2.4f, 0.8f);
        Entity wall4 = new Entity(Entity.type.WallV);
        wall4.pos = new Vector2(2.4f, 1.6f);
        Entity wall5 = new Entity(Entity.type.WallV);
        wall5.pos = new Vector2(2.4f, 2.4f);
        Entity wall6 = new Entity(Entity.type.WallV);
        wall6.pos = new Vector2(0f, -0.8f);
        Entity wall7 = new Entity(Entity.type.WallH);
        wall7.pos = new Vector2(0f, -1.6f);
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
            RaycastHit2D hit = Physics2D.Raycast(player.pos, new Vector2(0, 1), 0.8f, player.layerMask);
            if (hit.collider == null)
            {
                player.dest.y += squareSize;
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            RaycastHit2D hit = Physics2D.Raycast(player.pos, new Vector2(0, -1), 0.8f, player.layerMask);
            if (hit.collider == null)
            {
                player.dest.y -= squareSize;
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            RaycastHit2D hit = Physics2D.Raycast(player.pos, new Vector2(1, 0), 0.8f, player.layerMask);
            if (hit.collider == null)
            {
                player.dest.x += squareSize;
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            RaycastHit2D hit = Physics2D.Raycast(player.pos, new Vector2(-1, 0), 0.8f, player.layerMask);
            if (hit.collider == null)
            {
                player.dest.x -= squareSize;
            }
        }

        player.go.transform.position = Vector2.Lerp(player.pos, player.dest, player.speed * Time.deltaTime);
    }
}
