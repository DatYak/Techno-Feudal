using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    float maxHP;

    [SerializeField]
    float imbalanceToDamage;

    [SerializeField]
    float balanceToHeal;

    [SerializeField]
    float imbalanceDamageRate;

    [SerializeField]
    float balanceHealRate;

    float currentHP;

    public float currentHPPercent { get { return (float)currentHP / (float)maxHP;}}

    HumorTracker humorTracker;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHP = maxHP;
        humorTracker = GetComponent<HumorTracker>();
    }

    void FixedUpdate()
    {
        float imbalance = humorTracker.GetHumorImbalance();
        if (imbalance > imbalanceToDamage)
        {
            Damage(imbalance * imbalanceDamageRate * Time.deltaTime);
        }
        else if (imbalance < balanceToHeal)
        {
            Damage(-balanceHealRate * Time.deltaTime);
        }
    }

    public void Damage (float damage)
    {
        currentHP -= damage;
    }
}
