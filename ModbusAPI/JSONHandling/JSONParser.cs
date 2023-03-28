using System.Text.Json;

namespace ModbusAPI.JSONHandling
{
    public class JSONParser
    {
        
        private const string analogueFilePath = @"Configurations/AnalogueInputs.json";

        //Convert JSON to string
        static string jsonAnlogueString = File.ReadAllText(analogueFilePath);

        public static (int address, int multiplier) parseAnalogueInput(string pid)
        {
            // Deserialize the JSON string to a JsonDocument
            JsonDocument doc = JsonDocument.Parse(jsonAnlogueString);

            // Access the root element of the JsonDocument
            JsonElement root = doc.RootElement;

            int address = root.GetProperty(pid).GetProperty("address").GetInt32();
            int multiplier = root.GetProperty(pid).GetProperty("address").GetInt32();

            return (address,multiplier);


        }
    }
}
