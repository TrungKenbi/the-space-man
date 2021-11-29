using ECS;
using Item;
using Manager;
using UnityEngine;

namespace Enemy
{
    public class EnemyEntity : BaseEntity
    {
        public BaseItemData[] dropItems;
        
        protected override void EntityDead()
        {
            ItemManager.Instance.EntityDeadDropItems(dropItems, transform.position);
        }
    }
}
