using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BasicServerHTTPlistener
{
    public class Headers
    {
        private NameValueCollection headers;

        public Headers(HttpListenerRequest HttpListenerRequest)
        {
            headers = HttpListenerRequest.Headers;
        }

        public String PrintHeaders()
        {
            StringBuilder builder = new StringBuilder();

            for(int i = 0; i < headers.Count; i++)
            {
                builder.Append(headers.GetKey(i));
                builder.Append(" : ");
                builder.Append(headers.Get(i));
                builder.AppendLine();
            }

            Console.WriteLine(builder.ToString());
            return builder.ToString();
        }
    }
}
