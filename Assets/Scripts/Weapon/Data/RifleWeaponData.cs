using UnityEngine;

namespace Weapon.Data
{
    [CreateAssetMenu(menuName = "Weapons/Rifle")]
    public class RifleWeaponData : BaseWeaponData
    {
        private const int MissingAngle = 7;

        public override void Fire(Transform firePoint)
        {
            float angle = Random.Range(-MissingAngle, MissingAngle);
            var bullet = GetBulletToFire(firePoint, angle);
            BackBulletToPool(ref bullet);
        }
    }
}
