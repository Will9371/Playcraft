// Credit: Game Dev Guide
// Source: https://www.youtube.com/watch?v=5roZtuqZyuw

using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Playcraft.Examples.Saving;
using UnityEngine;

namespace Playcraft
{
    public class SerializationManager
    {
        static string persistentPath => Application.persistentDataPath;
        
        static string path => "/saves/";
        static string fileName => "Save";
        static string extension => ".save";
        static string fullPath => path + fileName + extension;
        static object saveData => SaveData.current;

        public static bool Save() { return Save(fileName, saveData); }
        public static bool Save(string saveName, object saveData)
        {
            //Debug.Log("Saving " + saveName);
            var formatter = GetBinaryFormatter();
            
            if (!Directory.Exists(persistentPath + "/saves"))
                Directory.CreateDirectory(persistentPath + "/saves");
            
            var path = persistentPath + "/saves/" + saveName + ".save";
            var file = File.Create(path);
            
            formatter.Serialize(file, saveData);
            file.Close();
            
            return true;
        }
        
        public static object Load() { return Load(fullPath); }
        public static object Load(string _path)
        {
            var path = persistentPath + _path;
            //Debug.Log("Loading " + path);
        
            if (!File.Exists(path))
                return null;
                
            var formatter = GetBinaryFormatter();
            var file = File.Open(path, FileMode.Open);

            try
            {
                var save = formatter.Deserialize(file);
                file.Close();
                return save;
            }
            catch
            {
                Debug.LogErrorFormat("Failed to open save file at {0}", path);
                file.Close();
                return null;
            }
        }
        
        public static BinaryFormatter GetBinaryFormatter()
        {
            var formatter = new BinaryFormatter();
            var selector = new SurrogateSelector();
            
            var vector3Surrogate = new Vector3SerializationSurrogate();
            var quaternionSurrogate = new QuaternionSerializationSurrogate();

            selector.AddSurrogate(typeof(Vector3), new StreamingContext(StreamingContextStates.All), vector3Surrogate);
            selector.AddSurrogate(typeof(Quaternion), new StreamingContext(StreamingContextStates.All), quaternionSurrogate);
            
            formatter.SurrogateSelector = selector;
            return formatter;
        }
    }
}
