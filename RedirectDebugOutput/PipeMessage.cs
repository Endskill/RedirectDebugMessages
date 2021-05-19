using System.Windows.Media;

namespace RedirectDebugOutput
{
    public class PipeMessage
    {
        public PipeMessage(string message, string modName, Color background, Color foreground)
        {
            Message = message;
            ModName = modName;
            BackGroundColor = background;
            ForeGroundColor = foreground;
        }

        public PipeMessage(string message, string modName, Color background)
        {
            Message = message;
            ModName = modName;
            BackGroundColor = background;
            ForeGroundColor = Color.FromRgb(0, 0, 0);
        }

        public PipeMessage(string message, string modName)
        {
            Message = message;
            ModName = modName;
            BackGroundColor = Color.FromRgb(byte.MaxValue, byte.MaxValue, byte.MaxValue);
            ForeGroundColor = Color.FromRgb(0, 0, 0);
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
