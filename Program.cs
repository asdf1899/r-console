using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;


namespace r_console
{
    class Program
    {
        static void Main(string[] args)
        {
            Config configurazione = new Config();
            Console.WriteLine("##################################");
            Console.WriteLine("R CONSOLE v1.0");
            Console.WriteLine("##################################");
            Console.WriteLine("");
            Console.WriteLine("Caricamento configurazione dalla cartella " + AppDomain.CurrentDomain.BaseDirectory);
            bool letturaConfig = configurazione.readConfig(AppDomain.CurrentDomain.BaseDirectory);
            if (letturaConfig)
            {

            }
            else
            {
                Console.WriteLine("File non caricato");
                Console.WriteLine("--------------------------------------------------");
                Console.WriteLine("Crea una nuova configurazione" + @"Percorso simile a questo 'C:\Program Files\R\R-3.6.1\bin'");
                Console.WriteLine();
                Console.Write("Inserisci il percorso dell\' eseguibile di R: ");
                string newPath = Console.ReadLine();
                if (Config.checkIfExist(Path.Combine(newPath, "R.exe")))
                {
                    Console.WriteLine("Eseguibile R trovato");
                }
                else
                {
                    Console.WriteLine(newPath + "/R.exe " + "eseguibile non trovato");
                }
            }
        }
    }
    class Config
    {
        private string path { get; set; }
        public Config()
        {

        }
        public static bool checkIfExist(string path)
        {
            return (File.Exists(path));
        }
        public bool readConfig(string filePath)
        {
            if (checkIfExist(filePath))
            {
                JObject newConfig = JObject.Parse(File.ReadAllText(filePath));
                return true;
            }
            else
            {
                return false;
            }
        }
        public void createConfig(string filePath)
        {
            this.path = filePath;
        }
    }
}
