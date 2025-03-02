namespace CraftSystem.Customs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Exiled.API.Features;
    using Exiled.API.Features.Items;
    using Exiled.CustomItems.API.Features;

    /// <summary>
    /// Describes an Item Type which can be from BaseGame or EXILED CustomItems.
    /// </summary>
    public class OverallItemType
    {
        /// <summary>
        /// The <see cref="itemEnum"/> representing which type of Item this is.
        /// </summary>
        private ItemEnum itemEnum;

        /// <summary>
        /// The ID of the item in its corresponding item system.
        /// </summary>
        private int itemId;

        /// <summary>
        /// Initializes a new instance of the <see cref="OverallItemType"/> class.
        /// </summary>
        /// <param name="iEnum">The <see cref="ItemEnum"/> describing which item system this type comes from.</param>
        /// <param name="iId">The ID of this item in its corresponding item system.</param>
        public OverallItemType(ItemEnum iEnum, int iId)
        {
            itemEnum = iEnum;
            itemId = iId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OverallItemType"/> class based on the provided <see cref="CustomItem"/>.
        /// </summary>
        /// <param name="item">The provided <see cref="CustomItem"/>.</param>
        public OverallItemType(CustomItem item)
        {
            itemId = (int)item.Id;
            itemEnum = ItemEnum.CustomItem;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OverallItemType"/> class based on the provided <see cref="Item"/>.
        /// </summary>
        /// <param name="item">The provieded <see cref="Item"/>.</param>
        public OverallItemType(Item item)
        {
            itemId = (int)item.Type;
            itemEnum = ItemEnum.ItemType;
        }

        /// <summary>
        /// Gets the underlying <see cref="ItemType"/> described by this <see cref="OverallItemType"/>, or <see cref="ItemType.None"/>.
        /// </summary>
        public ItemType UnderlyingVanillaItemType
        {
            get
            {
                if (itemEnum != ItemEnum.ItemType)
                {
                    return ItemType.None;
                }

                if ((ItemType)itemId != ItemType.None)
                {
                    return (ItemType)itemId;
                }
                else
                {
                    return ItemType.None;
                }
            }
        }

        /// <summary>
        /// Gives the player an instance of the underlying item.
        /// </summary>
        /// <param name="player">The player being given the item.</param>
        public void Give(Player player)
        {
            switch (itemEnum)
            {
                case ItemEnum.ItemType:
                    if ((ItemType)itemId != ItemType.None)
                    {
                        player.AddItem((ItemType)itemId);
                    }

                    break;
                case ItemEnum.CustomItem:
                    if (CustomItem.TryGet((uint)itemId, out CustomItem customItem))
                    {
                        customItem.Give(player);
                    }

                    break;
            }
        }
    }
}
