using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public PlayerMovement playerMovement;
    public HumorTracker humorTracker;

    public bool isParrying;
    public bool isImmune;

    public float parryDamageReduction = 0.5f;

    public int damageBoost = 0;
    public float immunityTimeRemaining;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
        playerMovement = GetComponent<PlayerMovement>();
        humorTracker = GetComponent<HumorTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isImmune)
        {
            immunityTimeRemaining -= Time.deltaTime;
            if (immunityTimeRemaining <= 0)
            {
                immunityTimeRemaining = 0;
                isImmune = false;
            }
        }
    }

    public void AddImmunity(float time)
    {
        if (isImmune == false)
        {
            isImmune = true;
        }

        immunityTimeRemaining += time;
    }

    public void Damage(float damage)
    {
        if (isImmune)
        {
            return;    
        }
        else if (isParrying)
        {
            playerHealth.Damage(damage * parryDamageReduction);
            isParrying = false;
            playerMovement.FailParry();
        }
        else if (playerMovement.lastParryRelease + playerMovement.parryPerfectWindow > Time.time)
        {
            playerMovement.SucceedParry();
        }
        else
        {
            playerHealth.Damage(damage);
        }
    }
}
