namespace CraftSystem
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using CraftSystem.Customs;
    using Exiled.API.Interfaces;

    /// <summary>
    /// The plugin configuration.
    /// </summary>
    public class Config : IConfig
    {
        /// <inheritdoc/>
        public bool IsEnabled { get; set; } = true;

        /// <inheritdoc/>
        public bool Debug { get; set; } = false;

        [Description("The list of all recipes to register on plugin start.")]
        /// <summary>
        /// The list of all recipes to register on plugin start.
        /// </summary>
        public List<CraftRecipe> Recipes { get; set; } = new ();
    }
}
