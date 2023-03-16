using System;
using Newtonsoft.Json;
using System.IO;

namespace XMusic
{
    class JsonCon
    {
        // он работает, но не всегда, поэтому я его не использую
        static string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        public static T fromJson<T>(string Filename)
        {
            string text = File.ReadAllText(desktop + "\\" + Filename);
            T des = JsonConvert.DeserializeObject<T>(text);
            return des;
        }

        public static void toJson<T>(T entries, string Filename)
        {
            string entr = JsonConvert.SerializeObject(entries);
            File.WriteAllText(desktop + "\\"+ Filename, entr);
        }

    }
}
