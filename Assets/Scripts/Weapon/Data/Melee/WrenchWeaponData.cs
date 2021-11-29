using Enemy;
using UnityEngine;

namespace Weapon.Data.Melee
{
    [CreateAssetMenu(menuName = "Weapons/Melee/Wrench")]
    public class WrenchWeaponData : BaseMeleeWeaponData
    {
        public override void Fire(Transform firePoint)
        {
            int maxColliders = 5;
            Collider2D[] hitColliders = new Collider2D[maxColliders];
            int numColliders = Physics2D.OverlapCircleNonAlloc(firePoint.transform.position, fireRange, hitColliders);
            
            for (int i = 0; i < numColliders; i++)
            {
                var enemyEntity = hitColliders[i].GetComponent<EnemyEntity>();
                if (enemyEntity)
                {
                    enemyEntity.OnTakeDamage(damage);
                }
            }
            
            // Effect
            var bullet = GetEffectToFire(firePoint, 0);
            BackEffectToPool(ref bullet);
        }
    }
}
