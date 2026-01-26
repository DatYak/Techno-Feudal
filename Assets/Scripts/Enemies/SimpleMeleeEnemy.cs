using UnityEngine;

public class SimpleMeleeEnemy : MonoBehaviour
{
    
    [SerializeField] [Tooltip("Damage of each attack")]
    float attackDamage;

    [SerializeField] [Tooltip ("How close to initiate an attack")]
    float attackDistance;

    [SerializeField] [Tooltip ("How long before dealing damage after getting in range")]
    float attackWindup;

    [SerializeField] [Tooltip ("Time after completing an attack before initiating a new one")]
    float attackDelay;

    //time the last attack was initiated
    float lastAttack;

    bool hasAttacked = false;

    private Enemy enemy;


    void Start()
    {
        enemy = GetComponent<Enemy>();   
        lastAttack = -(attackDelay + attackWindup);
    }
    void Update()
    { 
        float distance = Vector3.Distance(transform.position, enemy.target.transform.position);
        
        if (distance < attackDistance && lastAttack + attackDelay + attackWindup < Time.time)
        {
            //Start attack windup
            lastAttack = Time.time;
            hasAttacked = false;
        }

        if (lastAttack + attackWindup < Time.time && !hasAttacked)
        {
            if (distance < attackDistance)
            {
                // we are still in range, deal damage
                enemy.target.GetComponent<Player>().Damage(attackDamage);
            }

            hasAttacked = true;
        }
    }
}
