namespace CraftSystem.Customs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using AdminToys;
    using Exiled.API.Features.Toys;
    using UnityEngine;

    /// <summary>
    /// A class to represent a crafting table.
    /// </summary>
    public class CraftingTable
    {
        /// <summary>
        /// The primitive object toy. TODO: remove when better thing is found.
        /// </summary>
        private Primitive primitiveObjectToy;

        /// <summary>
        /// The position of the crafting table.
        /// </summary>
        public Vector3 Position
        {
            get
            {
                return primitiveObjectToy.Position;
            }

            set
            {
                primitiveObjectToy.Position = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CraftingTable"/> class.
        /// </summary>
        /// <param name="position">The position to spawn at.</param>
        /// <param name="primitiveType">The <see cref="PrimitiveType"/> to use.</param>
        public CraftingTable(Vector3 position, PrimitiveType primitiveType)
        {
            this.primitiveObjectToy = Primitive.Create(primitiveType, position, Vector3.zero, Vector3.one, true);
            Plugin.CraftingTables.Add(this);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CraftingTable"/> class.
        /// </summary>
        /// <param name="primitiveObjectToy">The primitive object toy to use.</param>
        public CraftingTable(Primitive primitiveObjectToy)
        {
            this.primitiveObjectToy = primitiveObjectToy;
            Plugin.CraftingTables.Add(this);
        }
    }
}
