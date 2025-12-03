using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class HumorTracker : MonoBehaviour
{
    public float totalFluid = 20;
    public Dictionary<HumorType, Humor> humors;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        humors = new Dictionary<HumorType, Humor>();

        //Start with an equal portion of the starting fluid
        float baseFluid = totalFluid / 4.0f;

        // Fill humor dictionary, and set their balanced values based on weight
        humors.Add(HumorType.Blood, new Humor());
        humors[HumorType.Blood].balancedValue = baseFluid;

        humors.Add(HumorType.Phlegm, new Humor());
        humors[HumorType.Phlegm].balancedValue = baseFluid;

        humors.Add(HumorType.YellowBile, new Humor());
        humors[HumorType.YellowBile].balancedValue = baseFluid;

        humors.Add(HumorType.BlackBile, new Humor());
        humors[HumorType.BlackBile].balancedValue = baseFluid;

        BalanceHumors();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BalanceHumors()
    {
        foreach (Humor h in humors.Values)
        {
            h.currentValue = h.balancedValue;
        }
    }

    public bool IsChangePossible(HumorType target, float delta)
    {
        float newTarget = humors[target].currentValue + delta;

        // The change would put target over max, or put it below 0
        if ( newTarget > totalFluid || newTarget < 0)
        {
            return false;
        }

        // The other humors experience a third of the effect in the other direction
        float deltaOthers = -delta / 3.0f;
        // Make sure changing target doesn't make any other humor go out of bounds
        foreach (HumorType h in humors.Keys)
        {
            if (h == target) continue;

            float newOther = humors[h].currentValue + deltaOthers;

            // The change would put another humor over max, or put it below 0
            if (newOther > totalFluid || newOther < 0)
            {
                return false;
            }
        }

        return true;

    }

    public bool ModifyBalance(HumorType target, float delta)
    {
        if (!IsChangePossible(target, delta))
            { return false; }

        // The other humors experience a third of the effect in the other direction
        float deltaOthers = -delta / 3.0f;
        foreach (HumorType t in humors.Keys)
        {
            if (t == target)
            {
                humors[t].currentValue += delta;
            }
            else
            {
                humors[t].currentValue += deltaOthers;
            }
        }

        return true;
    }

    public float GetHumorPercent(HumorType type)
    {
        return humors[type].currentValue / totalFluid;
    }

    public float GetHumorImbalance()
    {
        float balance = 0;
        foreach (HumorType t in humors.Keys)
        {
            // Add together how far each humor is from balanced. 
            float diff = Mathf.Abs(humors[t].currentValue - humors[t].balancedValue);
            balance += diff;
        }

        Debug.Log (balance);
        return balance;

    }
}

public enum HumorType
{ 
    None = 0,
    Blood = 1,
    Phlegm = 2,
    YellowBile = 3,
    BlackBile = 4
}

[System.Serializable]
public class Humor
{
    public float balancedValue;
    public float currentValue;
}