using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Flac_with_CUE_to_Tak
{
    public class FlacProcessor
    {
        public bool CompressWavFile(String WAVfullPath)
        {
            if (!File.Exists(WAVfullPath))
            {
                new Thread(() => { MessageBox.Show("Файла не существует или он был удалён\n" + WAVfullPath); }).Start();
                return false;
            }

            if (!StartCompession(WAVfullPath))
            {
                return false;
            }
            return true;
        }

        public bool DecompressFlacFile(String fullPath)
        {
            if(!File.Exists(fullPath))
            {
                new Thread(() => { MessageBox.Show("Файла не существует или он был удалён\n" + fullPath); }).Start();
                return false;
            }

            if(!StartDecompession(fullPath))
            {
                return false;
            }
            return true;
        }

        Boolean StartCompession(String fullPath)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo("FLAC\\flac.exe");
            startInfo.Arguments = "-8 " + "\"" + fullPath + "\"";
            Process flacDecompesser = new Process();
            startInfo.WindowStyle = ProcessWindowStyle.Minimized;
            flacDecompesser.StartInfo = startInfo;
            flacDecompesser.Start();
            flacDecompesser.WaitForExit();
            if (flacDecompesser.ExitCode == 0)
            {
                return true;
            }
            else
            {
                // Ошибка при кодировании в .flac
                return false;
            }
        }

        Boolean StartDecompession(String fullPath)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo("FLAC\\flac.exe");
            startInfo.Arguments = "-d -f " + "\"" + fullPath + "\"";
            Process flacDecompesser = new Process();
            startInfo.WindowStyle = ProcessWindowStyle.Minimized;
            flacDecompesser.StartInfo = startInfo;
            flacDecompesser.Start();
            flacDecompesser.WaitForExit();
            if (flacDecompesser.ExitCode == 0)
            {                
                return true;
            }
            else
            {
                // Ошибка при распаковке .flac
                return false;
            }
        }
    }
}
