

namespace AMOS_V4
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            Quiz main = new Quiz();
            main.OpenLecture("C:\\Users\\42073\\source\\repos\\AMOS_V3\\AMOS_V3\\TestFiles\\TestLection.json");
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}