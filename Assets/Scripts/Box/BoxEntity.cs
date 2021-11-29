using ECS;
using Item;
using Manager;
using UnityEngine;

namespace Box
{
    public class BoxEntity : BaseEntity
    {
        public BaseItemData[] dropItems;
        
        protected override void EntityDead()
        {
            ItemManager.Instance.EntityDeadDropItems(dropItems, transform.position);
        }
    }
}
