using System.Windows.Media;

namespace RedirectDebugMessages.Information
{
    public class MessageObj
    {
        public MessageObj(string message, Color fontColor, Color backGroundColor)
        {
            Message = message;
            ForeGroundColor = new SolidColorBrush(fontColor);
            BackgroundColor = new SolidColorBrush(backGroundColor);
        }

        public MessageObj(string message)
        {
            Message = message;
            ForeGroundColor = new SolidColorBrush(Color.FromRgb(0,0,0));
            BackgroundColor = new SolidColorBrush(Color.FromRgb(byte.MaxValue, byte.MaxValue, byte.MaxValue));
        }

        public string Message { get; set; }
        public SolidColorBrush ForeGroundColor { get; set; }
        public SolidColorBrush BackgroundColor { get; set; }
    }
}
