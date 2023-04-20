using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceManager : MonoBehaviour
{
    [SerializeField]
    public int startingResources = 50;

    public TMP_Text resourceText;

    private int m_currentResources;
    // Start is called before the first frame update
    void Start()
    {
        SetResources(startingResources);
    }

    private void SetResources(int pResources)
    {
        m_currentResources = pResources;
        resourceText.text = "Gold: " + m_currentResources;
    }

    public bool CanAfford(int pCost)
    {
        if(pCost <= m_currentResources)
        {
            return true;
        } else
        {
            return false;
        }
    }

    public bool BuyUpgrade(int pUpgradeCost)
    {
        if(CanAfford(pUpgradeCost))
        {
            SetResources(m_currentResources - pUpgradeCost);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AddResources(int pResources)
    {
        SetResources(m_currentResources + pResources);
    }
}
