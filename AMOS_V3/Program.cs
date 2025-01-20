

using Microsoft.Win32;
using System.IO;
using System.Xml.Linq;

namespace AMOS_V4
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {

            
            //// To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            //CreateTestFile.Create();
            //LoadBiODatabase.Load("C:\\Users\\42073\\Pictures\\data-poznavacka BiO AB-20250109T083551Z-001\\data-poznavacka BiO AB\\amphibia", "C:\\Users\\42073\\source\\repos\\AMOS_V3\\AMOS_V3\\bin\\Debug\\net6.0-windows\\TestDatabase.Amos");

            ApplicationConfiguration.Initialize();
            
            if (args.Length > 0)
            {
                Form1 aplication = new Form1();
                string filePath = args[0];

                if (File.Exists(filePath))
                {

                    // Naètìte a zpracujte soubor
                    aplication.Open(filePath);
                }
                else
                {
                    throw new Exception("File is corrupted");
                }
                Application.Run(aplication);
            }
            else { Application.Run(new Form1()); }
            
        }
    }
}