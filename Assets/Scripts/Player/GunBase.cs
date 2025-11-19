using UnityEngine;
using UnityEngine.InputSystem;

public class GunBase : MonoBehaviour
{
    [SerializeField]
    protected Color color;

    [SerializeField]
    protected float fireDelay;

    [SerializeField]
    protected bool isAutomatic;

    private bool isHeld;

    [SerializeField]
    protected Transform spawnPoint;

    [SerializeField]
    protected GameObject projectile;

    float lastFired;

    HumorTracker humorTracker;

    [SerializeField]
    protected HumorType humor;

    [SerializeField]
    protected float humorCost;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        humorTracker = transform.root.GetComponent<HumorTracker>();

        MeshRenderer mr = GetComponentInChildren<MeshRenderer>();
        if (mr != null )
        {
            mr.material.color = color;
        }
    }

    private void FixedUpdate()
    {
        if (isHeld && isAutomatic)
        {
            FireProjectile();
        }
    }

    public void FireGun()
    {
        if (!isAutomatic)
        {
            FireProjectile();
        }
        else
        {
            isHeld = true;
        }
    }

    public void ReleaseGun()
    {
        isHeld = false;
    }

    private void FireProjectile()
    {

        if (lastFired + fireDelay > Time.time)
        {
            return;
        }

        if (humorTracker.IsChangePossible(humor, humorCost) == false)
        {
            return;
        }

        humorTracker.ModifyBalance(humor, humorCost);

        lastFired = Time.time;

        if (projectile == null)
        {
            Debug.Log("Fired without projectile");
            return;
        }
        else
        {
            Instantiate(projectile, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
