using ModbusAPI.Models;
using System.Text.Json;

namespace ModbusAPI.JSONHandling
{
    public class DeviceDeserialiser
    {
        private const string JsonFilePath = @"Configurations/DeviceConfig.json";

        public static Device GetDeviceConfig()
        {
            try
            {
                Device ModbusDevice = new Device();
                var jsonString = File.ReadAllText(JsonFilePath);

                JsonDocument doc = JsonDocument.Parse(jsonString);

                JsonElement root = doc.RootElement;

                ModbusDevice.IpAddress = root.GetProperty("Device").GetProperty("IpAddress").GetString();
                ModbusDevice.Port = root.GetProperty("Device").GetProperty("Port").GetInt32();
                ModbusDevice.DeviceId = root.GetProperty("Device").GetProperty("DeviceId").GetInt32();


                return ModbusDevice;
            }
            catch (FileNotFoundException ex)
            {
                throw new FileNotFoundException($"The file {JsonFilePath} could not be found.", ex);
            }
            catch (JsonException ex)
            {
                throw new JsonException($"Error deserializing JSON from {JsonFilePath}.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting device configuration from {JsonFilePath}.", ex);
            }
        }
    }
}
