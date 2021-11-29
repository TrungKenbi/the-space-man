using ECS;
using UnityEngine;
using UnityEngine.UI;

namespace Enemy
{
    public class EnemyHealth : BaseHealthComponent
    {
        public Image healthBar;

        protected override void Awake()
        {
            base.Awake();
            if (healthBar != null)
                healthBar.fillAmount = 1f;
        }

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);

            if (healthBar != null)
            {
                healthBar.fillAmount = (float)_currentHealth / maxHealth;
            }
        }
    }
}
