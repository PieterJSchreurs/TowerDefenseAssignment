using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceManager : MonoBehaviour
{
    [SerializeField]
    public int startingResources;

    public TMP_Text resourceText;

    private int currentResources;
    // Start is called before the first frame update
    void Start()
    {
        SetResources(startingResources);
    }

    private void SetResources(int pResources)
    {
        currentResources = pResources;
        resourceText.text = "Gold: " + currentResources;
    }

    public bool CanAfford(int pCost)
    {
        if(pCost <= currentResources)
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
            SetResources(currentResources - pUpgradeCost);
            return true;
        }
        else
        {
            return false;
        }
    }
}
