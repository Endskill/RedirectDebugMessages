using Newtonsoft.Json;
using System;
using System.IO;
using System.IO.Pipes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;

namespace RedirectDebugOutput
{
    /// <summary>
    /// Handles sending Messages
    /// </summary>
    public class ExternalLogger : Il2CppSystem.Object
    {
        private static ExternalLogger _current;
        private const string _PIPE_NAME = "DebugRedirection";
        private readonly bool _isActive;
        private readonly UnicodeEncoding _streamEncoding = new UnicodeEncoding();

        public ExternalLogger()
        {
            if(_current != null)
            {
                throw new Exception("Generating a new Instance of ExternalLogger is not safe! Please use the static Current Property");
            }

            _isActive = BepInExLoader.IsActiveLocal.Value;

            _current = this;
        }

        /// <summary>
        /// The Single Instance of <see cref="ExternalLogger"/>
        /// </summary>
        public static ExternalLogger Current 
        { 
            get
            {
                if(_current is null)
                {
                   _ = new ExternalLogger();
                }

                return _current;
            } 
        }

        public NamedPipeClientStream ClientPipe { get; private set; }

        /// <summary>
        /// Tries to send a Message to the PC <see cref="_pcName"/>
        /// I don't recommend awaiting this Message!
        /// </summary>
        /// <returns>If the Message was sent Successfull</returns>
        public async Task<bool> SendMessageAsync(string message, string modName, UnityEngine.Color backGroundColor, UnityEngine.Color foreGroundColor)
        {
            return await SendMessageInternalAsync(new PipeMessage(message, modName, UnityColorToSystemMedia(backGroundColor), UnityColorToSystemMedia(foreGroundColor)));
        }

        /// <summary>
        /// Tries to send a Message to the PC <see cref="_pcName"/>
        /// I don't recommend awaiting this Message!
        /// </summary>
        /// <returns>If the Message was sent Successfull</returns>
        public async Task<bool> SendMessageAsync(string message, string modName, UnityEngine.Color backGroundColor)
        {
            return await SendMessageInternalAsync(new PipeMessage(message, modName, UnityColorToSystemMedia(backGroundColor)));
        }

        /// <summary>
        /// Tries to send a Message to the PC <see cref="_pcName"/>
        /// I don't recommend awaiting this Message!
        /// </summary>
        /// <returns>If the Message was sent Successfull</returns>
        public async Task<bool> SendMessageAsync(string message, string modName)
        {
            return await SendMessageInternalAsync(new PipeMessage(message, modName));
        }

        /// <summary>
        /// Tries to send a Message to the PC <see cref="_pcName"/>
        /// I don't recommend awaiting this Message!
        /// </summary>
        /// <returns>If the Message was sent Successfull</returns>
        public async Task<bool> SendMessageAsync(PipeMessage message)
        {
           return await SendMessageInternalAsync(message);
        }

        private async Task<bool> SendMessageInternalAsync(PipeMessage message)
        {
            TempLog.Debug("Received Log request");

            if(!_isActive)
            {
                return false;
            }

            if(ClientPipe is null)
            {
                TempLog.Debug("Client was Null");
                ClientPipe = new NamedPipeClientStream((BepInExLoader.NetworkingPcName.Value.ToLower() == "localhost" ? "." : BepInExLoader.NetworkingPcName.Value),
                            _PIPE_NAME, PipeDirection.Out);
            }

            if(!ClientPipe.IsConnected)
            {
                TempLog.Debug("Connecting Client");
                try
                {
                    ClientPipe.Connect(2);
                }
                catch(Exception e)
                {
                    TempLog.Error(e.Message);
                }
                if (!ClientPipe.IsConnected)
                {
                    return false;
                }
            }

            TempLog.Debug("Pipe is Connected");

            //TODO Send Message
            var stream = (Stream)ClientPipe;
            var jsonMessage = JsonConvert.SerializeObject(message);

            await SendMessageAsync(stream, jsonMessage);

            return true;
        }

        private object _lockObject = new object();

        private async Task<bool> SendMessageAsync(Stream stream, string message)
        {
            //Working with Monitor.Enter to not Flush any other Messages
            try
            {
                Monitor.Enter(_lockObject);
                byte[] outBuffer = _streamEncoding.GetBytes(message);
                int len = outBuffer.Length;
                if (len > UInt16.MaxValue)
                {
                    len = (int)UInt16.MaxValue;
                }

                TempLog.Debug("Monitor Entered and Message is bytes");

                stream.WriteByte((byte)(len / 256));
                stream.WriteByte((byte)(len & 255));
                await stream.WriteAsync(outBuffer, 0, len);
                await stream.FlushAsync();

                TempLog.Debug("Stream written Bytes");

                return true;
            }
            catch (Exception)
            {
                TempLog.Debug("SendMessage failed");
                return false;
            }
            finally
            {
                Monitor.Exit(_lockObject);
            }
        }

        private static Color UnityColorToSystemMedia(UnityEngine.Color value)
        {
            return Color.FromArgb(FloatToByte(value.a), FloatToByte(value.r), FloatToByte(value.g), FloatToByte(value.b));
        }

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
