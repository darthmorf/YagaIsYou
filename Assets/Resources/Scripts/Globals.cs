using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Global
{
    public class Global
    {
        public const float squareSize = 0.8f;
        public enum objType
        {
            Yaga, WallH, WallV, Rock, RockType, IsSetter, PushRule, None
        };

        public static List<GameObject> GetObjsOfType(List<GameObject> objects, objType type)
        {
            List<GameObject> matches = new List<GameObject>();
            foreach (GameObject go in objects)
            {
                Properties prop = go.GetComponent<Properties>();
                if (prop.type == type)
                {
                    matches.Add(go);
                }
            }
            return matches;
        }
    }  

}
