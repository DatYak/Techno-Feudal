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

    public HumorType humorType = HumorType.None;

    public float humorIntensity = 0;

    public Material baseMat;
    public Material phlegmMat;
    public Material bloodMat;
    public Material yBileMat;
    public Material bBileMat;

    Vector3 spawnPoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Rigidbody body = GetComponent<Rigidbody>();

        if (body) body.linearVelocity = transform.forward * speed; 

        spawnPoint = transform.position;

        MeshRenderer model = GetComponentInChildren<MeshRenderer>();
        if (model)
            switch(humorType)
            {
                case HumorType.None:
                    model.material = baseMat;
                    break;
                case HumorType.Phlegm:
                    model.material = phlegmMat;
                    break;
                case HumorType.Blood:
                    model.material = bloodMat;
                    break;
                case HumorType.YellowBile:
                    model.material = yBileMat;
                    break;
                case HumorType.BlackBile:
                    model.material = bBileMat;
                    break;
            }
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
            Player hit = other.gameObject.GetComponent<Player>();
            hit.Damage(damage);

            if (humorType != HumorType.None)
            {
                hit.humorTracker.ModifyBalance(humorType, humorIntensity, true);
            }
        }
        else if (other.gameObject.tag != "Enemy")
        {
            Debug.Log ("Projectile destroyed");
            Destroy(this.gameObject);
        }
    }
}
