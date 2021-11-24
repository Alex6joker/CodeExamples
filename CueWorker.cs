using System;
using System.IO;

namespace Flac_with_CUE_to_Tak
{
    public class CueWorker
    {
        public bool WriteNewExtensionOnCueFile(String CueFilePath, String newExtension)
        {
            string str = string.Empty;
            try
            {
                using (System.IO.StreamReader reader = new StreamReader(CueFilePath, System.Text.Encoding.UTF8, true))
                {
                    str = reader.ReadToEnd();
                }
            }
            catch (Exception)
            {
                return false;
            }
            str = str.Replace(".wav", newExtension);
            str = str.Replace(".tak", newExtension);

            try
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(CueFilePath, false, System.Text.Encoding.UTF8))
                {
                    file.Write(str);
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
