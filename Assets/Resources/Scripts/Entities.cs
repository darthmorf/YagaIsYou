using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity
{
    public enum type
    {
        Yaga, WallH, WallV
    };

    public bool move = true;
    public GameObject go;
    public Vector2 dest;
    public Vector2 pos { get { return go.transform.position; } set { go.transform.position = value; } }
    public float speed = 6f;
    public int layerMask = ~(1 << 8);

    public Entity(type entityType)
    {
        go = new GameObject();
        SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
        dest = pos;

        switch (entityType)
        {
            case type.Yaga:
                go.name = "Yaga";
                sr.sprite = Resources.Load<Sprite>("Sprites/yaga");
                go.layer = 8;
                move = false;
                break;

            case type.WallH:
                go.name = "Wall";
                sr.sprite = Resources.Load<Sprite>("Sprites/wall01");
                move = false;
                break;

            case type.WallV:
                go.name = "Wall";
                sr.sprite = Resources.Load<Sprite>("Sprites/wall02");
                move = false;
                break;
        }
        go.AddComponent<BoxCollider2D>();
    }
}