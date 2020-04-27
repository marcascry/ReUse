using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReUse
{
    public class Option
    {
        /// <summary>
        /// <para>Document</para>
        /// </summary>
        public enum SystemSafe
        {
            Override,
            Skip,
            KeepBoth
        }

        public enum TimeUnit:int
        {
            Second=1,
            Minute=60,
            Hour= 3600,
            Day = 86400,
            Week = 604800,
            Month = 2592000,
            Year = 31536000
        }

        public enum SizeUnit
        {
            B,
            KB,
            MB,
            GB,
            TB,
            PB
        }

        public static int GetLength(Type type)
        {
            return Enum.GetNames(type).Length;
        }

        public static string ConvertEnum(Type to, int index)
        {
            string result = Enum.ToObject(to, index).ToString();
            return result;
        }

        public static int ConvertEnum(Type to, string item)
        {
            int result = (int)Enum.Parse(to, item);
            return result;
        }

    }
}
