using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    public Slider slider;
    [SerializeField]
    public Gradient gradient;
    [SerializeField]
    public RawImage fillImage;

    private void Awake()
    {
        if (slider == null)
        {
            slider = GameObject.FindObjectOfType<Slider>();
        }
        if (fillImage == null)
        {
            fillImage = GameObject.FindObjectOfType<RawImage>();
        }
    }
    public void SetMaxHealth(float pHealth)
    {
        slider.maxValue = pHealth;
        slider.value = pHealth;
    }

    public void SetHealth(float pHealth)
    {
        slider.value = pHealth;
        fillImage.color = gradient.Evaluate(slider.normalizedValue);
    }
}
