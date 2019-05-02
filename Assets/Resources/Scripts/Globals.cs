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
            Yaga, Wall, Rock, RockRule, WallRule, IsSetter, PushRule, StopRule, None
        };

        public class layers
        {
            public const int player = 8;
            public const int stop   = 9;
            public const int push   = 10;
            public const int death  = 11;
            public const int win    = 12;
            public const int none   = 13;
            public const int rule   = 14;
        }

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
