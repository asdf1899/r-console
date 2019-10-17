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
            var config = loadConfig(configurazione);
            if (config != null)
            {
                menu(config);
            }
            else
            {
                newConfig(configurazione);
            }
        }
        public static void menu(Config configurazione)
        {
            while (true)
            {
                Console.WriteLine("\nMenu", Color.Aquamarine);
                Console.WriteLine("1) Esegui un file R", Color.Aquamarine);
                Console.WriteLine("2) Nuova configurazione", Color.Aquamarine);
                Console.WriteLine("3) Mostra directory attuale", Color.Aquamarine);
                Console.WriteLine("4) Aiuto", Color.Aquamarine);
                Console.WriteLine("5) Esci", Color.Aquamarine);
                Console.WriteLine("");
                Console.Write("$> ", Color.Yellow);
                string read = Console.ReadLine();
                switch (read)
                {
                    case "1":
                        executeFile(configurazione.path);
                        break;
                    case "2":
                        Console.WriteLine("--------------------------------------------------");
                        configurazione = newConfig(configurazione);
                        loadConfig(configurazione);
                        break;
                    case "3":
                        Console.Write("Directory attuale ");
                        Console.Write("" + configurazione.path.ToString() + "\n", Color.Yellow);
                        break;
                    case "4":
                        Console.WriteLine("Non ne ho voglia di scrivere :)");
                        break;
                    case "5":
                        Environment.Exit(0);
                        break;
                }
            }
        }
        public static void executeFile(string rstudioPath)
        {
            Console.Write("Inserisci il nome del file (senza estensione .r): ");
            string filename = Console.ReadLine();
            filename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename+".r");
            Console.WriteLine(filename);
            if (File.Exists(filename))
            {

            }
            else
            {
                Console.WriteLine(filename + " non trovato", Color.Red);
            }
        }
        public static Config loadConfig(Config configurazione)
        {
            Console.WriteLine("Caricamento configurazione dalla cartella " + AppDomain.CurrentDomain.BaseDirectory);
            Config letturaConfig = configurazione.readConfig(AppDomain.CurrentDomain.BaseDirectory + @"r-config.json");
            if (letturaConfig != null)
            {
                Console.WriteLine("Configurazione caricata con successo", Color.LimeGreen);
                Console.Write("Directory eseguibile Rscript.exe: ");
                Console.WriteLine(letturaConfig.path, Color.Yellow);
                return letturaConfig;
            }
            else
            {
                Console.WriteLine("File non caricato", Color.Red);
                Console.WriteLine("--------------------------------------------------");
                return null;
            }
        }
        public static Config newConfig(Config configurazione)
        {
            Console.Write("Crea una nuova configurazione " + @"Percorso simile a questo ");
            Console.Write(@"'C:\Program Files\R\R-3.6.1\bin'", Color.Yellow);
            Console.WriteLine("\n");
            string newPath;
            do
            {
                Console.Write("Inserisci il percorso dell\' eseguibile di R: ");
                newPath = Console.ReadLine();
                newPath = Path.Combine(newPath, "Rscript.exe");
                if (Config.checkIfExist(newPath))
                {
                    Console.WriteLine("Eseguibile R trovato", Color.LimeGreen);
                    configurazione.path = newPath;
                    configurazione.createConfig(configurazione);
                    return configurazione;
                }
                else
                {
                    Console.WriteLine("Eseguibile non trovato", Color.Red);
                }
            } while (true);
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
