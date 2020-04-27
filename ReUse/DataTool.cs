using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReUse
{
    public class DataTool
    {
        /// <summary>
        /// Convert String to Int
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static int StoI(string input)
        {
            int result = 0;
            try
            {
                int startPoint = input.LastIndexOf("=")+1;
                string getData = input.Substring(startPoint);
                result = int.Parse(getData);
                return result;
            }
            catch
            {
                return result;
            }
            
        }

        /// <summary>
        /// Convert String to Boolean
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool StoB(string input)
        {
            bool result = false;
            try
            {
                int startPoint = input.LastIndexOf("=") + 1;
                string getData = input.Substring(startPoint);
                result = Boolean.Parse(getData);
                return result;
            }
            catch
            {
                return result;
            }

        }

        /// <summary>
        /// Convert String to String
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string StoS(string input)
        {
            string result = "";
            try
            {
                int startPoint = input.LastIndexOf("=") + 1;
                result = input.Substring(startPoint);
                return result;
            }
            catch
            {
                return result;
            }

        }

    }
}
