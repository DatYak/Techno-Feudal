using UnityEngine;

[RequireComponent(typeof(EnemyHealth))]
public class Enemy : MonoBehaviour
{   

    public GameObject target;

    void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    public void Die()
    {
        Destroy (this.gameObject);
    }
}
