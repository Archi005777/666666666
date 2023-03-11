using Newtonsoft.Json;
using System.Xml.Serialization;

namespace текстредактор
{
    internal class Program
    {
        public class autors
        {
            public List<autor> str;

            public class autor
            {
                public string name;
                public string[] album;
                public int trec;
            }
            public autors()
            {
                autor pink = new autor();
                pink.name = "Pink Floyd";
                pink.album = new string[] { "The Piper At The Gate Of Dawn", "A Saucerful of Secret", "More", "Atom Heart Mother", "Meddle", "Obscured By Clouds" };
                pink.trec = 7;

                autor strat = new autor();
                strat.name = "Stratovarius";
                strat.album = new string[] { "Elements Pt.1", "Eagleheart", "Intermission", "Infinite", "Hunting High And Low (Ep)", "Destiny" };
                strat.trec = 6;

                autor kraft = new autor();
                kraft.name = "KRAFTWERK";
                kraft.album = new string[] { "Computerwelt", "Electric cafe", "The mix", "Tribute to Kraftwerk" };
                kraft.trec = 4;

                str = new List<autor>();
                str.Add(pink);
                str.Add(strat);
                str.Add(kraft);
                //первичная инициализация закончена
            }
        }
        public class opensave
        {
            private void saveastxt(string path, List<autors.autor> str)
            {
                File.WriteAllText(path, str.ToString());
            }
            private void saveasjson(string path, List<autors.autor> str)
            {
                string json = JsonConvert.SerializeObject(str);
                File.WriteAllText(path, json);
            }
            private autors openasjson(string path)
            {
                return (autors)JsonConvert.DeserializeObject(File.ReadAllText(path), typeof(autors));
            }
            private void saveasxml(string path, List<autors.autor> str)
            {
                XmlSerializer xml = new XmlSerializer(typeof(autors));
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                {
                    xml.Serialize(fs, str);
                }
            }
            private autors openasxml(string path)
            {
                XmlSerializer xml = new XmlSerializer(typeof(autors));
                using (FileStream fs = new FileStream(path, FileMode.Open))
                {
                    return (autors)xml.Deserialize(fs);
                }
            }
            public void save(string path, List<autors.autor> str)
            {
                string l = path.Substring(path.Length - 4, 4);
                if (0 == string.Compare(l, "json")) { saveasjson(path, str); }
                if (0 == string.Compare(l, ".xml")) { saveasxml(path, str); }
                if (0 == string.Compare(l, ".txt")) { saveastxt(path, str); }
            }
            public void open(string path, List<autors.autor> str)
            {
                string l = path.Substring(path.Length - 4, 4);
                if (0 == string.Compare(l, "json")) { openasjson(path); }
                if (0 == string.Compare(l, ".xml")) { openasxml(path); }
            }
        }
        static void Main(string[] args)
        {
            autors ai = new autors();
            opensave os = new opensave();

            Console.WriteLine("F1 - открыть файл, F2 - сохранить файл, Esc - выход");
            Console.WriteLine("");
            Console.WriteLine("имя файла");
            foreach (autors.autor aut in ai.str)
            {
                Console.WriteLine("автор - " + aut.name);
                foreach (string st in aut.album) { Console.Write(st + " "); }
                Console.WriteLine("");
                Console.WriteLine("альбомов - " + aut.trec);
            }

            ConsoleKeyInfo cki;
            Console.TreatControlCAsInput = true;
            int i = 0;
            do
            {
                cki = Console.ReadKey();
                switch (cki.Key)
                {
                    case ConsoleKey.F1:
                        Console.SetCursorPosition(1, 1);
                        Console.WriteLine("Введите имя файла");
                        os.open(Console.ReadLine(), ai.str);
                        i = 1;
                        break;
                    case ConsoleKey.F2:
                        Console.SetCursorPosition(1, 1);
                        Console.WriteLine("Введите имя файла");
                        os.save(Console.ReadLine(), ai.str);
                        i = 2;
                        break;
                    case ConsoleKey.Escape: i = 5; break;
                }
            } while (i != 5);
        }
    }
}