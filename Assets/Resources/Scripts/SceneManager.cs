using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Global.Global;

public class SceneManager : MonoBehaviour {

    public GameObject player;
    public Properties playerProp;
    public List<GameObject> gameObjs = new List<GameObject>();

    // Use this for initialization
    void Start ()
    {
        player = initObj(objType.Yaga, new Vector3(0, 0, -1));
        playerProp = player.GetComponent<Properties>();
        gameObjs.Add(player);

        gameObjs.Add(initObj(objType.Wall, new Vector3(0, 0.8f)));
        gameObjs.Add(initObj(objType.Wall, new Vector3(0.8f, 0.8f)));
        gameObjs.Add(initObj(objType.Wall, new Vector3(2.4f, 0.8f)));
        gameObjs.Add(initObj(objType.Wall, new Vector3(2.4f, 1.6f)));
        gameObjs.Add(initObj(objType.Wall, new Vector3(2.4f, 2.4f)));
        gameObjs.Add(initObj(objType.Wall, new Vector3(0f, -0.8f)));
        gameObjs.Add(initObj(objType.Wall, new Vector3(0f, -1.6f)));

        gameObjs.Add(initObj(objType.Rock,  new Vector3(-2.4f, 0)));
        gameObjs.Add(initObj(objType.Rock,  new Vector3(-4.0f, 0)));

        gameObjs.Add(initObj(objType.RockType, new Vector3(-5.6f, 3.2f)));
        gameObjs.Add(initObj(objType.IsSetter, new Vector3(-4.8f, 3.2f)));
        gameObjs.Add(initObj(objType.PushRule, new Vector3(-4.0f, 3.2f)));

        gameObjs.Add(initObj(objType.WallType, new Vector3(-5.6f, 1.6f)));
        gameObjs.Add(initObj(objType.IsSetter, new Vector3(-4.8f, 1.6f)));
        gameObjs.Add(initObj(objType.StopRule, new Vector3(-4.0f, 1.6f)));
    }
	
	// Update is called once per frame
	void Update ()
    {
        direction touchDirection = TouchControls();
        Movement(touchDirection);
	}

    void Movement (direction touchDirection)
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || touchDirection == direction.up)
        {
            List<GameObject> movedObjs = isMoveValid(playerProp.pos, new Vector3(0, 1), squareSize);
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
        else if (Input.GetKeyDown(KeyCode.DownArrow) || touchDirection == direction.down)
        {
            List<GameObject> movedObjs = isMoveValid(playerProp.pos, new Vector3(0, -1), squareSize);
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
        else if (Input.GetKeyDown(KeyCode.RightArrow) || touchDirection == direction.right)
        {
            List<GameObject> movedObjs = isMoveValid(playerProp.pos, new Vector3(1, 0), squareSize);
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
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || touchDirection == direction.left)
        {
            List<GameObject> movedObjs = isMoveValid(playerProp.pos, new Vector3(-1, 0), squareSize);
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
            props.pos = Vector3.Lerp(props.pos, props.dest, props.speed * Time.deltaTime);
        }
    }

    bool swiping = false;
    bool eventSent = false;
    Vector2 lastPosition;
    enum direction { up, down, left, right, none };

    direction TouchControls()
    {
        if (Input.touchCount == 0) return direction.none;

        if (Input.GetTouch(0).deltaPosition.sqrMagnitude != 0)
        {
            if (swiping == false)
            {
                swiping = true;
                lastPosition = Input.GetTouch(0).position;
                return direction.none;
            }
            else
            {
                if (!eventSent)
                {
                    Vector2 direction_ = Input.GetTouch(0).position - lastPosition;

                    if (Mathf.Abs(direction_.x) > Mathf.Abs(direction_.y))
                    {
                        if (direction_.x > 0)
                        {
                            eventSent = true;
                            return direction.right;
                        }
                        else
                        {
                            eventSent = true;
                            return direction.left;
                        }
                    }
                    else
                    {
                        if (direction_.y > 0)
                        {
                            eventSent = true;
                            return direction.up;
                        }

                        else
                        {
                            eventSent = true;
                            return direction.down;
                        }
                    }
                }
            }
        }
        else
        {
            swiping = false;
            eventSent = false;
        }
        return direction.none;
    }

    GameObject initObj(objType entityType, Vector3 position)
    {
        GameObject go = new GameObject();
        Properties prop = go.AddComponent<Properties>();
        prop.go = go;
        prop.pos = position;
        SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
        prop.dest = prop.pos;
        prop.type = entityType;
        go.layer = 13;

        switch (entityType)
        {
            case objType.Yaga:
                go.name = "Yaga";
                sr.sprite = Resources.Load<Sprite>("Sprites/yaga");
                go.layer = 8;
                break;

            case objType.Wall:
                go.name = "Wall";
                sr.sprite = Resources.Load<Sprite>("Sprites/wallTop");
                SpriteManager sp = go.AddComponent<SpriteManager>();
                sp.parent = go;
                break;

            case objType.Rock:
                go.name = "Rock";
                sr.sprite = Resources.Load<Sprite>("Sprites/rock");
                break;

            case objType.RockType:
                go.name = "RockType";
                sr.sprite = Resources.Load<Sprite>("Sprites/rules/rock");
                go.layer = 14;
                break;

            case objType.WallType:
                go.name = "WallType";
                sr.sprite = Resources.Load<Sprite>("Sprites/rules/wall");
                go.layer = 14;
                break;

            case objType.IsSetter:
                go.name = "IsSetter";
                sr.sprite = Resources.Load<Sprite>("Sprites/rules/is");
                StateManager st = go.AddComponent<StateManager>();
                st.parent = go;
                go.layer = 14;
                break;

            case objType.PushRule:
                go.name = "PushRule";
                sr.sprite = Resources.Load<Sprite>("Sprites/rules/push");
                go.layer = 14;
                break;

            case objType.StopRule:
                go.name = "StopRule";
                sr.sprite = Resources.Load<Sprite>("Sprites/rules/stop");
                go.layer = 14;
                break;
        }
        go.AddComponent<BoxCollider2D>();

        return go;
    }

    List<GameObject> isMoveValid (Vector3 start, Vector3 direction, float distance)
    {
        List<GameObject> movedObjs = new List<GameObject>();
        RaycastHit2D[] hit = Physics2D.RaycastAll(start, direction, distance); // hit[0] will be self, so can be ignored
        //Debug.DrawRay(start, direction, Color.red, distance);
        if (hit.Length > 1 && hit[1].collider.gameObject.layer == 9)
        {
            return null;
        }
        else if (hit.Length > 1 && (hit[1].collider.gameObject.layer == 10 || hit[1].collider.gameObject.layer == 14))
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
