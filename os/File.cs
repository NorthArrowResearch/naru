using System;

namespace naru.os
{
    public class File
    {
        public static System.IO.FileInfo GetNewSafeName(string sDirectory, string sFileName, string sExtension)
        {
            if (string.IsNullOrEmpty(sDirectory))
                throw new Exception(string.Format("Invalid directory {0}", sDirectory));

            sFileName = RemoveDangerousCharacters(sFileName);

            int nInteration = 0;
            string sCandidate = string.Empty;
            do
            {
                sCandidate = System.IO.Path.Combine(sDirectory, sFileName);

                if (nInteration > 0)
                    sCandidate = string.Format("{0}{1}", sCandidate, nInteration);

                sCandidate = System.IO.Path.ChangeExtension(sCandidate, sExtension);

                nInteration++;
            } while (System.IO.File.Exists(sCandidate) && nInteration < 1000);

            return new System.IO.FileInfo(sCandidate);
        }

        public string GetNewSafeDirectoryName(string sParent, string sRootName)
        {
            if (string.IsNullOrEmpty(sParent))
            {
                throw new ArgumentNullException("The parent folder cannot be an empty string.");
            }
            else
            {
                if (!System.IO.Directory.Exists(sParent))
                {
                    ArgumentException ex = new ArgumentException("The parent folder must already exist.");
                    ex.Data["Parent folder"] = sParent;
                    throw ex;
                }
            }

            if (string.IsNullOrEmpty(sRootName))
            {
                throw new ArgumentNullException("The root name cannot be an empty string.");
            }

            string sNewName = null;
            int nCount = 0;
            string sResult = null;

            do
            {
                sNewName = sRootName;
                if (nCount > 0)
                {
                    sNewName += nCount.ToString();
                }
                sResult = System.IO.Path.Combine(sParent, sNewName);
                nCount += 1;
            } while (System.IO.Directory.Exists(sResult) && nCount < 9999);

            return sResult;
        }

        public static string RemoveDangerousCharacters(string sInput)
        {
            string sResult = sInput;
            if (!string.IsNullOrEmpty(sInput))
            {
                foreach (char c in System.IO.Path.GetInvalidFileNameChars())
                {
                    sResult = sResult.Replace(c.ToString(), "");
                }
            }

            sResult = sResult.Replace("-", "");
            sResult = sResult.Replace("_", "");
            sResult = sResult.Replace(" ", "");
            sResult = sResult.Replace(".", "");
            sResult = sResult.Replace("!", "");
            sResult = sResult.Replace("@", "");
            sResult = sResult.Replace("#", "");
            sResult = sResult.Replace("$", "");
            sResult = sResult.Replace("%", "");
            sResult = sResult.Replace("^", "");
            sResult = sResult.Replace("&", "");
            sResult = sResult.Replace("*", "");
            sResult = sResult.Replace("(", "");
            sResult = sResult.Replace(")", "");
            sResult = sResult.Replace("+", "");
            sResult = sResult.Replace("=", "");
            sResult = sResult.Replace("'", "");
            sResult = sResult.Replace("~", "");
            sResult = sResult.Replace("`", "");
            sResult = sResult.Replace("{", "");
            sResult = sResult.Replace("}", "");
            sResult = sResult.Replace("[", "");
            sResult = sResult.Replace("]", "");
            sResult = sResult.Replace(";", "");
            sResult = sResult.Replace(",", "");

            return sResult;
        }


        public static string TrimFilename(string filename, int trimmedlength)
        {
            string trimmedfilename = "";

            if (filename.Length > trimmedlength)
            {
                string[] PathSegments = filename.Split(System.IO.Path.AltDirectorySeparatorChar);
                trimmedfilename = PathSegments[PathSegments.Length - 1];
                int PathIndex = PathSegments.Length - 2;
                while (PathIndex >= 0 && trimmedfilename.Length + PathSegments[PathIndex].Length < 80)
                {
                    trimmedfilename = System.IO.Path.Combine(PathSegments[PathIndex], trimmedfilename);
                    PathIndex = PathIndex - 1;
                }
                if (PathIndex > -1)
                {
                    trimmedfilename = "...\\" + trimmedfilename;
                }
            }
            else
            {
                trimmedfilename = filename;
            }
            return trimmedfilename;
        }

        public static string GetFormattedFileSize(System.IO.FileInfo fiFile)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            double len = fiFile.Length;
            int order = 0;
            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len = len / 1024;
            }

            // Adjust the format string to your preferences. For example "{0:0.#}{1}" would
            // show a single decimal place, and no space.
            return String.Format("{0:0.##} {1}", len, sizes[order]);
        }
    }
}
