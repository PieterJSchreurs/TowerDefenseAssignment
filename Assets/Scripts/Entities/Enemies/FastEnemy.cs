using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastEnemy : Enemy
{
    [SerializeField]
    private float m_movementSpeed;
    public override float MovementSpeed { get => m_movementSpeed; set => m_movementSpeed = value; }
    [SerializeField]
    private float m_health;
    public override float Health { get => m_health; set => m_health = value; }
    [SerializeField]
    private int m_killReward;
    public override int KillReward { get => m_killReward; set => m_killReward = value; }
}
