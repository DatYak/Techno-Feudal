using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class SimpleShootEnemy : MonoBehaviour
{
    [SerializeField]
    float fireDistance;

    [SerializeField]
    float fireDelay;

    float lastFired;

    [SerializeField]
    EnemyProjectile projectile;

    private Enemy enemy;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemy = GetComponent<Enemy>();   
        lastFired = -fireDelay;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, enemy.target.transform.position);

        if (distance < fireDistance && lastFired + fireDelay < Time.time)
        {
            lastFired = Time.time;
            Quaternion rotation = Quaternion.LookRotation(enemy.target.transform.position - transform.position);
            EnemyProjectile proj = Instantiate(projectile, transform.position + transform.up, rotation);

            proj.humorType = enemy.humorType;
            proj.humorIntensity = enemy.humorIntensity;
        }
    }
}
