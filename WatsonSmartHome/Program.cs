using System;
using System.Net;

namespace WatsonSmartHome
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            StartServer();
            Console.ReadKey();
        }

        private static void StartServer()
        {
            var listener = new HttpListener();
            var server = new HubitatServer(listener, "http://192.168.1.237:8567/", ProcessResponse);
            server.Start();
        }

        private static byte[] ProcessResponse(string content)
        {
            Console.WriteLine(content);
            return Array.Empty<byte>();
        }
    }
}