﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pawn : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float rotateSpeed = 180.0f;
    public float health = 100f;
    private Transform tf;

    void Start()
    {
        tf = gameObject.GetComponent<Transform>();
    }

    public virtual void Attack()
    {
        Debug.Log("Pawn Attack");
    }

    public virtual void Move()
    {
        Debug.Log("Moved with pawn movement behavior");
    }
}
