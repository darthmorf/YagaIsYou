using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Properties : MonoBehaviour {

    public bool push = false;
    public GameObject go;
    public Vector2 dest;
    public Vector2 pos { get { return go.transform.position; } set { go.transform.position = value; } }
    public float speed = 6f;
    public int layerMask = ~(1 << 8);
}
