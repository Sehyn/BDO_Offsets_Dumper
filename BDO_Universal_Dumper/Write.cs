using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BDO_Universal_Dumper
{
    class Write
    {
        public static void W(string s, bool NewLine)
        {
            var Path = DateTime.Now.ToString("yyyy-MM-dd") + " Offsets.txt";

            if (NewLine)
            {
                File.AppendAllText(Path, System.Environment.NewLine + s);
            }
            else
            {
                File.AppendAllText(Path, s);
            }

        }

    }
}
