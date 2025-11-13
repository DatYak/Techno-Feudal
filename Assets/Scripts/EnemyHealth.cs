using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    int maxHP;

    [SerializeField]
    Slider hpBar;

    int currentHP;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hpBar.value = 1;
        currentHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(int damage)
    {
        currentHP -= damage;
        hpBar.value = (float)currentHP / (float)maxHP;

        if (currentHP <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
