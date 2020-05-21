using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CWLibrary;
using System.IO;
using System.Globalization;

namespace ControlWork
{
    class Program
    {
        static Random rnd = new Random();
        static string pathIn = @"C:\Users\zod75\source\repos\06-04-prepare-to-cw-PashchenkoYaroslav\ControlWork\dictionary.txt";
        static string pathOut = @"C:\Users\zod75\source\repos\06-04-prepare-to-cw-PashchenkoYaroslav\ControlWork\out.bin";
        static void Main(string[] args)
        {
            try
            {
                int N = GetNumber<int>("Введите число строк : ", x => x > 0);
                CreateFile(pathIn, N);
                List<Pair<string, string>> pair = new List<Pair<string, string>>();

                using (var sr = new StreamReader(new FileStream(pathIn, FileMode.Open, FileAccess.Read, FileShare.Read)))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] temp = line.Split();
                        pair.Add(new Pair<string, string>(temp[0], temp[1]));
                    }
                }

                Dictionary dict = new Dictionary(pair);
                dict.MySerialize(pathOut);
                Dictionary dict2 = Dictionary.MyDeserialize(pathOut);
                foreach (var runner in dict2.words)
                    Console.WriteLine(runner);
                Console.WriteLine("----------------------");
                foreach (var runner in dict2.GetWords(dict.locale == 1 ? (char)rnd.Next('а', 'я' + 1) : (char)rnd.Next('a', 'z' + 1)))
                    Console.WriteLine(runner);
            }
            catch (IOException ioex)
            {
                Console.WriteLine(ioex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        /// <summary>
        /// Метод создает файл словаря.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="N"></param>
        private static void CreateFile(string path, int N)
        {
            using (StreamWriter sw = new StreamWriter(path, false))
            {
                for (int i = 0; i < N; i++)
                {
                    sw.WriteLine($"{GenerateName('a', 'z')} {GenerateName('а', 'я')}");
                }
            }
        }

        /// <summary>
        /// Метод генерирует имя.
        /// </summary>
        /// <returns></returns>
        static string GenerateName(char startLetter, char finishLetter)
        {
            int len = rnd.Next(3, 8);
            var sb = new StringBuilder();
            for (int i = 0; i < len; i++)
                sb.Append((char)rnd.Next(startLetter, finishLetter + 1));
            return sb.ToString();
        }
        /// <summary>
        /// Метод возвращает введенное 
        /// число если оно удовлетворяет условиям.
        /// </summary>
        /// <returns></returns>
        public static T GetNumber<T>(string str, Predicate<T> conditions)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(str);
            do
            {
                try
                {
                    var result = (T)Convert.ChangeType(Console.ReadLine(), typeof(T));
                    if (conditions(result))
                        return result;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Введенная строка не является командой");
                }
                catch (InvalidCastException icex)
                {
                    Console.WriteLine(icex.Message);
                }
                catch (OverflowException oex)
                {
                    Console.WriteLine(oex.Message);
                }
                catch (FormatException fex)
                {
                    Console.WriteLine(fex.Message);
                }
                catch (ArgumentNullException anex)
                {
                    Console.WriteLine(anex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                Console.ResetColor();
            } while (true);
        }
    }
}
