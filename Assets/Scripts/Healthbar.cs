using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField]
    public Slider m_slider;
    [SerializeField]
    public Gradient m_gradient;
    [SerializeField]
    public RawImage m_fill;

    public void SetMaxHealth(float pHealth)
    {
        m_slider.maxValue = pHealth;
        m_slider.value = pHealth;
    }

    public void SetHealth(float pHealth)
    {
        m_slider.value = pHealth;
        m_fill.color = m_gradient.Evaluate(m_slider.normalizedValue);
    }
}
