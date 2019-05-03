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

    void Update () {
        updateLayer();
    }

    void updateLayer()
    {
        // Send out a ray left and right to look for type to effect, and the effect to grant
        RaycastHit2D[] ruleHit = Physics2D.RaycastAll(props.pos, new Vector3(1, 0), squareSize);
        RaycastHit2D[] objHit = Physics2D.RaycastAll(props.pos, new Vector3(-1, 0), squareSize);

        List<GameObject> allObjs = GameObject.Find("SceneManager").GetComponent<SceneManager>().gameObjs;

        // removes all effects from the type we were previously effecting
        if (lastEffectingType != null)
        {
            List<GameObject> effectedObjs = GetObjsOfType(allObjs, lastEffectingType);
            foreach (GameObject go in effectedObjs)
            {
                go.layer = layers.none;
            }
            lastEffectingType = objType.None;
        }

        // if there is some object to the left and right
        if (ruleHit.Length > 1 && objHit.Length > 1)
        {
            GameObject rule = ruleHit[1].collider.gameObject; // TODO these should check for the first applicable object not the 2nd one. (ie ignore none layer objects)
            GameObject obj = objHit[1].collider.gameObject;
            Properties ruleProp = rule.GetComponent<Properties>();
            Properties objProp = obj.GetComponent<Properties>();

            int layer = 0;

            // determine the destination layer corresponding to the adjacent rule
            switch (ruleProp.type)
            {
                case objType.StopRule:
                    layer = layers.stop;
                    break;

                case objType.PushRule:
                    layer = layers.push;
                    break;
            }

            // determine the object type corresponding to the adjacent type
            objType typeToSet = objType.None;
            switch (objProp.type)
            {
                case objType.RockRule:
                    typeToSet = objType.Rock;
                    break;

                case objType.WallRule:
                    typeToSet = objType.Wall;
                    break;
            }
            
            // set all objects of that type to the destination rule layer
            List<GameObject> effectedObjs = GetObjsOfType(allObjs, typeToSet);
            foreach (GameObject go in effectedObjs)
            {
                go.layer = layer;
            }
            lastEffectingType = typeToSet;
        }
    }
}
