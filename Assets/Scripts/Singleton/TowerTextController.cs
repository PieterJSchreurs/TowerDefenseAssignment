using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TowerTextController : MonoBehaviour
{
    [SerializeField]
    public SelectedTowerScriptableObject selectedTowerScriptableObject;

    [SerializeField]
    private Text m_singleTargetTowerNameText, m_singleTargetTowerCostText, m_debuffTowerNameText, m_debuffTowerCostText, m_multishotTowerNameText, m_multishotTowerCostText, m_towerInformationText;

    [SerializeField]
    private GameObject m_singleTargetTower, m_debuffTower, m_multiShotTower;

    public void Update()
    {
        if (selectedTowerScriptableObject.selectedTower != null)
        {
            m_towerInformationText.text = "Tower info: \nDamage: " + (selectedTowerScriptableObject.selectedTower.damage + (selectedTowerScriptableObject.selectedTower.level * selectedTowerScriptableObject.selectedTower.towerUpgrade.shootingSpeedIncrease)) + "\nRange: " + (selectedTowerScriptableObject.selectedTower.range + (selectedTowerScriptableObject.selectedTower.level * selectedTowerScriptableObject.selectedTower.towerUpgrade.rangeIncrease)) + "\nSpeed: " + (selectedTowerScriptableObject.selectedTower.shootingSpeed * (Mathf.Pow(selectedTowerScriptableObject.selectedTower.towerUpgrade.shootingSpeedIncrease, selectedTowerScriptableObject.selectedTower.level))); ;
            if (selectedTowerScriptableObject.selectedTower.GetType() == typeof(SingleTargetTower))
            {
                m_singleTargetTowerNameText.text = "Upgrade tower";
                m_singleTargetTowerCostText.text = "Cost : " + selectedTowerScriptableObject.selectedTower.GetUpgradeCost();

                //Reset rest
                m_debuffTowerNameText.text = "";
                m_debuffTowerCostText.text = "";
                m_multishotTowerNameText.text = "";
                m_multishotTowerCostText.text = "";
            }
            else if (selectedTowerScriptableObject.selectedTower.GetType() == typeof(DebuffTower))
            {
                m_debuffTowerNameText.text = "Upgrade tower";
                m_debuffTowerCostText.text = "Cost : " + selectedTowerScriptableObject.selectedTower.GetUpgradeCost();

                //Reset rest
                m_singleTargetTowerNameText.text = "";
                m_singleTargetTowerCostText.text = "";
                m_multishotTowerNameText.text = "";
                m_multishotTowerCostText.text = "";

                
            }
            else if (selectedTowerScriptableObject.selectedTower.GetType() == typeof(MultiShotTower))
            {
                m_multishotTowerNameText.text = "Upgrade tower";
                m_multishotTowerCostText.text = "Cost : " + selectedTowerScriptableObject.selectedTower.GetUpgradeCost();

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
            m_singleTargetTowerCostText.text = "Cost " + m_singleTargetTower.GetComponent<Tower>().cost;
            m_debuffTowerNameText.text = "Build Debuff tower";
            m_debuffTowerCostText.text = "" + m_debuffTower.GetComponent<Tower>().cost;
            m_multishotTowerNameText.text = "Build Multi shot tower" ;
            m_multishotTowerCostText.text = "" + m_multiShotTower.GetComponent<Tower>().cost;
            m_towerInformationText.text = "" ;
        }
    }
}
