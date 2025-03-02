namespace CraftSystem.Customs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// ItemEnum determines whether an <see cref="OverallItemType"/> is a <see cref="ItemEnum.ItemType"/>, <see cref="ItemEnum.CustomItem"/>, or <see cref="ItemEnum.None"/>.
    /// </summary>
    public enum ItemEnum
    {
        /// <summary>
        /// No item type.
        /// </summary>
        None,

        /// <summary>
        /// Base game item type.
        /// </summary>
        ItemType,

        /// <summary>
        /// EXILED CustomItem item type.
        /// </summary>
        CustomItem,
    }
}
