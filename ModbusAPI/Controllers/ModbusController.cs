using FieldTalk.Modbus.Master;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModbusAPI.JSONHandling;
using ModbusAPI.Models;

namespace ModbusAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModbusController : ControllerBase
    {

        //Declarations
        static MbusTcpMasterProtocol mbusProtocol = new MbusTcpMasterProtocol();
        static int result;
        static Device ModbusDevice = new Device();

        Dictionary<string, AnalogueInput> transmitters;
        

        public ModbusController()
        {
            ModbusDevice = DeviceDeserialiser.GetDeviceConfig();

            transmitters = AIDeserialiser.AiDictionary();
            result = startClient();

        }

        public static int startClient()
        {
            return mbusProtocol.openProtocol("127.0.0.1");
        }

        [HttpGet("api/ConnectionStatus")]
        public bool GetConnectionStatus()
        {
            if (result != BusProtocolErrors.FTALK_SUCCESS)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        [HttpGet("api/DeviceDetails")]
        public Device GetDeviceDetails()
        {
            return ModbusDevice;
        }

        [HttpGet("api/ReadMultipleRegisters")]
        public IActionResult ReadMultipleRegisters()
        {
            Dictionary<string, double> result = new Dictionary<string, double>();
            Int16[] dataArr = new Int16[transmitters.Count];
            mbusProtocol.readMultipleRegisters(1, 1, dataArr);

            for (int i = 0; i < transmitters.Count; i++)
            {
                string key = transmitters.ElementAt(i).Key;
                double value = dataArr[i] * transmitters.ElementAt(i).Value.Multiplier;
                result.Add(key, value);
            }

            return Ok(result);
        }


    }
}
