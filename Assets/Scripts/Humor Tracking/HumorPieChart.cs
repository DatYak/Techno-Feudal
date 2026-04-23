using UnityEngine;
using UnityEngine.UI;

public class HumorPieChart : MonoBehaviour
{

    public Image[] imagesPieChart;
    public float[] values;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetValues(values);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetValues(float[] valuesToSet)
    {
        float totalValues = 0;
        for(int i = 0; i < imagesPieChart.Length; i++)
        {
            totalValues += FindPercentage(valuesToSet, i);   
            imagesPieChart[i].fillAmount = totalValues;
        }
    }

    public float FindPercentage(float[] valuesToSet, int index){
        float totalAmmount = 0;
        for (int i = 0; i < valuesToSet.Length; i++)
        {
            totalAmmount += valuesToSet[i];
        }

        return valuesToSet[index] / totalAmmount;
    }
}
