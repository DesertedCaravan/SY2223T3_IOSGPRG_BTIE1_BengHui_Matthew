using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashBar : MonoBehaviour
{
    public Slider slider;

    public void SetStartingGauge(int amount)
    {
        slider.maxValue = amount;
    }

    public void SetGauge(int amount)
    {
        slider.value += amount;
    }

    public void RestartGauge()
    {
        slider.value = 0;
    }
}