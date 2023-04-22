using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public interface IDamageable
{
    [SerializeField]
    public float health { get; set; }

    public void TakeDamage(float pDamage);

    public void Die();
}
