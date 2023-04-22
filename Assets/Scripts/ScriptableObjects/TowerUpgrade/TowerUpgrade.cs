using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerUpgrade", menuName = "TowerUpgrade")]
public class TowerUpgrade : ScriptableObject
{
    public float upgradeCost, damageIncrease, rangeIncrease, shootingSpeedIncrease;
}
