using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float fieldOfView = 45f;
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
        CanHear(GameManager.instance.player);
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

    public bool CanHear(GameObject target)
    {
        // Get the noisemaker from our target
        NoiseMaker noise = target.GetComponent<NoiseMaker>();
        // If there is a noisemaker, we can potentially hear the target
        if (noise != null)
        {
            float adjustedVolumeDistance = noise.volumeDistance - Vector3.Distance(tf.position, target.transform.position);
            // if we are close enough, we heard the noise
            if (adjustedVolumeDistance > 0)
            {
                Debug.Log("I heard a noise");
                return true;
            }
        }
        return false;
    }

    public bool CanSee(GameObject target)
    {
        // Find the vector from the agent to the target
        // We do this by subtracting "destination minus origin", so that "origin plus vector equals destination."
        Vector3 agentToTargetVector = target.transform.position - transform.position;
        Vector3 vectorToTarget = target.transform.position - tf.position;
        // Detect if target is inside FOV
        float angleToTarget = Vector3.Angle(vectorToTarget, tf.right);
        if (angleToTarget <= fieldOfView)
        {
            // Detect if target is in line of sight, if they are in FOV
            // Raycast
            RaycastHit2D hitInfo = Physics2D.Raycast(tf.position, agentToTargetVector);

            // If the first object we hit is our target 
            if (hitInfo.collider.gameObject == target)
            {
                // return true 
                //    -- note that this will exit out of the function, so anything after this functions like an else
                Debug.Log("I saw something");
                return true;
            }
        }
        //   -- note that because we returned true when we determined we could see the target, 
        //      this will only run if we hit nothing or if we hit something that is not our target.
        return false;
    }
}