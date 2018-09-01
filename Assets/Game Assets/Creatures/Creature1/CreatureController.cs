using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class CreatureController : MonoBehaviour {

    Animator anim;
    Transform targetTrans;
    Transform playerPos;
    NavMeshAgent agent;
    bool canMove = false;
    Vector3 lastRechablePos;

    public bool killPlayer = false;

	// Use this for initialization
	void Start () {
        anim = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        playerPos = FindObjectOfType<PlayerC>().transform;

        if (agent == null)
        {
            Debug.Log("No NavMeshAgent");
            Destroy(this);
        }
        agent.stoppingDistance = 1.0f;
        targetTrans = playerPos;
	}
	
	// Update is called once per frame
	void Update () {
        if (canMove)
        {
            NavMeshPath path = new NavMeshPath();
            agent.CalculatePath(targetTrans.position, path);
            // Active path, move to target
            if (path.status == NavMeshPathStatus.PathComplete)
            {
                float lastDist = Vector3.Distance(lastRechablePos, targetTrans.position);
                if (lastDist > 2.0f)
                {
                    lastRechablePos = targetTrans.position;
                }
                anim.SetBool("Sprint", true);
                agent.SetDestination(targetTrans.position);
            } else
            {
                // Invalid / blocked path
                // Move to target's last PathComplete positio
                Debug.Log("Invalid path");
                agent.SetDestination(lastRechablePos);
            }
        }
        float distFromPoint = Vector3.Distance(targetTrans.position, transform.position);
        if (distFromPoint <= 1.0f)
        {
            anim.SetBool("Sprint", false);

            if (targetTrans.GetComponent<CreatureMovePoint>())
            {
                CreatureMovePoint cmp = targetTrans.GetComponent<CreatureMovePoint>();
                Debug.Log("At move point");
                cmp.CreatureAtNode(this);
            }
        }

        float distFromPlayer = Vector3.Distance(playerPos.position, transform.position);
        if (killPlayer)
        {
            // End player if it gets too close
            if (distFromPlayer <= 3.0f)
            {
                if (FindObjectOfType<PlayerC>().playerHiding != true)
                    FindObjectOfType<PlayerC>().EndPlayer();
            }
        }
    }

    public void SetTargetToPlayer()
    {
        Debug.Log("Creature target player");
        targetTrans = FindObjectOfType<PlayerC>().transform;
        lastRechablePos = targetTrans.position;
    }
    public void SetTargetToPoint(CreatureMovePoint point)
    {
        Debug.Log("Creature target new point");
        targetTrans = point.transform;
        lastRechablePos = targetTrans.position;
    }
    public void EnableMove()
    {
        canMove = true;
    }
    public void DisableMove()
    {
        canMove = false;
        anim.SetBool("Sprint", false);
        anim.SetBool("Walk", false);
    }
}
