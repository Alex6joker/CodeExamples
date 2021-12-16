using System;
using System.Collections.Generic;
using System.IO;

namespace Flac_with_CUE_to_Tak
{
    public class DirectoryAnalyzer
    {

        public List<String> GetDirectoriesList(String currentPath)
        {
            List<String> directoriesList = new List<String>();
            try
            {
                directoriesList.AddRange(Directory.GetDirectories(currentPath));
            }
            catch { }
            return directoriesList;
        }

        public List<String> GetFilesList(String currentPath, String mask)
        {
            List<String> flacFilesList = new List<String>();
            flacFilesList.AddRange(Directory.GetFiles(currentPath, mask));
            return flacFilesList;
        }

        public String GetCueFileForThisFile(String fileNameWExtension, String fileName, String path)
        {
            List<String> cueFiles = GetFilesList(path, "*.cue");
            foreach(String cueFile in cueFiles)
            {
                if(ExistsCueNamedLikeFile(fileName, Path.GetFileNameWithoutExtension(cueFile)))
                {
                    return cueFile;
                }
            }
            return null;
        }

        Boolean ExistsCueNamedLikeFile(String flacFileName, String cueFileName)
        {
            if(String.Equals(flacFileName, cueFileName))
            {
                return true;
            }
            else
            {
                return false;
            }            
        }
    }
}
