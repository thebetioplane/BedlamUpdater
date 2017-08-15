using System;
using System.IO;
using System.Collections.Generic;
using MD5 = System.Security.Cryptography.MD5;

namespace IndexGenerator
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            List<string> dllFiles = new List<string>();
            string[] files = Directory.GetFiles(Directory.GetCurrentDirectory());
            foreach (string file in files)
            {
                if (file.EndsWith(".dll"))
                {
                    string name = Path.GetFileName(file);
                    Console.WriteLine("Found .dll -> {0}", name);
                    dllFiles.Add(name);
                }
            }
            using (FileStream fs = new FileStream("index", FileMode.Create, FileAccess.Write))
            using (BinaryWriter writer = new BinaryWriter(fs))
            {
                writer.Write(dllFiles.Count);
                foreach (string file in dllFiles)
                {
                    byte[] hash = MD5File(file);
                    if (hash != null)
                    {
                        writer.Write(file);
                        writer.Write(hash);
                    }
                }
            }
        }

        private static byte[] MD5File(string fname)
        {
            try
            {
                using (FileStream fs = new FileStream(fname, FileMode.Open, FileAccess.Read))
                using (MD5 md5 = MD5.Create())
                {
                    return md5.ComputeHash(fs);
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
