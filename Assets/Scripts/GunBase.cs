using UnityEngine;
using UnityEngine.InputSystem;

public class GunBase : MonoBehaviour
{
    [SerializeField]
    protected Color color;

    [SerializeField]
    protected float fireDelay;

    [SerializeField]
    protected Transform spawnPoint;

    [SerializeField]
    protected GameObject projectile;

    float lastFired;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MeshRenderer mr = GetComponentInChildren<MeshRenderer>();
        if (mr != null )
        {
            mr.material.color = color;
        }
    }

    public void FireGun()
    {
        if (lastFired + fireDelay > Time.time)
        {
            return;
        }
        
        lastFired = Time.time;
        
        if (projectile == null)
        {
            Debug.Log("Fired");
            return;
        }
        else
        {
            Instantiate(projectile, spawnPoint.position, spawnPoint.rotation);
        }
    }

    public void ReleaseGun()
    {

    }
}
