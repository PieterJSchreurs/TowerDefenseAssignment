using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowEnemy : Enemy
{
    [SerializeField]
    private float movementSpeed;
    public override float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }
    [SerializeField]
    private float health;
    public override float Health { get => health; set => health = value; }
    [SerializeField]
    private int killReward;
    public override int KillReward { get => killReward; set => killReward = value; }
    // Start is called before the first frame update
    void Start()
    {
        
    }
}
