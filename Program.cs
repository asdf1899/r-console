using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Drawing;
using Console = Colorful.Console;

namespace r_console
{
    class Program
    {
        static void Main(string[] args)
        {
            Config configurazione = new Config();
            Console.WriteLine("################################################", Color.Yellow);
            Console.WriteAscii("R CONSOLE", Color.Yellow);
            Console.WriteLine("Copyright 2019 @asdf1899", Color.Yellow);
            Console.WriteLine("");
            Console.WriteLine("################################################", Color.Yellow);
            Console.WriteLine("");
            Console.WriteLine("Caricamento configurazione dalla cartella " + AppDomain.CurrentDomain.BaseDirectory);
            Config letturaConfig = configurazione.readConfig(AppDomain.CurrentDomain.BaseDirectory + @"r-config.json");
            if (letturaConfig != null)
            {
                Console.WriteLine("Configurazione caricata con successo", Color.LimeGreen);
                Console.Write("Directory eseguibile R.exe: ");
                Console.WriteLine(letturaConfig.path, Color.Yellow);
                menu(letturaConfig);
            }
            else
            {
                Console.WriteLine("File non caricato", Color.Red);
                Console.WriteLine("--------------------------------------------------");
                Console.WriteLine("Crea una nuova configurazione" + @"Percorso simile a questo 'C:\Program Files\R\R-3.6.1\bin'");
                Console.WriteLine();
                string newPath;
                do
                {
                    Console.Write("Inserisci il percorso dell\' eseguibile di R: ");
                    newPath = Console.ReadLine();
                    newPath = Path.Combine(newPath, "R.exe");
                    if (Config.checkIfExist(newPath))
                    {
                        Console.WriteLine("Eseguibile R trovato", Color.LimeGreen);
                        configurazione.path = newPath;
                        configurazione.createConfig(configurazione);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Eseguibile non trovato", Color.Red);
                    }
                } while (true);
            }
        }
        public static void menu(Config configurazione)
        {
            Console.WriteLine(configurazione.path.ToString());
            while (true)
            {
                Console.WriteLine("\nMenu", Color.Aquamarine);
                Console.WriteLine("1) Nuova configurazione", Color.Aquamarine);
                Console.WriteLine("2) Mostra directory attuale", Color.Aquamarine);
                Console.WriteLine("3) Esci", Color.Aquamarine);
                Console.WriteLine("");
                Console.Write("$> ", Color.Yellow);
                string read = Console.ReadLine();
                switch (read)
                {
                    case "1":

                        break;
                    case "2":
                        Console.Write("Directory ");
                        Console.Write("asd", Color.Yellow);
                        break;
                    case "3":
                        Environment.Exit(0);
                        break;
                }
            }
        }
    }
    class Config
    {
        public string path { get; set; }
        public Config()
        {

        }
        public static bool checkIfExist(string path)
        {
            return (File.Exists(path));
        }
        public Config readConfig(string filePath)
        {
            if (checkIfExist(filePath))
            {
                return JsonConvert.DeserializeObject<Config>(File.ReadAllText(filePath));
            }
            else
            {
                return null;
            }
        }
        public void createConfig(Config configurazione)
        {
            string serialized = JsonConvert.SerializeObject(configurazione);
            Console.WriteLine(serialized);
            try
            {
                System.IO.File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"r-config.json", serialized);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
