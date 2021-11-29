using Effect;
using Hero;
using UnityEngine;

namespace Item.Data
{
    [CreateAssetMenu(menuName = "Items/Boots")]
    public class BootsItemData : BaseItemData
    {
        public int speed;
        public EffectLogic bootsEffect;
        
        public override void Activate(Collider2D other)
        {
            var heroEffect = other.GetComponent<HeroEffect>();
            var effect = new Effect.Effect(bootsEffect, speed);
            heroEffect.AddEffect(effect);
        }

        public override void Deactivate(Collider2D other)
        {
            
        }
    }
}
