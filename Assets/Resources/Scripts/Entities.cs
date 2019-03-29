using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity
{
    public bool move = true;
    public bool player = false;
    public GameObject go;
    public Vector2 destination;
    public float speed = 6f;

    public Entity(string entityName, string spriteFile)
    {
        go = new GameObject();
        go.name = entityName;
        SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
        sr.sprite = Resources.Load<Sprite>(spriteFile);
        destination = go.transform.position;
    }
}