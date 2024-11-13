using Newtonsoft.Json.Linq;
using System.Collections;
using System.IO;
using UnityEngine;

namespace Assets.MyFolder.Scripts
{
    /// <summary>
    /// Utility Class for JSON.
    /// </summary>
    public class JsonUtil : MonoBehaviour
    {
        // Function for getting specific value from given key
        public static string GetValueFromPacket(JObject packet, string key)
        {
            if (!packet.TryGetValue(key, out JToken value))
            {
                throw new System.FormatException($"Key '{key}' not found in the JSON packet!");
            }
            return value?.ToString();
        }

        // Function for reading the JSON file
        public static string LoadJsonFile(string jsonFileName)
        {
            Debug.Log($"Loading JSON file: {jsonFileName}"); // Debug: Print het bestand dat je probeert te laden
            TextAsset jsonFile = Resources.Load<TextAsset>(jsonFileName);

            if (jsonFile != null)
            {
                Debug.Log("JSON file successfully loaded!"); // Bevestiging dat het bestand is gevonden
                return jsonFile.text;
            }
            else
            {
                Debug.LogError($"JSON file not found: {jsonFileName}"); // Foutmelding
                return string.Empty;
            }
        }

        public static JObject ParseJsonString(string jsonString)
        {
            return string.IsNullOrEmpty(jsonString) ? null : JObject.Parse(jsonString);
        }

        public static JArray GetStepsArray(JObject manual)
        {
            return manual?["steps"] as JArray;
        }
    }
}