using UnityEngine;
using UnityEngine.AI;

public class SimpleWalkEnemy : EnemyMovement
{
    [SerializeField]
    float followDistance = 6;

    [SerializeField]
    float walkSpeed = 4;

    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (agent)
        {
            agent.speed = walkSpeed;
        }
    }

    public override void ProcessMovement()
    {
        base.ProcessMovement();

        if (enemy.target != null && agent != null)
        {
            Vector3 targetPosition = enemy.target.transform.position;

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
