using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    [SerializeField]
    float launchSpeed;

    [SerializeField]
    int damage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * launchSpeed, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        EnemyHealth hp;
        if ( hp = collision.gameObject.GetComponent<EnemyHealth>())
        {
            hp.Damage(damage);
        }

        Destroy(this.gameObject);
    }
}
