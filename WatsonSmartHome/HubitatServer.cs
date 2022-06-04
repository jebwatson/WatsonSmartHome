using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace WatsonSmartHome
{
    public delegate byte[] ProcessDataDelegate(string data);
    public class HubitatServer
    {
        private const int HandlerThread = 2;
        private readonly HttpListener listener;
        private readonly ProcessDataDelegate handler;
        public HubitatServer(HttpListener listener, string url, ProcessDataDelegate handler)
        {
            this.listener = listener;
            this.handler = handler;
            listener.Prefixes.Add(url);
        }

        public void Start()
        {
            if (this.listener.IsListening) return;
            
            this.listener.Start();

            for (int i = 0; i < HandlerThread; i++)
            {
                this.listener.GetContextAsync().ContinueWith(ProcessRequestHandler);
            }
        }

        public void Stop()
        {
            if (this.listener.IsListening) this.listener.Stop();
        }

        private void ProcessRequestHandler(Task<HttpListenerContext> result)
        {
            var context = result.Result;

            if (!this.listener.IsListening) return;
            
            // Start new listener which replaces this
            this.listener.GetContextAsync().ContinueWith(this.ProcessRequestHandler);
            
            // Read request
            string request = new StreamReader(context.Request.InputStream).ReadToEnd();
            
            // Prepare response
            var responseBytes = this.handler.Invoke(request);
            context.Response.ContentLength64 = responseBytes.Length;

            var output = context.Response.OutputStream;
            output.WriteAsync(responseBytes, 0, responseBytes.Length);
            output.Close();
        }
    }
}