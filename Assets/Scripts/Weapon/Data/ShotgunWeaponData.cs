using UnityEngine;

namespace Weapon.Data
{
    [CreateAssetMenu(menuName = "Weapons/Shotgun")]
    public class ShotgunWeaponData : BaseWeaponData
    {
        private const int AngleCone = 7;

        public override void Fire(Transform firePoint)
        {
            int mostRightAngle = AngleCone * (numBulletsToFire / 2);

            for (int i = 0; i < numBulletsToFire; i++)
            {
                float angle = Random.Range(-mostRightAngle, mostRightAngle);
                var bullet = GetBulletToFire(firePoint, angle);
                BackBulletToPool(ref bullet);
            }
        }
    }
}
