using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReUse
{
    public class Log
    {
        public static String LogPath = $"{AppDomain.CurrentDomain.BaseDirectory}Log.txt";
        public static void Create_TDCLogFile()
        {
            try
            {
                if (!File.Exists($"{LogPath}"))
                {
                    File.Create(LogPath).Close();
                }
            }
            catch (Exception e)
            {
                ErrorLog.Write(e, MethodBase.GetCurrentMethod().Name);
            }

        }
        public static void Write(String Log, String Item)
        {
            try
            {
                if (!File.Exists(LogPath))
                {
                    Create_TDCLogFile();
                }

                File.AppendAllText(LogPath, $"{DateTime.Now}: {Item}: {Log}\n");
            }
            catch (Exception e)
            {
                ErrorLog.Write(e, MethodBase.GetCurrentMethod().Name);
            }
        }


    }
}
