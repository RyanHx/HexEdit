﻿using System;
using System.Collections.Generic;
using System.IO;

namespace HexEdit
{
    class Program
    {
        static void Main(string[] args)
        {
            var failed = new Dictionary<string,string>();
            byte[] bytes = new byte[] { 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00 };
            foreach(var path in args)
            {
                try
                {
                    Stream outStream = File.Open(path, FileMode.Open);
                    outStream.Seek(26, SeekOrigin.Begin);
                    outStream.Write(bytes, 0, 13);
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
