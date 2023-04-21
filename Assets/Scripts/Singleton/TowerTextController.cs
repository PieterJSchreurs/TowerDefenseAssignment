using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TowerTextController : MonoBehaviour
{
    [SerializeField]
    public SelectedTowerScriptableObject SelectedTower;

    [SerializeField]
    private Text m_singleTargetTowerNameText, m_singleTargetTowerCostText, m_debuffTowerNameText, m_debuffTowerCostText, m_multishotTowerNameText, m_multishotTowerCostText, m_towerInformationText;

    [SerializeField]
    private GameObject m_singleTargetTower, m_debuffTower, m_multiShotTower;

    public void Update()
    {
        if (SelectedTower.SelectedTower != null)
        {
            m_towerInformationText.text = "Tower info: \nDamage: " + (SelectedTower.SelectedTower.Damage + (SelectedTower.SelectedTower.Level * SelectedTower.SelectedTower.TowerUpgrade.ShootingSpeedIncrease)) + "\nRange: " + (SelectedTower.SelectedTower.Range + (SelectedTower.SelectedTower.Level * SelectedTower.SelectedTower.TowerUpgrade.RangeIncrease)) + "\nSpeed: " + (SelectedTower.SelectedTower.ShootingSpeed * (Mathf.Pow(SelectedTower.SelectedTower.TowerUpgrade.ShootingSpeedIncrease, SelectedTower.SelectedTower.Level))); ;
            if (SelectedTower.SelectedTower.GetType() == typeof(SingleTargetTower))
            {
                m_singleTargetTowerNameText.text = "Upgrade tower";
                m_singleTargetTowerCostText.text = "Cost : " + SelectedTower.SelectedTower.GetUpgradeCost();

                //Reset rest
                m_debuffTowerNameText.text = "";
                m_debuffTowerCostText.text = "";
                m_multishotTowerNameText.text = "";
                m_multishotTowerCostText.text = "";
            }
            else if (SelectedTower.SelectedTower.GetType() == typeof(DebuffTower))
            {
                m_debuffTowerNameText.text = "Upgrade tower";
                m_debuffTowerCostText.text = "Cost : " + SelectedTower.SelectedTower.GetUpgradeCost();

                //Reset rest
                m_singleTargetTowerNameText.text = "";
                m_singleTargetTowerCostText.text = "";
                m_multishotTowerNameText.text = "";
                m_multishotTowerCostText.text = "";

                
            }
            else if (SelectedTower.SelectedTower.GetType() == typeof(MultiShotTower))
            {
                m_multishotTowerNameText.text = "Upgrade tower";
                m_multishotTowerCostText.text = "Cost : " + SelectedTower.SelectedTower.GetUpgradeCost();

                //Reset rest
                m_singleTargetTowerNameText.text = "";
                m_singleTargetTowerCostText.text = "";
                m_debuffTowerNameText.text = "";
                m_debuffTowerCostText.text = "";
            }
        }
        else
        {
            m_singleTargetTowerNameText.text = "Build Single target Tower";
            m_singleTargetTowerCostText.text = "Cost " + m_singleTargetTower.GetComponent<Tower>().Cost;
            m_debuffTowerNameText.text = "Build Debuff tower";
            m_debuffTowerCostText.text = "" + m_debuffTower.GetComponent<Tower>().Cost;
            m_multishotTowerNameText.text = "Build Multi shot tower" ;
            m_multishotTowerCostText.text = "" + m_multiShotTower.GetComponent<Tower>().Cost;
            m_towerInformationText.text = "" ;
        }
    }
}
