using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IDamageable
{
    public int health { get; set; }

    public void TakeDamage(int pDamage);

    public void Die();
}
