using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    int maxHP;

    int currentHP;

    public float currentHPPercent { get { return (float)currentHP / (float)maxHP;}}

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHP = maxHP;
    }

    public void Damage (int damage)
    {
        currentHP -= damage;
    }
}
