namespace RP_Keycard_remastered.Customs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Contains a keycard's data that can't be contained in the keycard itself.
    /// </summary>
    public class KeycardContainer
    {
        /// <summary>
        /// Gets or sets the name associated with this keycard.
        /// </summary>
        public string Name { get; set; } = "John Doe";
    }
}
