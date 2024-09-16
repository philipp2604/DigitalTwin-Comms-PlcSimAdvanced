using DigitalTwin_Comms_PlcSimAdvanced.Constants;
using Siemens.Simatic.Simulation.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalTwin_Comms_PlcSimAdvanced.Converters;
public static class CommunicationInterfaceTypeConverter
{
    public static ECommunicationInterface ConvertCommunicationInterfaceType(CommunicationInterfaceType type)
    {
        return type switch
        {
            CommunicationInterfaceType.None => ECommunicationInterface.None,
            CommunicationInterfaceType.Softbus => ECommunicationInterface.Softbus,
            CommunicationInterfaceType.TCPIP => ECommunicationInterface.TCPIP,
            _ => throw new NotImplementedException()
        };
    }
}