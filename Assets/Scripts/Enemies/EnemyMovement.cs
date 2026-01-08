using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{

    public float speedMod = 1;

    protected Enemy enemy;

    void Awake()
    {
        enemy = GetComponent<Enemy>();
    }

    void FixedUpdate()
    {
        ProcessMovement();
    }

    public virtual void ProcessMovement()
    {
        
    }
}
