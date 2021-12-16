using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPSSEmu
{
    public class BlocksTypeTextSplitter
    {
        public String ExtractBlockType(String Type)
        {
            return Type.Contains("EQU") || Type.Contains("STORAGE") ? Type.Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[1] :
                Type.Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0];            
        }

        public String[] ExtractBlockParameters(String Type)
        {
            String[] Ret;
            try
            {
                Ret = Type.Split(new Char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (Type.Contains("EQU") || Type.Contains("STORAGE"))
                    Ret = new String[] { Ret[0], Ret[2] };
                else
                    if(Ret.Length > 1)
                        Ret = Ret.Where(w => w != Ret[0]).ToArray();
                    else
                        Ret = new String[0];
            }
            catch (IndexOutOfRangeException ex)
            {
                Ret = null;
            }
            return Ret;
        }
    }
}
