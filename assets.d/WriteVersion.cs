using System;
using System.IO;

public class WriteVersion
{
    public static void Main(string[] args)
    {
        string fname;
        if (args.Length == 0)
            fname = "version.txt";
        else
            fname = args[0];
        try
        {
            File.WriteAllText(fname, DateTime.Now.ToString("yyyy-MM-dd  HH:mm"));
        }
        catch (Exception e)
        {
            Console.WriteLine("[Error] {0}", e.Message);
        }
    }
}