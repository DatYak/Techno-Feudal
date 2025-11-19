using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{

    public PlayerHealth health;
    
    public Slider hpSlider;

    // Update is called once per frame
    void Update()
    {
        hpSlider.value = health.currentHPPercent;
    }
}
