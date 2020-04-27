using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReUse
{
    public class TimeTool
    {
        public static String Get()
        {
            return $"{DateTime.Today.ToString("yyMMdd")}{DateTime.Now.TimeOfDay.ToString("hhmmss")}";
            //yy MM dd hh mm ss
        }

        public static long GetTimeStamp()
        {
            TimeSpan ts = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc));
            return (long)ts.TotalMilliseconds;
        }


    }
}
