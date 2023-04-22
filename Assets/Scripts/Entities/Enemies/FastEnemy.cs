using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastEnemy : Enemy
{
    [SerializeField]
    private float m_movementSpeed;
    public override float movementSpeed { get => m_movementSpeed; set => m_movementSpeed = value; }
    [SerializeField]
    private float m_health;
    public override float health { get => m_health; set => m_health = value; }
    [SerializeField]
    private int m_killReward;
    public override int killReward { get => m_killReward; set => m_killReward = value; }
}
