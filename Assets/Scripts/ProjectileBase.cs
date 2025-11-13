using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    [SerializeField]
    float launchSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * launchSpeed, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
