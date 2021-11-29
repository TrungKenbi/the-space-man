using Effect;
using Hero;
using UI;
using UnityEngine;

namespace Item.Data
{
    [CreateAssetMenu(menuName = "Items/Bullet")]
    public class BulletItemData : BaseItemData
    {
        public int numBullet = 120;
        
        public override void Activate(Collider2D other)
        {
            var heroShooting = other.GetComponent<HeroShooting>();
            heroShooting.totalBulletsCount += numBullet;
            heroShooting.RechargeBullet();
        }

        public override void Deactivate(Collider2D other)
        {
            
        }
    }
}
