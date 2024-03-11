using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceBar : MonoBehaviour
{
    [SerializeField] private Slider slider;

    protected void Lower(float amount)
    {
        slider.value -= amount;
    }

    protected void Raise(float amount)
    {
        slider.value += amount;
        if (slider.value > slider.maxValue)
            slider.value = slider.maxValue;
    }

    protected void SetMax(float amount)
    {
        slider.maxValue = amount;
        slider.value = slider.maxValue;
    }
}
