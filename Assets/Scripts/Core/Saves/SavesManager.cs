using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Core.Saves
{
    public static class SavesManager
    {
        private static string Path => Application.persistentDataPath;


        public static void Save(SaveObject data, string path)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(Path + path, FileMode.Create);
            
            formatter.Serialize(stream, data);
            stream.Close();
        }

        public static T Load<T>(string path) where T : SaveObject
        {
            if (!File.Exists(Path + path)) return null;
            
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(Path + path, FileMode.Open);
            T data = formatter.Deserialize(stream) as T;
            stream.Close();
            
            return data;
        }
    }
}