using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace CWLibrary
{
    [Serializable]
    public class Dictionary
    {
        static Random rnd = new Random();
        public int locale;
        public List<Pair<string, string>> words = new List<Pair<string, string>>();

        public Dictionary(List<Pair<string, string>> words)
        {
            locale = rnd.Next(2);
            this.words = words;
        }

        public Dictionary()
        {
            locale = rnd.Next(2);

        }

        public void Add(Pair<string, string> pair)
        {
            words.Add(pair);
        }
        public void stringsdAdd(string a, string b)
        {
            words.Add(new Pair<string, string>(a, b));
        }
        public void MySerialize(string path)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                formatter.Serialize(fs, this);
                Console.WriteLine("Объект сериализован");
            }
        }
        public static Dictionary MyDeserialize(string path)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream(path, FileMode.Open,FileAccess.Read, FileShare.Read))
            {
                Dictionary newList = (Dictionary)formatter.Deserialize(fs);

                Console.WriteLine("Объект десериализован");
                return newList;
            }
        }
        public IEnumerator GetEnumerator()
        {
            return locale == 0 ?
                (from word in words
                orderby word.t
                select word).GetEnumerator()
                :
                (from word in words
                orderby word.u
                select word).GetEnumerator();
        }

        public IEnumerable GetWords(char letter)
        {
            return locale == 0 ?
                (from word in words
                 orderby word.t
                 where word.t.StartsWith(letter.ToString())
                 select word)
                :
                (from word in words
                 orderby word.u
                 where word.u.StartsWith(letter.ToString())
                 select word);
        }
    }
}
/*
 * 
 * Int locale–полякласса, определяющие тип словаря русско-английский (0) или англо-русский (1).•
 * Закрытый список wordsхранит элементы типа Pair<string, string>(русское слово, английское слово)
 * .•Конструкторы класса (по умолчанию, принимающий список типаPair<string, string>)
 * .Локаль генерируется случайно в конструкторе.•Метод Add (Pair<string, string>) для добавления нового элемента в список words. 
 * А также перегруженный метод Add (string, string).•Итератор, позволяющий перебирать с помощью цикла foreach все пары словаря в алфавитном порядке локали 
 * (т.е. если локаль 0, то перебираем в алфавитном порядке по русским словам, если локаль 1, то перебираем в алфавитном порядке по английским словам);
 * •Именнованный  итератор,  позволяющий  перебирать  с  помощью  цикла foreach все  пары  словаря  в алфавитном порядке локали
 * (т.е. если локаль 0, то перебираем в алфавитном порядке по русским словам, если  локаль  1,  то  перебираем  в  алфавитном  порядке  по  английским  словам),
 * начинающиеся  с определенной буквы (задается в качестве параметра);Hint: Нужна будет сортировка с помощью лямбда-выражения для U item2•Метод void MySerialize(string path)
 * для выполнения бинарнойсериализации текущего объекта класса Journal, path –путь к файлу, лежащему в одной папке с *.exe.
 * •Статический * метод DictionaryMyDeserialize(string  path)для  выполнения бинарной десериализации объекта.
 * 
 */
