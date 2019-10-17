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
            Config letturaConfig = configurazione.readConfig(AppDomain.CurrentDomain.BaseDirectory + @"r-config.json");
            if (letturaConfig != null)
            {
                Console.WriteLine("Configurazione caricata con successo");
                Console.WriteLine("Directory eseguibile R.exe: " + letturaConfig.path);
            }
            else
            {
                Console.WriteLine("File non caricato");
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
                        Console.WriteLine("Eseguibile R trovato");
                        configurazione.path = newPath;
                        configurazione.createConfig(configurazione);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Eseguibile non trovato");
                    }
                } while (true);
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
