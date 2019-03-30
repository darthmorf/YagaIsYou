﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StateManager : MonoBehaviour {

    public GameObject player;
    public Properties playerProp;
    public List<GameObject> gameObjs = new List<GameObject>();
    
    float squareSize = 0.8f;

    // Use this for initialization
    void Start ()
    {
        player = initObj(objType.Yaga, new Vector2(0, 0));
        playerProp = player.GetComponent<Properties>();
        gameObjs.Add(player);

        gameObjs.Add(initObj(objType.WallH, new Vector2(0, 0.8f)));
        gameObjs.Add(initObj(objType.WallH, new Vector2(0.8f, 0.8f)));
        gameObjs.Add(initObj(objType.WallH, new Vector2(2.4f, 0.8f)));
        gameObjs.Add(initObj(objType.WallV, new Vector2(2.4f, 1.6f)));
        gameObjs.Add(initObj(objType.WallV, new Vector2(2.4f, 2.4f)));
        gameObjs.Add(initObj(objType.WallV, new Vector2(0f, -0.8f)));
        gameObjs.Add(initObj(objType.WallH, new Vector2(0f, -1.6f)));
        gameObjs.Add(initObj(objType.Rock,  new Vector2(-2.4f, 0)));
        gameObjs.Add(initObj(objType.Rock,  new Vector2(-4.0f, 0)));
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
            List<GameObject> movedObjs = isMoveValid(playerProp.pos, new Vector2(0, 1), squareSize);
            if (movedObjs != null)
            {
                movedObjs.Add(player);
                foreach (GameObject movedObj in movedObjs)
                {
                    Properties prop = movedObj.GetComponent<Properties>();
                    prop.destY += squareSize;
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            List<GameObject> movedObjs = isMoveValid(playerProp.pos, new Vector2(0, -1), squareSize);
            if (movedObjs != null)
            {
                movedObjs.Add(player);
                foreach (GameObject movedObj in movedObjs)
                {
                    Properties prop = movedObj.GetComponent<Properties>();
                    prop.destY -= squareSize;
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            List<GameObject> movedObjs = isMoveValid(playerProp.pos, new Vector2(1, 0), squareSize);
            if (movedObjs != null)
            {
                movedObjs.Add(player);
                foreach (GameObject movedObj in movedObjs)
                {
                    Properties prop = movedObj.GetComponent<Properties>();
                    prop.destX += squareSize;
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            List<GameObject> movedObjs = isMoveValid(playerProp.pos, new Vector2(-1, 0), squareSize);
            if (movedObjs != null)
            {
                movedObjs.Add(player);
                foreach (GameObject movedObj in movedObjs)
                {
                    Properties prop = movedObj.GetComponent<Properties>();
                    prop.destX -= squareSize;
                }
            }
        }

        foreach (GameObject go in gameObjs)
        {
            Properties props = go.GetComponent<Properties>();
            props.pos = Vector2.Lerp(props.pos, props.dest, props.speed * Time.deltaTime);
        }
    }

    public enum objType
    {
        Yaga, WallH, WallV, Rock
    };

    GameObject initObj(objType entityType, Vector2 position)
    {
        GameObject go = new GameObject();
        Properties prop = go.AddComponent<Properties>();
        prop.go = go;
        prop.pos = position;
        SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
        prop.dest = prop.pos;

        switch (entityType)
        {
            case objType.Yaga:
                go.name = "Yaga";
                sr.sprite = Resources.Load<Sprite>("Sprites/yaga");
                go.layer = 8;
                break;

            case objType.WallH:
                go.name = "Wall";
                sr.sprite = Resources.Load<Sprite>("Sprites/wall01");
                go.layer = 9;
                break;

            case objType.WallV:
                go.name = "Wall";
                sr.sprite = Resources.Load<Sprite>("Sprites/wall02");
                go.layer = 9;
                break;

            case objType.Rock:
                go.name = "Rock";
                sr.sprite = Resources.Load<Sprite>("Sprites/rock");
                go.layer = 10;
                prop.push = true;
                break;
        }
        go.AddComponent<BoxCollider2D>();

        return go;
    }

    List<GameObject> isMoveValid (Vector2 start, Vector2 direction, float distance)
    {
        List<GameObject> movedObjs = new List<GameObject>();
        RaycastHit2D[] hit = Physics2D.RaycastAll(start, direction, distance); // hit[0] will be self, so can be ignored
        Debug.DrawRay(start, direction, Color.red, distance);
        if (hit.Length > 1 && hit[1].collider.gameObject.layer == 9)
        {
            return null;
        }
        else if (hit.Length > 1 && hit[1].collider.gameObject.layer == 10)
        {
            Properties props = hit[1].collider.gameObject.GetComponent<Properties>();
            List<GameObject> pushingObjs = isMoveValid(props.pos, direction, distance);
            if (pushingObjs == null) // Invalid move
            {
                return null;
            }
            else
            {
                movedObjs.Add(hit[1].collider.gameObject);
                movedObjs = movedObjs.Union(pushingObjs).ToList();
                return movedObjs;
            }
        }
        else
        {
            return movedObjs;
        }

    }
}
