using System.Collections;
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
        updateLayer();
    }

    void updateLayer()
    {
        RaycastHit2D[] ruleHit = Physics2D.RaycastAll(props.pos, new Vector3(1, 0), squareSize);
        RaycastHit2D[] objHit = Physics2D.RaycastAll(props.pos, new Vector3(-1, 0), squareSize);
        List<GameObject> allObjs = GameObject.Find("SceneManager").GetComponent<SceneManager>().gameObjs;

        if (lastEffectingType != null)
        {
            List<GameObject> effectedObjs = GetObjsOfType(allObjs, lastEffectingType);
            foreach (GameObject go in effectedObjs)
            {
                go.layer = 13;
            }
            lastEffectingType = objType.None;
        }

        if (ruleHit.Length > 1 && objHit.Length > 1)
        {
            GameObject rule = ruleHit[1].collider.gameObject;
            GameObject obj = objHit[1].collider.gameObject;
            Properties ruleProp = rule.GetComponent<Properties>();
            Properties objProp = obj.GetComponent<Properties>();

            int layer = 0;

            switch (ruleProp.type)
            {
                case objType.StopRule:
                    layer = 9;
                    break;

                case objType.PushRule:
                    layer = 10;
                    break;
            }

            objType typeToSet = objType.None;
            switch (objProp.type)
            {
                case objType.RockType:
                    typeToSet = objType.Rock;
                    break;

                case objType.WallType:
                    typeToSet = objType.Wall;
                    break;
            }
            List<GameObject> effectedObjs = GetObjsOfType(allObjs, typeToSet);
            foreach (GameObject go in effectedObjs)
            {
                go.layer = layer;
            }
            lastEffectingType = typeToSet;
        }
    }
}
