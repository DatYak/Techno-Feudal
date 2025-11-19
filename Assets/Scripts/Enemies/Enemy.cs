using UnityEngine;

[RequireComponent(typeof(EnemyHealth))]
public class Enemy : MonoBehaviour
{   
    public void Die()
    {
        Destroy (this.gameObject);
    }
}
