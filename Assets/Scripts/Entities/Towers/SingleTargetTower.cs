using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SingleTargetTower : Tower
{
    [SerializeField]
    private int m_cost, m_level;

    [SerializeField]
    private float m_range, m_shootingSpeed, m_damage;

    //[SerializeField]
    //private TextMeshPro m_textCost;

    public override float Range { get => m_range; set => m_range = value; }
    public override float ShootingSpeed { get => m_shootingSpeed; set => m_shootingSpeed = value; }

    public override int Cost { get => m_cost; set => m_cost = value; }
    public override float Damage { get => m_damage; set => m_damage = value; }

    public override int Level { get => m_level; set => m_level = value; }

    //public override TextMeshPro TextCost { get => m_textCost; set => m_textCost = value; }
}
