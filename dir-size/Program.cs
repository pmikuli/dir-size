using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dir_size
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length == 1)
            {
                var parentDirectory = args[0];
                var dirs = Directory.GetDirectories(parentDirectory);

                var result = new Dictionary<string, long>();

                foreach (var dir in dirs)
                {
                    var size = GetDirectorySize(dir);
                    result.Add(dir, size);
                }

                var sorted = result.ToList();
                sorted.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));
                foreach (var item in sorted)
                {
                    Console.WriteLine(item.Key + " -> " + item.Value + " KiloBytes");
                }
            }

#if DEBUG
            Console.ReadKey();
#endif
        }

        private static long GetDirectorySize(string directory)
        {
            try
            {
                long size = 0;
                var dirs = Directory.GetDirectories(directory);
                foreach (var dir in dirs)
                {
                    size += GetDirectorySize(dir);
                }

                var files = Directory.GetFiles(directory);
                foreach (var file in files)
                {
                    size += new FileInfo(file).Length / 1000;
                }

                return size;
            }
            catch (UnauthorizedAccessException)
            {
                return 0;
            }
        }
    }
}