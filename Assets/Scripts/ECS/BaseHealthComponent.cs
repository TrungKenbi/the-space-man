namespace ECS
{
    public class BaseHealthComponent : BaseComponent, IDamageable
    {
        public int maxHealth = 100;
        protected int _currentHealth;

        protected virtual void Awake()
        {
            _currentHealth = maxHealth;
        }
        
        public virtual void TakeDamage(int damage)
        {
            int newHealth = _currentHealth - damage;
            if (newHealth <= 0)
            {
                entity.OnDeath?.Invoke();
                _currentHealth = 0;
                gameObject.SetActive(false);
                return;
            }

            if (newHealth > maxHealth)
            {
                _currentHealth = maxHealth;
                return;
            }

            _currentHealth = newHealth;
        }

        public void Healing(int health)
        {
            throw new System.NotImplementedException();
        }
    }
}
