using Unity.AI.Navigation;
using UnityEngine;

public class SpawningProjectile : MonoBehaviour
{

    Rigidbody rb;

    public float launchSpeed;


    [Header("Collision Validation")]
    public bool onlySpawnOnNavmesh;
    
    [Header("Bouncing")]
    public float maxBounces;
    [Tooltip("Should this projectile bounce on valid collisions? If false; the projectile will spawn toSpawn and destroy itself on the first valid collision")]
    public bool willBounceOnValid;
    public float extraBounceForce;


    public GameObject toSpawn;

    [Header("Extras")]
    public bool addRandomAngle;
    public float randomAngleForce;


    private int bounces;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * launchSpeed, ForceMode.Impulse);

        if (addRandomAngle)
        {
            rb.AddTorque(Random.onUnitSphere * randomAngleForce);
        }

    }

    void OnCollisionEnter(Collision collision)
    {

        if (IsCollisionValid(collision))
        {
            if (willBounceOnValid && bounces < maxBounces)
            {
                Bounce(collision);
            }

            else 
            {
                Spawn();
            }
            
        }
        else
        {
            if (bounces < maxBounces)
            {
                Bounce(collision);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

    }

    bool IsCollisionValid(Collision collision)
    {

        if (onlySpawnOnNavmesh)
        {
            if (collision.gameObject.GetComponent<NavMeshSurface>() == null) return false;
        }
        

        return true;
    }

    void Bounce(Collision collision)
    {

        Debug.Log("Bouncing Projectile!");

        Vector3 vel = Vector3.Reflect(rb.linearVelocity, collision.contacts[0].normal);

        vel *= extraBounceForce;

        rb.linearVelocity = vel;

        // rb.AddForce(force, ForceMode.Impulse);

        bounces++;   
    }

    void Spawn()
    {
        Instantiate(toSpawn, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
