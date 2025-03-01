using Exiled.API.Features;
using InventorySystem.Items.Usables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftSystem
{
    public class Plugin : Plugin<Config>
    {
        public override string Name => "icedchqi's Crafting System";
        public override string Author => "icedchqi";
        public override string Prefix => "crafting";
        public override Version Version => new Version(1, 0, 0);
        public static Plugin instance = null;
        public override void OnEnabled()
        {
            instance = this;
            base.OnEnabled();
            RegisterEvents();
        }
        public override void OnDisabled()
        {
            instance = null;
            base.OnDisabled();
            UnregisterEvents();
        }
        public void RegisterEvents()
        {
        }
        public void UnregisterEvents()
        {
        }
    }
}
