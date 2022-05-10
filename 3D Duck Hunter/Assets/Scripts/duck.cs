using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class duck : MonoBehaviour
{

    Rigidbody[] rbs;
    Animator animator;
    public List <GameObject> targets;
    public float moveSpeed;
    bool alive;
    GameObject[] targetArray;

    public int removedTargets;
    int targetClosenessMultipler;
    float randomSpeedMultiplier;


    public ParticleSystem Blood;

    Vector3 lastPos;
    float t;

    void Start()
    {
        animator = gameObject.GetComponentInChildren<Animator>();
        rbs = gameObject.GetComponentsInChildren<Rigidbody>();
        DisableRagdoll();

        //Turn Targets into List---------------------------------------------
        targetArray = GameObject.FindGameObjectsWithTag("Target");
        targets = new List<GameObject>(targetArray);
        //--------------------------------------------------------------------
        alive = true;

        //setting duck specific randomness
        targetClosenessMultipler = Random.Range(0,10);
        randomSpeedMultiplier = Random.Range(0.5f,2f);
        for (int i = 0; i < removedTargets; i++)
        {
            targets.Remove(targetArray[(int)Mathf.Round(Random.Range(0, targetArray.Length))]);
        }
    }

    void Update()
    {
        if (alive == true && PauseGame.GameIsPaused==false)
        {
            if (targets.Count == 0) { Destroy(gameObject); FindObjectOfType<GameOver>().GameIsOver(); } //Destroys Duck if theres no target to go to

            else if (Vector3.Distance(transform.position, NearestTarget(targets).transform.position) < targetClosenessMultipler) //Removes nearest target from list if at targets location
            { targets.Remove(NearestTarget(targets)); }

            else if (Vector3.Distance(transform.position, NearestTarget(targets).transform.position) != 0)
            {
                transform.position = Vector3.MoveTowards(transform.position, NearestTarget(targets).transform.position, moveSpeed*randomSpeedMultiplier*Time.deltaTime);

                if (NearestTarget(targets).transform.position-transform.position!=Vector3.zero)
                { transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(NearestTarget(targets).transform.position - transform.position), Time.deltaTime * 5f); }
            }
        }

        //Backup for Stuck Ducks to Be destroyed-----------------------------------
        if(Time.time - t > 1f) 
        {
            if (lastPos == transform.position && Time.time - t > 1f) { Destroy(gameObject); }
            else { lastPos = transform.position; t = Time.time; }
        }
        //-------------------------------------------------------------

    }

    public void Shot()
    {
        FindObjectOfType<AudioManager>().Play("Quack "+Mathf.Round(Random.Range(1f,3f)),0f);
        Blood.Play();
        EnableRagdoll();
        alive = false;
        Destroy(gameObject, 15f);
    }

    void EnableRagdoll()
    {
        animator.enabled = false;
        foreach (Rigidbody r in rbs)
        {
            r.isKinematic = false;
        }
    }
    void DisableRagdoll()
    {
        animator.enabled = true;
        foreach (Rigidbody r in rbs)
        {
            r.isKinematic = true;
        }
    }
    GameObject NearestTarget(List<GameObject> targets)
    {
        GameObject nearestTarget = null;
        float minDistance = Mathf.Infinity;


        foreach (GameObject n in targets)
        {
            float distance = Vector3.Distance(transform.position, n.transform.position);

            if (distance < minDistance)
            {
                nearestTarget = n;
                minDistance = distance;
            }
        }
        return nearestTarget;
    }

}
