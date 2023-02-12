using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public interface IDamageable
{
    [SerializeField]
    public int health { get; set; }

    public void TakeDamage(int pDamage);

    public void Die();
}
