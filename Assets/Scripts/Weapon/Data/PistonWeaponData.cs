using UnityEngine;

namespace Weapon.Data
{
    [CreateAssetMenu(menuName = "Weapons/Piston")]
    public class PistonWeaponData : BaseWeaponData
    {
        private const int MissingAngle = 5;

        public override void Fire(Transform firePoint)
        {
            float angle = Random.Range(-MissingAngle, MissingAngle);
            var bullet = GetBulletToFire(firePoint, angle);
            BackBulletToPool(ref bullet);
        }
    }
}
