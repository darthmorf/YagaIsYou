using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Global.Global;

public class Properties : MonoBehaviour {

    public GameObject go;
    public Vector3 dest;
    public float destX { get { return dest.x; } set { dest = new Vector3(value, dest.y, dest.z); } }
    public float destY { get { return dest.y; } set { dest = new Vector3(dest.x, value, dest.z); } }
    public float destZ { get { return dest.z; } set { dest = new Vector3(dest.x, dest.y, value); } }
    public Vector3 pos { get { return go.transform.position; } set { go.transform.position = value; } }
    public float speed = 6f;
    public objType type;
    //public int layerMask {  get { return ~(1 << go.layer); } }
}
