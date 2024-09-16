using DigitalTwin_Comms_PlcSimAdvanced.Constants;
using Siemens.Simatic.Simulation.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalTwin_Comms_PlcSimAdvanced.Converters;
public static class CommunicationInterfaceConverter
{
    public static ECommunicationInterface ConvertCommunicationInterfaceType(CommunicationInterface type)
    {
        return type switch
        {
            CommunicationInterface.None => ECommunicationInterface.None,
            CommunicationInterface.Softbus => ECommunicationInterface.Softbus,
            CommunicationInterface.TCPIP => ECommunicationInterface.TCPIP,
            _ => throw new NotImplementedException()
        };
    }

    public static CommunicationInterface ConvertCommunicationInterfaceType(ECommunicationInterface type)
    {
        return type switch
        {
            ECommunicationInterface.None => CommunicationInterface.None,
            ECommunicationInterface.Softbus => CommunicationInterface.Softbus,
            ECommunicationInterface.TCPIP => CommunicationInterface.TCPIP,
            _ => throw new NotImplementedException()
        };
    }
}