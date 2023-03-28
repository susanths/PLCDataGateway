using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace ModbusAPI.JSONHandling
{
    public class AIDeserialiser
    {
        private const string analogueFilePath = @"Configurations/AnalogueInputs.json";

        public static Dictionary<string, AnalogueInput> AiDictionary()
        {
            string jsonAnlogueString = File.ReadAllText(analogueFilePath); //Convert JSON to string
            return JsonSerializer.Deserialize<Dictionary<string, AnalogueInput>>(jsonAnlogueString);
        }
    }
}
