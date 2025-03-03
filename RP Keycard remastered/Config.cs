namespace RP_Keycard_remastered
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Exiled.API.Enums;
    using Exiled.API.Interfaces;

    /// <summary>
    /// Plugin's configuration file.
    /// </summary>
    public class Config : IConfig
    {
        /// <inheritdoc/>
        public bool IsEnabled { get; set; } = true;

        /// <inheritdoc/>
        public bool Debug { get; set; } = false;

        /// <summary>
        /// Gets or sets the hint to use as reference for displaying to players.
        /// </summary>
        [Description("%name% translates to the name of the owner. %permissions% translates to the list of permissions. (cAsE sEnSiTiVe!)")]
        public string DisplayHint { get; set; } = "Name: %name%\nClearance Level:\n%permissions%";

        /// <summary>
        /// Gets or sets a dictionary that converts singular <see cref="KeycardPermissions"/> to strings.
        /// </summary>
        [Description("If a permission is missing from this list, it falls back to the enum name.")]
        public Dictionary<KeycardPermissions, string> PermissionsToWord { get; set; } = new Dictionary<KeycardPermissions, string> {
            {KeycardPermissions.ContainmentLevelOne, "Containment Access - 1" } ,
            {KeycardPermissions.ContainmentLevelTwo, "Containment Access - 2" } ,
            {KeycardPermissions.ContainmentLevelThree, "Containment Access - 3" } ,
            {KeycardPermissions.ArmoryLevelOne, "Armory Access - 1" } ,
            {KeycardPermissions.ArmoryLevelTwo, "Armory Access - 2" } ,
            {KeycardPermissions.ArmoryLevelThree, "Armory Access - 3" } ,
            {KeycardPermissions.ExitGates, "Gate Access" } ,
            {KeycardPermissions.AlphaWarhead, "Alpha Warhead Access" } ,
            {KeycardPermissions.Checkpoints, "Checkpoint Access" } ,
            {KeycardPermissions.Intercom, "Intercom Access" } ,
        };
    }
}
