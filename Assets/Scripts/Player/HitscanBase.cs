using UnityEngine;

public class HitscanBase : MonoBehaviour
{

    [SerializeField]
    protected float distance;

    [SerializeField]
    protected int damage;

    [SerializeField]
    LineRenderer lineRenderer;

    [SerializeField]
    float lineDuration;

    float spawnTime;

    Player owner;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        owner = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        RaycastHit Hit;
        lineRenderer.SetPosition(0, Vector3.zero);

        if (Physics.Raycast(transform.position, transform.forward, out Hit, distance))
        {
            EnemyHealth HP;
            if (HP = Hit.collider.gameObject.GetComponent<EnemyHealth>())
            {
                HP.Damage(damage + owner.damageBoost);
                owner.damageBoost = 0;
            }
            lineRenderer.SetPosition(1, transform.InverseTransformPoint(Hit.point));
        }
        else
        {
            lineRenderer.SetPosition(1, transform.InverseTransformPoint(transform.position + transform.forward * distance));
        }

        spawnTime = Time.time;
    }

    private void Update()
    {
        if (Time.time >  spawnTime + lineDuration) 
        {
            Destroy(this.gameObject);
        }
    }
}
