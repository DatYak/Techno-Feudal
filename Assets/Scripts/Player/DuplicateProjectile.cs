using UnityEngine;

public class DuplicateProjectile : MonoBehaviour
{

    Rigidbody rb;

    public float launchSpeed;

    public GameObject taunt;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * launchSpeed, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision)
    {
        Instantiate(taunt, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
