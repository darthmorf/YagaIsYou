﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global;
using static Global.Global;

public class StateManager : MonoBehaviour {

    public GameObject parent;
    Properties props;
    objType lastEffectingType;

    void Start()
    {
        props = parent.GetComponent<Properties>();
    }

    // Update is called once per frame
    void Update () {
        RaycastHit2D[] ruleHit = Physics2D.RaycastAll(props.pos, new Vector2(1,0), squareSize);
        RaycastHit2D[] objHit = Physics2D.RaycastAll(props.pos, new Vector2(-1, 0), squareSize);
        List<GameObject> allObjs = GameObject.Find("SceneManager").GetComponent<SceneManager>().gameObjs;

        if (ruleHit.Length > 1 && objHit.Length > 1)
        {
            GameObject rule = ruleHit[1].collider.gameObject;
            GameObject obj  = objHit[1].collider.gameObject;
            Properties ruleProp = rule.GetComponent<Properties>();
            Properties objProp  = obj.GetComponent<Properties>();

            if (ruleProp.type == objType.PushRule)
            {
                objType typeToSet = objType.None;
                switch (objProp.type)
                {
                    case objType.RockType:
                        typeToSet = objType.Rock;
                        break;
                }
                List<GameObject> effectedObjs = GetObjsOfType(allObjs, typeToSet);
                foreach (GameObject go in effectedObjs)
                {
                    go.layer = 10;
                }
                lastEffectingType = typeToSet;
            }
        }
        else
        {
            List<GameObject> effectedObjs = GetObjsOfType(allObjs, lastEffectingType);
            foreach (GameObject go in effectedObjs)
            {
                go.layer = 13;
            }
            lastEffectingType = objType.None;
        }
    }
}
