using UnityEngine;

public class HitscanPierce : MonoBehaviour
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

        RaycastHit[] hits;
        lineRenderer.SetPosition(0, Vector3.zero);

        hits = Physics.RaycastAll(transform.position, transform.forward, distance);
        if (hits.Length > 0)
        {
            int furthestIndex = 0;
            float furthestDistance = 0;
            for (int i = 0; i < hits.Length; i ++)
            {  
                EnemyHealth HP;
                if (HP = hits[i].collider.gameObject.GetComponent<EnemyHealth>())
                {
                    HP.Damage(damage + owner.damageBoost);
                    owner.damageBoost = 0;
                }

                if (hits[i].distance > furthestDistance)
                {
                    furthestDistance = hits[i].distance;
                    furthestIndex = i;
                }
            }

            lineRenderer.SetPosition(1, transform.InverseTransformPoint(hits[furthestIndex].point));
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
