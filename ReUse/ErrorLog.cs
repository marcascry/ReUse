using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReUse
{
    public class ErrorLog
    {
        public static String ErrorLogPath = $"{AppDomain.CurrentDomain.BaseDirectory}ErrorLog.log";
        public static int ErrorLogIndex = 1;
        public static void Create()
        {
            try
            {
                if (!File.Exists(ErrorLogPath))
                {
                    File.Create(ErrorLogPath).Close();
                }
            }
            catch
            {

            }
        }

        public static void Write(Exception e, String log)
        {
            try
            {
                Create();
                FileInfo errorLogFile = new FileInfo(ErrorLogPath);
                if (errorLogFile.Length > 1024 * 1024 * 1024)
                {
                    List<String> logLine = new List<String>(File.ReadAllLines(ErrorLogPath));
                    List<String> newLogLine = new List<string>();
                    for (int a = logLine.Count / 2; a < logLine.Count; a++)
                    {
                        newLogLine.Add(logLine[a]);
                    }
                    File.Delete(ErrorLogPath);
                    File.WriteAllLines(ErrorLogPath, newLogLine.ToArray());
                }
                File.AppendAllText(ErrorLogPath, $"{DateTime.Now}: {log}\r\n");
                File.AppendAllText(ErrorLogPath, $"{e.Message}\r\n");
            }
            catch
            {

            }


        }


    }
}
