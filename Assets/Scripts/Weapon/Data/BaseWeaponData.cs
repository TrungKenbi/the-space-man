using ObjectPooling;
using UnityEngine;

namespace Weapon.Data
{
    public abstract class BaseWeaponData : ScriptableObject
    {
        public Sprite icon;
        public GameObject weaponPrefab;
        public PooledObject bulletPrefab;
        public float bulletSpeed = 10f;
        public int numBulletsToFire = 1;
        public float fireRate = 0.1f;
        public float reloadTime = 1f;
        public int cartridgeCapacity = 10;
        public int maxBullets = 30;
        public AudioClip fireAudio;
        public AudioClip fireReloadAudio;

        public abstract void Fire(Transform firePoint);

        protected PooledObject GetBulletToFire(Transform firePoint, float angle = 0f)
        {
            var bulletRotation = Quaternion.AngleAxis(angle, Vector3.forward) * firePoint.rotation;
            var bullet = Pool.Instance.Spawn(bulletPrefab, firePoint.position, bulletRotation);
            var rb2d = bullet.As<Bullet.BulletController>().rb2d;
            rb2d.AddForce(rb2d.transform.up * bulletSpeed, ForceMode2D.Impulse);
            return bullet;
        }

        protected static void BackBulletToPool(ref PooledObject bullet, float time = 2f)
        {
            bullet.FinishDelayed(time);
        }
    }
}
