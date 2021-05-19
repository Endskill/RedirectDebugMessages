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

        public byte ForeR { get; set; }
        public byte ForeG { get; set; }
        public byte ForeB { get; set; }

        public byte BackR { get; set; }
        public byte BackG { get; set; }
        public byte BackB { get; set; }
    }
}
