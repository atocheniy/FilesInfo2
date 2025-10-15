using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilesInfoNamespace
{
    public class FilesInfo
    {
        public Dictionary<string, int> info = new Dictionary<string, int>();

        public FilesInfo() { }

        public void Load(string path)
        {
            string[] filePaths = Directory.GetFiles(path, "*", SearchOption.AllDirectories);
            foreach (string file in filePaths) 
            {
                if (info.ContainsKey(Path.GetExtension(file))) info[Path.GetExtension(file)]++;
                else info[Path.GetExtension(file)] = 1;
            }
        }

        public void Save(string path) 
        {
            string textInfo = "";
            var sortedDictionary = info.OrderByDescending(pair => pair.Value);

            foreach (var pair in sortedDictionary) textInfo += $"{pair.Key}: {pair.Value} \n";
            File.WriteAllText(path, textInfo);
        }
    }
}
