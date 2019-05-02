using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Global.Global;

public class SpriteManager : MonoBehaviour
{
    public GameObject parent;
    Properties props;
    objType lastEffectingType;

    void Start()
    {
        props = parent.GetComponent<Properties>();
        parent.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/wallSide");
    }

    void Update()
    {
        updateSprite();
    }

    void updateSprite()
    {
        if (props.type == objType.Wall)
        {
            List<GameObject> allObjs = GameObject.Find("SceneManager").GetComponent<SceneManager>().gameObjs;
            List<GameObject> walls = GetObjsOfType(allObjs, objType.Wall);
            walls = walls.OrderBy(wall => wall.GetComponent<Properties>().pos.y).ToList();

            for (int i = 0; i < walls.Count; i++)
            {
                Vector3 pos = walls[i].GetComponent<Properties>().pos;
                pos.z = i;
                walls[i].GetComponent<Properties>().pos = pos;
            }
        }
    }
}
