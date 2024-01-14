using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Core.Saves
{
    public static class SavesManager
    {
        private static string BasePath => Application.persistentDataPath;
        private static string SettingsPath => BasePath + "/Settings.dat";

        private static string GamePath => BasePath + "/Game.dat";
        
        public static void SaveSettings()
        {
            Save(new SettingsSave(), SettingsPath);
        }

        public static void SaveGame()
        {
            Save(new GameSave(), GamePath);
        }

        public static bool LoadSettings()
        {
            return Load<SettingsSave>(SettingsPath);
        }

        public static bool LoadGame()
        {
            return Load<GameSave>(GamePath);
        }

        private static void Save(Save data, string path)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Create);
            
            formatter.Serialize(stream, data);
            stream.Close();
        }

        private static bool Load<T>(string path) where T : Save
        {
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);
                T data = formatter.Deserialize(stream) as T;
                data?.Load();
                stream.Close();
                return true;
            }
            
            Debug.unityLogger.Log($"No save file found at {path}");
            return false;
        }
    }
}