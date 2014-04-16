using System.Collections.Generic;
using System.IO;

namespace GAIA.Common
{
    public static class IOHelper
    {
        public static void CopyAll(string source_dir, string destination_dir)
        {
            // Create subdirectory structure in destination    
            foreach (string dir in Directory.GetDirectories(source_dir, "*", System.IO.SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(destination_dir + dir.Substring(source_dir.Length));
            }

            foreach (string file_name in Directory.GetFiles(source_dir, "*.*", System.IO.SearchOption.AllDirectories))
            {
                File.Copy(file_name, destination_dir + file_name.Substring(source_dir.Length), true);
                File.SetAttributes(destination_dir + file_name.Substring(source_dir.Length), FileAttributes.Normal);
            }
        }

        public static IEnumerable<string> GetTemplateFiles(string targetPath, string searchCriteria)
        {
            var allFiles = Directory.EnumerateFiles(targetPath, "*.*", System.IO.SearchOption.AllDirectories);

            foreach (string fileName in allFiles)
            {
                var contents = File.ReadLines(fileName);

                foreach (string line in contents)
                {
                    if (line.Contains(searchCriteria)) yield return fileName;
                }
            }
        }

        public static void Move(string source, string dest, bool overwrite = false)
        {
            if (File.Exists(dest)) File.Delete(dest);
            File.Move(source, dest);
        }

        public static void Copy(string source, string dest, bool overwrite = false)
        {
            File.Copy(source, dest, overwrite);
        }
    }
}
