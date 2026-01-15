using UnityEngine;

[RequireComponent(typeof(EnemyHealth))]
public class Enemy : MonoBehaviour
{   

    public GameObject target;

    public EnemyMovement movement;

    public HumorType humorType = HumorType.None;
    public float humorIntensity = 0;

    void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        movement = GetComponent<EnemyMovement>();
    }

    public void Taunt(GameObject target)
    {
        this.target = target;
    }

    public void EndTaunt()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    public void Die()
    {
        Destroy (this.gameObject);
    }
}
