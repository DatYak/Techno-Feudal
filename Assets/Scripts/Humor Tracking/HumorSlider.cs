using UnityEngine;
using UnityEngine.UI;

public class HumorSlider : MonoBehaviour
{
    public HumorType type;
    public Slider slider;
    public HumorTracker tracker;


    public void FixedUpdate()
    {
        if (tracker != null)
        {
            slider.value = tracker.GetHumorPercent(type);
        }
    }

    public void IncreaseSlider()
    {
        tracker.ModifyBalance(type, 1);
    }

    public void DecreaseSlider()
    {
        tracker.ModifyBalance(type, -1);
    }

}
