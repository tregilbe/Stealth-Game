using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Keep track of our transform
    private Transform tf;

    // Keep track of our target location
    public Transform target;

    // Track what state the AI is in
    public string AIState = "Idle";

    // Track enemy health
    public float HitPoints;

    // Track attack range
    public float AttackRange;

    // Track health cutoff
    public float HPCutoff;

    // Track enemy movement speed
    public float speed = 5.0f;

    // Track our healing rate per second
    public float restingHealRate = 1.0f;

    // Track max hitpoints
    public float maxHP;

    // Start is called before the first frame update
    void Start()
    {
        tf = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (AIState == "Idle")
        {
            // Do the state behavior
            Idle();

            // Check for transitions
            if (isInRange())
            {
                ChangeState("Seek");
            }
        }
        else if (AIState == "Rest")
        {
            // Do the state behavior
            Rest();

            // Check for transitions
            if (HitPoints >= HPCutoff)
            {
                ChangeState("Idle");
            }
        }
        else if (AIState == "Seek")
        {
            // Do the state behavior
            Seek();

            // Check for transitions
            if (HitPoints < HPCutoff)
            {
                ChangeState("Rest");
            }
            else if (!isInRange())
            {
                ChangeState("Idle");
            }
        }
        else
        {
            Debug.LogError("State does not exist: " + AIState);
        }
    }

    public void Idle()
    {
        // Do nothing!
    }

    public void Rest()
    {
        // Stand Still
        // Heal
        HitPoints += restingHealRate * Time.deltaTime;

        HitPoints = Mathf.Min(HitPoints, maxHP);
    }

    public void Seek()
    {
        // Move toward player
        Vector3 vectorToTarget = target.position - tf.position;
        tf.position += vectorToTarget.normalized * speed * Time.deltaTime;
    }

    public void ChangeState(string newState)
    {
        AIState = newState;
    }

    public bool isInRange()
    {
        return (Vector3.Distance(tf.position, target.position) <= AttackRange);
    }
}