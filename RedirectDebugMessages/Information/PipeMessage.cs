using System.Windows.Media;

namespace RedirectDebugMessages.Information
{
    public class PipeMessage
    {
        public PipeMessage(string message, string modName)
        {
            Message = message;
            ModName = modName;
        }

        /// <summary>
        /// Gets or sets the Message you want to Log
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the corresponding Mod in which it should get logged
        /// </summary>
        public string ModName { get; set; }

        /// <summary>
        /// Gets or sets the Background Colour of this logged Message
        /// </summary>
        public Color ForeGroundColor { get; set; }

        /// <summary>
        /// Gets or sets the Background Colour of this logged Message
        /// </summary>
        public Color BackGroundColor { get; set; }
    }
}
