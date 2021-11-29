using System;
using DG.Tweening;
using ECS;
using UI;
using UnityEngine;

namespace Hero
{
    public class HeroHealth : BaseComponent, IDamageable
    {
        public int maxHealth = 5;
        public AudioClip damageHeroAudio;

        private int _currentHealth;

        public int Health
        {
            get => _currentHealth;

            set
            {
                UIManager.Instance.SetHealth(value);
                _currentHealth = value;
            }
        }

        private void Awake()
        {
            Health = maxHealth;
        }

        public void TakeDamage(int damage)
        {
            Debug.Log($"Hero HP -{damage}");
            
            // Play sound when take damage
            (entity.GetComponent<AudioSource>()).PlayOneShot(damageHeroAudio);
            Camera.main.DOShakePosition(0.05f, 0.15f, 10, 90, false);
            
            int newHealth = Health - damage;
            if (newHealth <= 0)
            {
                entity.OnDeath?.Invoke();
                Health = 0;
                Destroy(gameObject);
                return;
            }

            if (newHealth > maxHealth)
            {
                Health = maxHealth;
                return;
            }

            Health = newHealth;
        }

        public void Healing(int health)
        {
            if (health <= 0)
                return;

            if (Health + health > maxHealth)
            {
                Health = maxHealth;
                return;
            }

            Health += health;
        }
    }
}
