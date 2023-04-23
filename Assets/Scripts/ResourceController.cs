using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ResourceController : MonoBehaviour
{
    [SerializeField]
    private ResourceValue m_resourceValue;

    [SerializeField]
    private Text m_incomeText;

    private float m_incomeTimer = 0.0f;

    [SerializeField]
    private GameEventEnemyDiedListener m_gameEventEnemyDiedListener;

    // Start is called before the first frame update
    void Start()
    {
        SetResources(m_resourceValue.initialValue);
    }

    private void SetResources(float pResources)
    {
        m_resourceValue.runTimeValue = pResources;
    }

    public bool CanAfford(float pCost)
    {
        if (pCost <= m_resourceValue.runTimeValue)
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
            SetResources(m_resourceValue.runTimeValue - pUpgradeCost);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AddResources(Enemy pKilledEnemy)
    {
        SetResources(m_resourceValue.runTimeValue + pKilledEnemy.killReward);
        m_incomeText.text = pKilledEnemy.killReward.ToString();
        m_incomeTimer = 0.0f;
    }

    public void Update()
    {
        m_incomeTimer += Time.deltaTime;
        if (m_incomeTimer > 0.5f)
        {
            if (m_incomeText != null)
            {
                m_incomeText.text = "";
            }
        }
    }
}
