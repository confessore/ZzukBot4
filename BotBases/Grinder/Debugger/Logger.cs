using System;
using System.IO;

namespace Grinder.Debugger
{
    class Logger
    {
        static Lazy<Logger> _instance = new Lazy<Logger>(() => new Logger());
        public static Logger Instance => _instance.Value;

        public void LogOne(string logMessage)
        {
            string path = @"/log1.txt";

            using (StreamWriter w = File.AppendText(path))
            {
                w.Write("\r\nLog Entry : ");
                w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
                w.WriteLine("{0}", logMessage);
                w.WriteLine("-------------------------------");
            }
        }
        public void LogTwo(string logMessage)
        {
            string path = @"/log2.txt";

            using (StreamWriter w = File.AppendText(path))
            {
                w.Write("\r\nLog Entry : ");
                w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
                w.WriteLine("{0}", logMessage);
                w.WriteLine("-------------------------------");
            }
        }
    }
}