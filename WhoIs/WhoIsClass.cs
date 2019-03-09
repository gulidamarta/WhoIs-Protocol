using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Text;

namespace WhoIs
{
    class WhoIsClass
    {
        public static string GetInfo(string domain, ResultType type)
        {
            TcpClient client = new TcpClient();
            client.Connect("whois.networksolutions.com", 43);

            domain += "\r\n";
            byte[] arrDomain = Encoding.UTF8.GetBytes(domain);

            Stream str = client.GetStream();
            str.Write(arrDomain, 0, domain.Length);

            StreamReader reader = new StreamReader(client.GetStream(), Encoding.UTF8);
            string info = "";

            if(type == ResultType.TextBox)
            {
                info = Regex.Replace(reader.ReadToEnd(), "\n", "\r\n");
            }
            else
            {
                info = Regex.Replace(reader.ReadToEnd(), "\n", "<br>");
            }
            return info;
        }   
        
        public enum ResultType
        {
            TextBox, 
            Web
        }
    }
}
