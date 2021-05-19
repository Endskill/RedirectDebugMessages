using Newtonsoft.Json;
using RedirectDebugMessages.Information;
using System;
using System.IO;
using System.IO.Pipes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RedirectDebugMessages.BusinessLogic
{
    public class CommunicationService
    {
        private readonly UnicodeEncoding _streamEncoding;
        private const string _PIPE_NAME = "DebugRedirection";

        public CommunicationService()
        {
            _streamEncoding = new UnicodeEncoding();
        }

        public EventHandler<PipeMessage> ReceivedMessage;

        public Task StartServerStackableAsync()
        {
            while (true)
            {
                //try
                //{
                //    //254 is the max Mods that can write into here
                //    
                //}
                //catch(Exception e)
                //{

                //}
                var pipe = new NamedPipeServerStream(_PIPE_NAME, PipeDirection.InOut, 1);
                pipe.WaitForConnection();
                try
                {
                    _ = ReadFromPipeAsync(pipe);
                }
                catch(Exception e)
                {

                }
            }
        }

        /// <summary>
        /// Reads all Messages of <paramref name="pipe"/> and invokes <see cref="ReceivedMessage"/> with the read Message
        /// </summary>
        private async Task ReadFromPipeAsync(NamedPipeServerStream pipe, CancellationToken token = default)
        {
            while (pipe.IsConnected)
            {
                try
                {
                    //Using asyncronous Code here, to not block any other Code running somewhere else
                    var message = await ReadMessage((Stream)pipe);

                    ReceivedMessage.Invoke(pipe, JsonConvert.DeserializeObject<PipeMessage>(message));
                }
                catch (IOException)
                {
                    pipe.Disconnect();
                    pipe.Dispose();
                    return;
                }
            }
        }

        private async Task<string> ReadMessage(Stream stream)
        {
            int len = 0;

            len = stream.ReadByte() * 256;
            len += stream.ReadByte();
            byte[] inBuffer = new byte[len];
            await stream.ReadAsync(inBuffer, 0, len);

            return _streamEncoding.GetString(inBuffer);
        }

        ///// <summary>
        ///// Writes <paramref name="message"/> into <see cref="_currentPipe"/>
        ///// </summary>
        ///// <param name="sendObject"></param>
        ///// <returns>If <paramref name="message"/>was succesfully sent</returns>
        //public bool SendMessage(string sendObject)
        //{
        //    if (IsConnected)
        //    {
        //        var bytesOfMessage = Encoding.Default.GetBytes(sendObject);
        //        _currentPipe.Write(bytesOfMessage, 0, bytesOfMessage.Length);
        //        _currentPipe.WaitForPipeDrain();
        //    }

        //    return _currentPipe.IsConnected;
        //}
    }
}
