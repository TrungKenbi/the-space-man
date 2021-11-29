using ECS;
using Item;
using Manager;
using UI;
using UnityEngine;
using Weapon.Data;

namespace Hero
{
    public class HeroEntity : BaseEntity
    {
        [SerializeField]
        private HeroShooting shooting;
        
        protected override void OnValidate()
        {
            base.OnValidate();
            
            foreach (var component in components)
            {
                if (component is HeroShooting heroShooting)
                {
                    shooting = heroShooting;
                }
            }
        }

        protected override void EntityDead()
        {
            Debug.Log("Hero dead");
            GameManager.Instance.GameOver();
        }

        public void ChangeWeapon(BaseWeaponData weapon)
        {
            ItemManager.Instance.DropItem(shooting.weaponData);
            
            shooting.weaponData = weapon;
            shooting.InitWeapon();
        }
    }
}
