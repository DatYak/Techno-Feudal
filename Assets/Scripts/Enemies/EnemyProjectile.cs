using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyProjectile : MonoBehaviour
{
    [SerializeField]
    float speed;

    [SerializeField]
    float distance;

    [SerializeField]
    int damage;

    Vector3 spawnPoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Rigidbody body = GetComponent<Rigidbody>();

        if (body) body.linearVelocity = transform.forward * speed; 

        spawnPoint = transform.position;

        Debug.Log ("Projectile spawned");
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, spawnPoint) > distance)
        {
            //Probably want to display a VFX here
            Destroy (this.gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerHealth hit = other.gameObject.GetComponent<PlayerHealth>();
            hit.Damage(damage);
        }
        else if (other.gameObject.tag != "Enemy")
        {
            Debug.Log ("Projectile destroyed");
            Destroy(this.gameObject);
        }
    }
}
