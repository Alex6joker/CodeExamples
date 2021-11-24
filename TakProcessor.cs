using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Flac_with_CUE_to_Tak
{
    public class TakProcessor
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

        public bool DecompressTakFile(String TAKfullPath)
        {
            if (!File.Exists(TAKfullPath))
            {
                new Thread(() => { MessageBox.Show("Файла не существует или он был удалён\n" + TAKfullPath); }).Start();
                return false;
            }

            if (!StartDecompession(TAKfullPath))
            {
                return false;
            }
            return true;
        }

        Boolean StartCompession(String fullPath)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo("TAK\\takc.exe");
            startInfo.Arguments = "-e -pMax -wm1 -md5 -overwrite -v " + "\"" + fullPath + "\"" + " " +
                "\"" + Path.GetDirectoryName(fullPath) + "\\" + Path.GetFileNameWithoutExtension(fullPath) + ".tak" + "\"";
            Process TakCompressor = new Process();
            startInfo.WindowStyle = ProcessWindowStyle.Minimized;
            TakCompressor.StartInfo = startInfo;
            TakCompressor.Start();
            TakCompressor.WaitForExit();
            if (TakCompressor.ExitCode == 0)
            {
                return true;
            }
            else
            {
                // Ошибка при конвертировании в .Tak
                return false;
            }
        }

        Boolean StartDecompession(String fullPath)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo("TAK\\takc.exe");
            startInfo.Arguments = "-d " + "\"" + fullPath + "\"";
            Process TakCompressor = new Process();
            startInfo.WindowStyle = ProcessWindowStyle.Minimized;
            TakCompressor.StartInfo = startInfo;
            TakCompressor.Start();
            TakCompressor.WaitForExit();
            if (TakCompressor.ExitCode == 0)
            {
                return true;
            }
            else
            {
                // Ошибка при распаковке .tak
                return false;
            }
        }
    }
}
