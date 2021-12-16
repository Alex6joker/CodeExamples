using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Flac_with_CUE_to_Tak
{
    public class ConvertationProcessor
    {
        String beginningPath;

        public ConvertationProcessor(String path)
        {
            beginningPath = path;
        }

        public void TryStartAnalyzingProcess()
        {
            if (Directory.Exists(beginningPath))
            {
                StartAnalyzingProcess();
            }
            else
            {
                new Thread(() => { MessageBox.Show("Директория для начала поиска была не существует или введенный путь неверный\nПроцесс анализа будет прерван"); }).Start();
            }
        }

        void StartAnalyzingProcess()
        {
            CyclingDirAnalyze(beginningPath);
            MessageBox.Show("Готово!");
        }

        void CyclingDirAnalyze(String currentPath)
        {
            // Get common list of subdirs on this dir
            List<String> directories = new DirectoryAnalyzer().GetDirectoriesList(currentPath);
            Analyze(currentPath);
            foreach(String newDir in directories)
            {
                CyclingDirAnalyze(newDir);
            }
        }

        void Analyze(String currentPath)
        {
            DirectoryAnalyzer dirAnalyzer = new DirectoryAnalyzer();
            // Found tak files
            List<String> TakFiles = dirAnalyzer.GetFilesList(currentPath, "*.tak");
            List<String> CueFiles = dirAnalyzer.GetFilesList(currentPath, "*.cue");
            foreach (String TakFile in TakFiles)
            {
                String CueFile = dirAnalyzer.GetCueFileForThisFile(Path.GetFileName(TakFile), Path.GetFileNameWithoutExtension(TakFile), Path.GetDirectoryName(TakFile));
                if (CueFile == null)
                {
                    continue;
                }

                // Decompress this .tak file
                if (!new TakProcessor().DecompressTakFile(TakFile))
                {
                    continue;
                }

                String WAV_File;
                try
                {
                    WAV_File = dirAnalyzer.GetFilesList(currentPath, Path.GetFileNameWithoutExtension(TakFile) + ".wav")[0];
                }
                catch(ArgumentOutOfRangeException)
                {
                    continue;
                }

                // Compress new .wav file to .flac
                if (!new FlacProcessor().CompressWavFile(WAV_File))
                {
                    continue;
                }

                try
                {
                    File.Delete(TakFile);
                    File.Delete(WAV_File);
                }
                catch { }
            }
            foreach(String CueFile in CueFiles)
            {
                // Found .cue file named like .flac on this dir
                new CueWorker().WriteNewExtensionOnCueFile(CueFile, ".flac");
            }
        }
    }
}
