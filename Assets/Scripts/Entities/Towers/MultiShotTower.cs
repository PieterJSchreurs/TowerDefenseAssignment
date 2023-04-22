using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiShotTower : Tower
{
    [SerializeField]
    private int m_cost, m_level = 0;

    [SerializeField]
    private float m_range, m_shootingSpeed, m_damage;

    //[SerializeField]
    //private TextMeshPro m_textCost;

    //public override TextMeshPro TextCost { get => m_textCost; set => m_textCost = value; }

    public override float range { get => m_range; set => m_range = value; }
    public override float shootingSpeed { get => m_shootingSpeed; set => m_shootingSpeed = value; }

    public override int cost { get => m_cost; set => m_cost = value; }
    public override float damage { get => m_damage; set => m_damage = value; }

    public override int level { get => m_level; set => m_level = value; }

    public override void Attack()
    {
        m_enemyTarget = null;
        base.Attack();
        foreach (Enemy enemy in m_targetList)
        {
            enemy.TakeDamage(damage * (level + towerUpgrade.damageIncrease));
            LineRendererController lineRendererController = new LineRendererController();
            lineRendererController.SetupLine(this.gameObject.transform, enemy.transform);
        }
    }
}
