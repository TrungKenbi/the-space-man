using ObjectPooling;
using UnityEngine;

namespace Weapon.Data.Melee
{
    public abstract class BaseMeleeWeaponData : ScriptableObject
    {
        public Sprite icon;
        public GameObject weaponPrefab;
        public PooledObject hitEffectPrefab;
        
        public AudioClip fireAudio;

        public float fireRate = 0.1f;
        public float fireRange = 2f;
        public int damage = 10;

        public abstract void Fire(Transform firePoint);

        protected PooledObject GetEffectToFire(Transform firePoint, float angle = 0f)
        {
            var bulletRotation = Quaternion.AngleAxis(angle, Vector3.forward) * firePoint.rotation;
            var bullet = Pool.Instance.Spawn(hitEffectPrefab, firePoint.position, bulletRotation);
            
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            if (firePoint.transform.position.x <= mousePos.x)
            {
                Vector3 theScale = bullet.transform.localScale;
                theScale.x = 1;
                bullet.transform.localScale = theScale;
            }
            else
            {
                Vector3 theScale = bullet.transform.localScale;
                theScale.x = -1;
                bullet.transform.localScale = theScale;
            }

            return bullet;
        }

        protected static void BackEffectToPool(ref PooledObject bullet, float time = 2f)
        {
            bullet.FinishDelayed(time);
        }
    }
}
