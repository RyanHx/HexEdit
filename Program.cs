using System;
using System.Text;
using System.Collections.Generic;
using System.IO;

namespace HexEdit
{
    class Program
    {
        static void Main(string[] args)
        {
            var failed = new Dictionary<string,string>();
            int offset = Convert.ToInt32("26", 16); // Hexadecimal offset 26 -> decimal offset
            int length = Convert.ToInt32("39", 16) - offset; // Range between 26h to 39h

            foreach (var path in args)
            {
                try
                {
                    Stream outStream = File.Open(path, FileMode.Open);
                    outStream.Seek(offset, SeekOrigin.Begin);
                    for(int i = 0; i < length + 1; i++)
                    {
                        outStream.WriteByte(00);
                    }
                    outStream.Flush();
                }
                catch(Exception e)
                {
                    failed.Add(path, $"Reason: {e.Message}");
                }                
            }

            if(failed.Count > 0)
            {
                Console.WriteLine("Failed on following files:");
                foreach(var f in failed)
                {
                    Console.WriteLine($"{f.Key}     {f.Value}");
                }               
            }
            else
            {
                Console.WriteLine("Operation finished.");
            }
            Console.ReadLine();
        }
    }
}
