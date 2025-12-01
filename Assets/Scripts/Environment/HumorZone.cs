using UnityEngine;

public class HumorZone : MonoBehaviour
{
    [SerializeField]
    HumorType typeToEffect;

    [SerializeField]
    private float humorModification;

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<HumorTracker>().ModifyBalance(typeToEffect, humorModification * Time.deltaTime);
        }
        
    }
}
