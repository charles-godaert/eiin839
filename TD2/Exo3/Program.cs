using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace Exo4and5
{
    internal class Program
    {
        private static void Main(string[] args)
        {

            //if HttpListener is not supported by the Framework
            if (!HttpListener.IsSupported)
            {
                Console.WriteLine("A more recent Windows version is required to use the HttpListener class.");
                return;
            }


            // Create a listener.
            HttpListener listener = new HttpListener();

            // Add the prefixes.
            if (args.Length != 0)
            {
                foreach (string s in args)
                {
                    listener.Prefixes.Add(s);
                    // don't forget to authorize access to the TCP/IP addresses localhost:xxxx and localhost:yyyy 
                    // with netsh http add urlacl url=http://localhost:xxxx/ user="Tout le monde"
                    // and netsh http add urlacl url=http://localhost:yyyy/ user="Tout le monde"
                    // user="Tout le monde" is language dependent, use user=Everyone in english 

                }
            }
            else
            {
                Console.WriteLine("Syntax error: the call must contain at least one web server url as argument");
            }
            listener.Start();

            // get args 
            foreach (string s in args)
            {
                Console.WriteLine("Listening for connections on " + s);
            }

            // Trap Ctrl-C on console to exit 
            Console.CancelKeyPress += delegate {
                // call methods to close socket and exit
                listener.Stop();
                listener.Close();
                Environment.Exit(0);
            };


            while (true)
            {
                // Note: The GetContext method blocks while waiting for a request.
                HttpListenerContext context = listener.GetContext();
                HttpListenerRequest request = context.Request;

                string documentContents;
                using (Stream receiveStream = request.InputStream)
                {
                    using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                    {
                        documentContents = readStream.ReadToEnd();
                    }
                }

                // get url 
                Console.WriteLine($"\nReceived request for {request.Url}");

                //get url protocol
                Console.WriteLine($"URL protocol : {request.Url.Scheme}");
                //get user in url
                Console.WriteLine($"User info : {request.Url.UserInfo}");
                //get host in url
                Console.WriteLine($"Host : {request.Url.Host}");
                //get port in url
                Console.WriteLine($"Port : {request.Url.Port}");
                //get path in url 
                Console.WriteLine($"Path : {request.Url.LocalPath}");

                // parse path in url 
                Console.WriteLine("Parse path :");
                foreach (string str in request.Url.Segments)
                {
                    Console.WriteLine(str);
                }

                //get params un url. After ? and between &

                Console.WriteLine($"All urls : {request.Url.Query}");

                //parse params in url
                String param1 = HttpUtility.ParseQueryString(request.Url.Query).Get("param1");
                String param2 = HttpUtility.ParseQueryString(request.Url.Query).Get("param2");

                Console.WriteLine("\tparam1 = " + param1);
                Console.WriteLine("\tparam2 = " + param2);

               
                Console.WriteLine($"documentContents : {documentContents}");

                // Obtain a response object.
                HttpListenerResponse response = context.Response;

                String content = "Nothing to show";

                if (request.Url.Segments[request.Url.Segments.Length-1].Contains("method1")) {
                    content = method1(param1, param2);
                }
                if (request.Url.Segments[request.Url.Segments.Length - 1].Contains("Exo5_Externe"))
                {
                    content = Exo5_Externe(param1, param2);
                }
                returnHTML(response, content);
            }
            // Httplistener neither stop ... But Ctrl-C do that ...
            // listener.Stop();
        }

        static String method1(String param1, String param2)
        {
            return $"<HTML><BODY> Hello {param1} and {param2}</BODY></HTML>";
        }

        static String Exo5_Externe(String param1, String param2)
        {
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = @"C:\Users\Charles\Documents\Drive\Polytech\SI4\S8\Serv.Oriented Computing - WS\TD\eiin839\TD2\Exo5_Externe\bin\Debug\Exo5_Externe.exe";
            start.Arguments = $"{param1} {param2}";
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            //
            // Start the process.
            //
            StringBuilder builder = new StringBuilder();
            using (Process process = Process.Start(start))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    builder.Append(reader.ReadToEnd());
                }
            }
            return builder.ToString();
        }

        static void returnHTML(HttpListenerResponse response, String content)
        {
            // Construct a response.
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(content);
            // Get a response stream and write the response to it.
            response.ContentLength64 = buffer.Length;
            System.IO.Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            // You must close the output stream.
            output.Close();
        }
    }
}