using Hero;
using UnityEngine;

namespace Item.Data
{
    [CreateAssetMenu(menuName = "Items/Health")]
    public class HealthItemData : BaseItemData
    {
        public int hp;
        
        public override void Activate(Collider2D other)
        {
            var heroEntity = other.GetComponent<HeroEntity>();
            heroEntity.OnHealing.Invoke(hp);
        }

        public override void Deactivate(Collider2D other)
        {
            
        }
    }
}
