using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.CustomItems.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftSystem.Customs
{
    public class OverallItemType
    {
        public void Give(Player player)
        {
            switch (ItemEnum)
            {
                case ItemEnum.ItemType:
                    if((ItemType)ItemId != ItemType.None)
                    {
                        player.AddItem((ItemType)ItemId);
                    }
                    break;
                case ItemEnum.CustomItem:
                    if(CustomItem.TryGet((uint)ItemId, out CustomItem customItem))
                    {
                        customItem.Give(player);
                    }
                    break;
            }
        }


        private ItemEnum ItemEnum;
        private int ItemId;
        public OverallItemType(ItemEnum itemEnum, int itemId)
        {
            ItemEnum = itemEnum;
            ItemId = itemId;
        }

        public OverallItemType(CustomItem item)
        {
            ItemId = (int)item.Id;
            ItemEnum = ItemEnum.CustomItem;
        }
        public OverallItemType(Item item)
        {
            ItemId = (int)item.Type;
            ItemEnum = ItemEnum.ItemType;
        }

        public ItemType UnderlyingVanillaItem 
        { 
            get 
            {
                if (ItemEnum != ItemEnum.ItemType) return ItemType.None;
                if ((ItemType)ItemId != ItemType.None)
                {
                    return (ItemType)ItemId;
                }
                else
                {
                    return ItemType.None;
                }
            } 
        }
        
    }
}
