using UnityEngine;
using UnityEngine.AI;

public class SimpleWalkEnemy : EnemyMovement
{
    [SerializeField]
    float followDistance = 6;

    [SerializeField]
    float walkSpeed = 4;

    NavMeshAgent agent;

    GameObject target;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player");

        if (agent)
        {
            agent.speed = walkSpeed;
        }
    }

    public override void ProcessMovement()
    {
        base.ProcessMovement();

        if (target != null && agent != null)
        {
            Vector3 targetPosition = target.transform.position;

            if (Vector3.Distance(targetPosition, transform.position) > followDistance)
            {
                agent.destination = targetPosition - transform.position;
            }
            else
            {
                agent.destination = transform.position;
            }
        }

    }

}
