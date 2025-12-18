using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    [SerializeField]
    float launchSpeed;

    [SerializeField]
    int damage;

    Player owner;

    public bool explodeOnImpact = false;
    public float explosionRadius = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        owner = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        GetComponent<Rigidbody>().AddForce(transform.forward * launchSpeed, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (explodeOnImpact == false)
        {
            EnemyHealth hp;
            if ( hp = collision.gameObject.GetComponent<EnemyHealth>())
            {
                hp.Damage(damage + owner.damageBoost);
                owner.damageBoost = 0;
            }
        }
        else
        {
            Collider[] colls = Physics.OverlapSphere(transform.position, explosionRadius);

            foreach (Collider coll in colls)
            {
                EnemyHealth hp;
                if (hp = coll.GetComponent<EnemyHealth>())
                {
                    hp.Damage(damage + owner.damageBoost);
                }   
            }
            //Make sure damage boost effects all targets, and isn't consumed on a total miss
            if (colls.Length > 0)
            {
                owner.damageBoost = 0;
            }
        }

        Destroy(this.gameObject);
    }
}
