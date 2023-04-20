using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffTower : Tower
{
    [SerializeField]
    private int m_cost;

    [SerializeField]
    private float m_range, m_shootingSpeed, m_damage;

    public override float Range { get => m_range; set => m_range = value; }
    public override float ShootingSpeed { get => m_shootingSpeed; set => m_shootingSpeed = value; }

    public override int Cost { get => m_cost; set => m_cost = value; }
    public override float Damage { get => m_damage; set => m_damage = value; }

    public override void OnTriggerExit(Collider pOther)
    {
        base.OnTriggerExit(pOther);
        if (pOther.tag == "Enemy")
        {
            pOther.gameObject.GetComponent<Enemy>().AffectedByStatus(Enemy.STATUS.NORMAL);
        }
    }

    public override void OnTriggerEnter(Collider pOther)
    {
        base.OnTriggerEnter(pOther);
        if (pOther.tag == "Enemy")
        {
            pOther.gameObject.GetComponent<Enemy>().AffectedByStatus(Enemy.STATUS.SLOWED);
        }
    }
}
