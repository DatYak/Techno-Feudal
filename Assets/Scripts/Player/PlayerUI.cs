using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{

    public PlayerHealth health;
    public HumorTracker playerHumors;
    
    public Slider hpSlider;

    public HumorPieChart humorUI;

    // Update is called once per frame
    void Update()
    {
        hpSlider.value = health.currentHPPercent;
        humorUI.SetValues(playerHumors.GetAllHumorPercents());
    }
}
