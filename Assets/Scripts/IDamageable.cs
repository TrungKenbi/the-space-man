using System.Collections;
using UnityEngine;

public interface IDamageable
{
    void TakeDamage(int damage);

    void Healing(int health);
}
