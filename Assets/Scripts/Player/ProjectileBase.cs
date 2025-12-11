using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    [SerializeField]
    float launchSpeed;

    [SerializeField]
    int damage;

    Player owner;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        owner = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        GetComponent<Rigidbody>().AddForce(transform.forward * launchSpeed, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        EnemyHealth hp;
        if ( hp = collision.gameObject.GetComponent<EnemyHealth>())
        {
            hp.Damage(damage + owner.damageBoost);
            owner.damageBoost = 0;
        }

        Destroy(this.gameObject);
    }
}
