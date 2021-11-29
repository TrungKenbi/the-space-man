using ECS;
using Hero;
using UnityEngine;

namespace Effect.Logic
{
    [CreateAssetMenu(menuName = "Effects/Boots")]
    public class BootsEffect : EffectLogic
    {
        private HeroMovement GetMovementComponent(BaseEntity entity)
        {
            HeroMovement heroMovement = entity.GetComponent<HeroMovement>();
            return heroMovement;
        }
        
        public override void Activation(BaseEntity entity)
        {
            var heroMovement = GetMovementComponent(entity);
            if (heroMovement != null)
            {
                heroMovement.moveSpeed += 5;
            }
        }

        public override void Tick(BaseEntity entity)
        {
            
        }

        public override void Deactivation(BaseEntity entity)
        {
            var heroMovement = GetMovementComponent(entity);
            if (heroMovement != null)
            {
                heroMovement.moveSpeed -= 5;
            }
        }
    }
}
