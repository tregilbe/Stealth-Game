using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyPawn))]
public class AIController : Controller
{
    // Start is called before the first frame update
    void Start()
    {
        pawn = gameObject.GetComponent<EnemyPawn>();
    }

    // Update is called once per frame
    void Update()
    {
        pawn.Attack();
    }
}