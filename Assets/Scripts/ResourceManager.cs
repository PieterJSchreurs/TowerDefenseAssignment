using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceManager : MonoBehaviour
{
    [SerializeField]
    private ResourceValue m_resourceValue;

    // Start is called before the first frame update
    void Start()
    {
        SetResources(m_resourceValue.InitialValue);
    }

    private void SetResources(float pResources)
    {
        m_resourceValue.RuntimeValue = pResources;
    }

    public bool CanAfford(float pCost)
    {
        if (pCost <= m_resourceValue.RuntimeValue)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool BuyUpgrade(float pUpgradeCost)
    {
        if (CanAfford(pUpgradeCost))
        {
            SetResources(m_resourceValue.RuntimeValue - pUpgradeCost);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AddResources(int pResources)
    {
        SetResources(m_resourceValue.RuntimeValue + pResources);
    }
}
