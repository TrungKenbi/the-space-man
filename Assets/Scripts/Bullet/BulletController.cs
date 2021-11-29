using ECS;
using Enemy;
using ObjectPooling;
using UnityEngine;

namespace Bullet
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class BulletController : MonoBehaviour
    {
        [HideInInspector]
        public Rigidbody2D rb2d;
        [HideInInspector]
        public PooledObject pooledObject;
    
        public PooledObject explosionEffect;
        public int damage = 5;

        public LayerMask layerTakeDamage;

        private void OnValidate()
        {
            if (rb2d == null)
                rb2d = GetComponent<Rigidbody2D>();
            if (pooledObject == null)
                pooledObject = GetComponent<PooledObject>();
        }

        public void Setup()
        {
            // Cài đặt hướng bay của viên đạn, ...
            // Vị trí xuất phát, hướng bay
            // 
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // Check layer in layer take damage
            if (layerTakeDamage == (layerTakeDamage | (1 << collision.gameObject.layer)))
            {
                BaseEntity enemyEntity = collision.gameObject.GetComponent<BaseEntity>();
                enemyEntity.OnTakeDamage?.Invoke(damage);
            }

            // Create explosion effect
            PooledObject effect = Pool.Instance.Spawn(explosionEffect, transform.position, Quaternion.identity);
            effect.FinishDelayed(1f);
            pooledObject.Finish();
        }
    }
}
