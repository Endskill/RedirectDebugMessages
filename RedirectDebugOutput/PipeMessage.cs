using System;

namespace RedirectDebugOutput
{
    public class PipeMessage
    {
        public PipeMessage(string message, string modName, UnityEngine.Color background, UnityEngine.Color foreground)
        {
            Message = message;
            ModName = modName;

            ForeR = FloatToByte(foreground.r);
            ForeG = FloatToByte(foreground.g);
            ForeB = FloatToByte(foreground.b);

            BackR = FloatToByte(background.r);
            BackG = FloatToByte(background.g);
            BackB = FloatToByte(background.b);
        }

        public PipeMessage(string message, string modName, UnityEngine.Color background)
        {
            Message = message;
            ModName = modName;

            ForeR = byte.MinValue;
            ForeG = byte.MinValue;
            ForeB = byte.MinValue;

            BackR = FloatToByte(background.r);
            BackG = FloatToByte(background.g);
            BackB = FloatToByte(background.b);
        }

        public PipeMessage(string message, string modName)
        {
            Message = message;
            ModName = modName;

            ForeR = byte.MinValue;
            ForeG = byte.MinValue;
            ForeB = byte.MinValue;

            BackR = byte.MaxValue;
            BackG = byte.MaxValue;
            BackB = byte.MaxValue;
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

        private static byte FloatToByte(float value)
        {
            if (value < 0f)
            {
                return 0;
            }
            else if (value > 1f)
            {
                return byte.MaxValue;
            }

            var test = value * byte.MaxValue;
            var test2 = Convert.ToByte(test);

            return Convert.ToByte(value * byte.MaxValue);
        }
    }
}
