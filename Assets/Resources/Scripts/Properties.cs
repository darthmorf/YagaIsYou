﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Global.Global;

public class Properties : MonoBehaviour {

    public GameObject go;
    public Vector2 dest;
    public float destX { get { return dest.x; } set { dest = new Vector2(value, dest.y); } }
    public float destY { get { return dest.y; } set { dest = new Vector2(dest.x, value); } }
    public Vector2 pos { get { return go.transform.position; } set { go.transform.position = value; } }
    public float speed = 6f;
    public objType type;
    //public int layerMask {  get { return ~(1 << go.layer); } }
}
